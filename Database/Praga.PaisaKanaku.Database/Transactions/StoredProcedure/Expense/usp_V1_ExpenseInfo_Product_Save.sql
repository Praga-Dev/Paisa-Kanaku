CREATE PROCEDURE [Transactions].[usp_V1_ExpenseInfo_Product_Save]
	@ExpenseDate DATETIME,
	@ExpenseData XML,
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF(@ExpenseDate IS NULL OR ISDATE(@ExpenseDate) = 0)
	BEGIN
		RAISERROR('INVALID_EXPENSE_DATE', 16, 1);
	END

	DECLARE @ExpenseInfoId UNIQUEIDENTIFIER;
	DECLARE @TotalExpense DECIMAL(12,3) = 0;

	DECLARE @handle INT  
	DECLARE @PrepareXmlStatus INT 

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

	-- If the product already exist in the date, we need to increase the quantity of the product.
	-- NOT REQUIRED, BECAUSE IT WILL GIVE CONFUSION TO USER 

	-- Before Insertion move all the items in EI tbl to EIL tbl
	INSERT INTO [Transactions].[ExpenseReferenceDetailLog] ([Id], [ExpenseInfoId], [ReferenceId]
		, [ExpenseById], [DateOfExpense], [Quantity], [ExpenseAmount], [Description], [CreatedBy])
	SELECT 
		[Id], [ExpenseInfoId], [ReferenceId], [ExpenseById], [DateOfExpense], [Quantity], [ExpenseAmount], [Description], [CreatedBy]
	FROM [Transactions].[ExpenseReferenceDetailInfo] 
	WHERE [DateOfExpense] = @ExpenseDate AND [RowStatus] = 'A'

	-- TODO NEED TO HARD DELETE IN TABLES 
	--		[ExpenseReferenceDetailInfo]
	--		[TempExpenseInfo]

	UPDATE [Transactions].[ExpenseReferenceDetailInfo]
	SET [RowStatus] = 'D'
	WHERE [DateOfExpense] = @ExpenseDate;

	INSERT INTO [Transactions].[ExpenseReferenceDetailInfo] ([Id], [ExpenseInfoId], [ReferenceId]
		, [ExpenseById], [DateOfExpense], [Quantity], [ExpenseAmount], [Description], [CreatedBy])
	SELECT
		NEWID(),
		@ExpenseInfoId,
		[ProductId],
		[ExpenseById],
		@ExpenseDate,
		[Quantity],
		[ExpenseAmount],
		[Description],
		@LoggedInUserId
	FROM OPENXML(@handle, '/Expense/Product', 2)
	WITH(
		[ProductId] UNIQUEIDENTIFIER,
		[ExpenseById] UNIQUEIDENTIFIER,
		[Quantity] INT,
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250)
	);

	SET @TotalExpense = (SELECT SUM([ExpenseAmount]) FROM [Transactions].[ExpenseReferenceDetailInfo] WHERE [ExpenseInfoId] = @ExpenseInfoId);

	UPDATE [Transactions].[ExpenseInfo]
	SET [Amount] = @TotalExpense
	WHERE [Id] = @ExpenseInfoId;

	UPDATE [Transactions].[TempExpenseInfo]
	SET [RowStatus] = 'I' -- Change to M - Moved
	WHERE [Date] = @ExpenseDate;

	SET @Result = @ExpenseInfoId;

	RETURN @Response;


END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH