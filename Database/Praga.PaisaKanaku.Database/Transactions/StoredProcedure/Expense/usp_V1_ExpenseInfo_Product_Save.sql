-- BRAND UPDATE IS NOT ALLOWED
-- PRODUCT CATEGORY UPDATE IS NOT ALLOWED

CREATE PROCEDURE [Transactions].[usp_V1_ExpenseInfo_Product_Save]
	@Id UNIQUEIDENTIFIER,
	@ExpenseInfoId UNIQUEIDENTIFIER,
	@ExpenseBy UNIQUEIDENTIFIER,
	@DateOfExpense DATETIME2,
	@ExpenseDescription NVARCHAR(250),
	@IsChangeInProduct BIT,
	@BrandId UNIQUEIDENTIFIER,
	@BrandName NVARCHAR(25),
	@ProductCategoryId UNIQUEIDENTIFIER,
	@ProductCategoryName NVARCHAR(25),
	@ProductId UNIQUEIDENTIFIER,
	@ProductName NVARCHAR(25),
	@ExpenseType NVARCHAR(25),
	@Price DECIMAL(12,3),
	@ProductDescription NVARCHAR(250),
	@PreferredRecurringTimePeriod NVARCHAR(10),
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF(@ExpenseBy IS NULL OR @ExpenseBy = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[MemberInfo] WHERE [Id] = @ExpenseBy))
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_BY', 16, 1);
	END

	-- TODO IF PRODUCT NEW, SAVE IT
	IF(@ProductId IS NULL OR @ProductId = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[ProductInfo] WHERE [Id] = @ProductId))
	BEGIN
		EXEC [Setup].[usp_V1_ProductInfo_Save] @ProductId, @ProductName, @ProductCategoryId, @ProductCategoryName, @BrandId, @BrandName, @ExpenseType, @Price, @ProductDescription, @PreferredRecurringTimePeriod, @LoggedInUserId, @ProductId OUTPUT;
	END

	-- TODO IF PRODUCT NEED TO UPDATE, SAVE IT
	ELSE IF(@IsChangeInProduct = 1)
	BEGIN
		EXEC [Setup].[usp_V1_ProductInfo_Save] @ProductId, @ProductName, @ProductCategoryId, @ProductCategoryName, @BrandId, @BrandName, @ExpenseType, @Price, @ProductDescription, @PreferredRecurringTimePeriod, @LoggedInUserId, @ProductId OUTPUT;
	END

	PRINT 'EXPENSE INFO SAVE'
	-- TODO EXPENSEINFO SAVE

	DECLARE @TempExpenseInfoId UNIQUEIDENTIFIER;

	IF(@ExpenseInfoId IS NULL OR @ExpenseInfoId = @EmptyGuid)
	BEGIN
		IF EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Date] = @DateOfExpense)
		BEGIN
			SELECT TOP 1 @TempExpenseInfoId = [Id] FROM [Transactions].[ExpenseInfo] WHERE [Date] = @DateOfExpense
		END
		ELSE
		BEGIN
			SET @TempExpenseInfoId = NEWID()
		END
	END
	ELSE IF NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Id] = @ExpenseInfoId)
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_INFO_ID', 16, 1);
	END

	IF(NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Date] = @DateOfExpense))
	BEGIN
		PRINT 'EXPENSE INFO CREATE'

		INSERT INTO [Transactions].[ExpenseInfo]
		([Id], [Date], [Amount], [CreatedBy])
		VALUES 
		(@TempExpenseInfoId, @DateOfExpense, @Price, @LoggedInUserId);
	END

	PRINT 'EXPENSE REFERENCE DETAIL INFO SAVE'
	-- TODO EXPENSE REFERENCE DETAIL INFO SAVE

	DECLARE @ExpenseReferenceDetailInfoId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

	IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseReferenceDetailInfo] WHERE [Id] = @Id))
	BEGIN

		PRINT 'EXPENSE INFO SAVE'

		INSERT INTO [Transactions].[ExpenseReferenceDetailInfo]
		([Id], [ExpenseInfoId], [ReferenceId], [ExpenseBy], [DateOfExpense], [ExpenseAmount], [Description], [CreatedBy])
		VALUES 
		(@ExpenseReferenceDetailInfoId, @TempExpenseInfoId, @ProductId, @ExpenseBy, @DateOfExpense, @Price, @ExpenseDescription, @LoggedInUserId);
	END
	ELSE
	BEGIN
		PRINT 'EXPENSE REFERENCE DETAIL INFO UPDATE SAVE'
		PRINT @TempExpenseInfoId
		UPDATE [Transactions].[ExpenseReferenceDetailInfo]
		SET
			[ExpenseInfoId] = @TempExpenseInfoId,
			[ReferenceId] = @ProductId, 
			[ExpenseBy] = @ExpenseBy, 
			[DateOfExpense] = @DateOfExpense, 
			[ExpenseAmount] = @Price,
			[Description] = @ExpenseDescription,
			[ModifiedBy] = @LoggedInUserId,
			[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @Id  AND [CreatedBy] = @LoggedInUserId AND [RowStatus] = 'A';
	END

	
	UPDATE [Transactions].[ExpenseInfo]
	SET
		[Amount] = (SELECT SUM([ExpenseAmount]) FROM [Transactions].[ExpenseReferenceDetailInfo] WHERE [Date] = @DateOfExpense AND [RowStatus] = 'A' AND [CreatedBy] = @LoggedInUserId),
		[ModifiedBy] = @LoggedInUserId,
		[ModifiedDate] = GETUTCDATE()
	WHERE [Date] = @DateOfExpense AND [RowStatus] = 'A' AND [CreatedBy] = @LoggedInUserId;


	SET @Result = @ExpenseReferenceDetailInfoId;
	RETURN @Response;
END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
