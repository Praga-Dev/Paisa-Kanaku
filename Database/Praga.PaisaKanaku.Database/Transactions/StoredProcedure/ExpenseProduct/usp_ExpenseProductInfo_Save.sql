CREATE PROCEDURE [Transactions].[usp_ExpenseProductInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@ExpenseDate DATETIME,
	@ExpenseInfoId UNIQUEIDENTIFIER,
	@ProductInfoId UNIQUEIDENTIFIER,
	@ExpenseById UNIQUEIDENTIFIER,
	@Quantity INT,
	@ExpenseAmount DECIMAL(12,3),
	@Description NVARCHAR(250),
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

	IF(@ExpenseDate IS NULL OR ISDATE(@ExpenseDate) = 0)
	BEGIN
		RAISERROR('INVALID_EXPENSE_DATE', 16, 1);
	END

	--IF THE RECORD DELETED
	IF EXISTS(SELECT 1 FROM [Transactions].[ExpenseProductInfo] WHERE [Id] = @Id AND NOT [RowStatus] = 'A')
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_PRODUCT_INFO_ID', 16, 1);
	END

	IF(@Quantity <= 0)
	BEGIN
		RAISERROR('INVALID_PARAM_QUANTITY', 16, 1);
	END

	IF(@ExpenseAmount <= 0)
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_AMOUNT', 16, 1);
	END

	DECLARE @ExpenseProductInfoId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

	SET @ExpenseDate = DATEADD(dd, 0, DATEDIFF(dd, 0, @ExpenseDate))

	IF(@ExpenseInfoId IS NULL)
	BEGIN
		IF(EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Date] = @ExpenseDate))
		BEGIN
			SELECT @ExpenseInfoId = [Id] FROM [Transactions].[ExpenseInfo] WHERE [Date] = @ExpenseDate
		END
		ELSE
		BEGIN
			SET @ExpenseInfoId = NEWID();

			INSERT INTO [Transactions].[ExpenseInfo] ([Id], [Date], [CreatedBy])
			VALUES( @ExpenseInfoId, @ExpenseDate, @LoggedInUserId)
		END
	END
	ELSE
	BEGIN
		IF(NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Id] = @ExpenseInfoId AND [Date] = @ExpenseDate))
		BEGIN
			RAISERROR('INVALID_PARAM_EXPENSE_INFO_ID', 16, 1);			
		END
	END


	-- EXPENSEPRODUCTINFO UPSERT
	DECLARE @ExpenseAmountResult DECIMAL(12,3);

	IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseProductInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Transactions].[ExpenseProductInfo]
		([Id], [ExpenseInfoId], [ProductInfoId], [ExpenseById], [ExpenseDate], [Quantity], [ExpenseAmount], [Description], [CreatedBy])
		VALUES
		(@ExpenseProductInfoId, @ExpenseInfoId, @ProductInfoId, @ExpenseById, @ExpenseDate, @Quantity, @ExpenseAmount, @Description, @LoggedInUserId)

		EXEC [Common].[usp_v1_Add_To_ExpenseAmount] @ExpenseInfoId = @ExpenseInfoId, @Amount = @ExpenseAmount, @Result = @ExpenseAmountResult OUTPUT;
	END
	ELSE
	BEGIN  	
		DECLARE @OldExpenseAmount DECIMAL(12,3);
		SELECT @OldExpenseAmount = [ExpenseAmount] FROM [Transactions].[ExpenseProductInfo] 
		WHERE [Id] = @ExpenseProductInfoId AND [RowStatus] = 'A'

		UPDATE [Transactions].[ExpenseProductInfo]
			SET	[ProductInfoId] = @ProductInfoId, 
				[ExpenseById] = @ExpenseById, 
				[Quantity] = @Quantity, 
				[ExpenseAmount] = @ExpenseAmount, 
				[Description] = @Description,
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
			WHERE [Id] = @ExpenseProductInfoId AND [RowStatus] = 'A';

		DECLARE @UpdatedExpenseAmount DECIMAL(12,3);
		SET @UpdatedExpenseAmount = @ExpenseAmount - @OldExpenseAmount;

		EXEC [Common].[usp_v1_Add_To_ExpenseAmount] @ExpenseInfoId = @ExpenseInfoId, @Amount = @UpdatedExpenseAmount, @Result = @ExpenseAmountResult OUTPUT;

		IF (@ExpenseAmountResult <= 0)
		BEGIN
			RAISERROR('EXPENSE_AMOUNT_NOT_UPDATED_ON_EXPENSE_INFO_TABLE', 16, 1);
		END
	END	

	SET @Result = @ExpenseProductInfoId;

	RETURN @Response;


END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH