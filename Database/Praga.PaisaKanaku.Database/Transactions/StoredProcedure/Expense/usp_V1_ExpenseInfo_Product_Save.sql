CREATE PROCEDURE [Transactions].[usp_V1_ExpenseInfo_Product_Save]
	@ExpenseBy UNIQUEIDENTIFIER,
	@ExpenseDate DATETIME,
	@ExpenseData XML,
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

	IF(@ExpenseDate IS NULL OR ISDATE(@ExpenseDate) = 0)
	BEGIN
		RAISERROR('INVALID_EXPENSE_DATE', 16, 1);
	END

	DECLARE @handle INT  
	DECLARE @PrepareXmlStatus INT 
	
	DECLARE @ExpenseInfoId UNIQUEIDENTIFIER;
	DECLARE @TotalExpense DECIMAL(12,3) = 0;


	IF(EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Date] = @ExpenseDate))
	BEGIN
		SELECT @ExpenseInfoId = [Id] FROM [Transactions].[ExpenseInfo] WHERE [Date] = @ExpenseDate
	END
	ELSE
	BEGIN
		SET @ExpenseInfoId = NEWID();

		INSERT INTO [Transactions].[ExpenseInfo] ([Id], [Date], [Amount], [CreatedBy])
		VALUES( @ExpenseInfoId, @ExpenseDate, @TotalExpense, @LoggedInUserId)
	END

	EXEC @PrepareXmlStatus = [sys].[sp_xml_preparedocument] @handle OUTPUT, @ExpenseData;

	INSERT INTO [Transactions].[ExpenseReferenceDetailInfo] ([Id], [ExpenseInfoId], [ReferenceId]
		, [ExpenseBy], [DateOfExpense], [Quantity], [ExpenseAmount], [Description], [CreatedBy])
	SELECT
		NEWID(),
		@ExpenseInfoId,
		[ProductId],
		@ExpenseBy,
		@ExpenseDate,
		[Quantity],
		[ExpenseAmount],
		[Description],
		@LoggedInUserId
	FROM OPENXML(@handle, '/Expense/Product', 2)
	WITH(
		[ProductId] UNIQUEIDENTIFIER,
		[Quantity] INT,
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250)
	);

	SET @TotalExpense = (SELECT SUM([ExpenseAmount]) FROM [Transactions].[ExpenseReferenceDetailInfo] WHERE [ExpenseInfoId] = @ExpenseInfoId);

	UPDATE [Transactions].[ExpenseInfo]
	SET [Amount] = @TotalExpense
	WHERE [Id] = @ExpenseInfoId;

	SET @Result = @ExpenseInfoId;

	RETURN @Response;


END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH