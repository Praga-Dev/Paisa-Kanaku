CREATE PROCEDURE [Transactions].[usp_V1_GetAllExpensesByDate]
	@ExpenseDate DATETIME2,
	@LoggedInUserId UNIQUEIDENTIFIER
AS
BEGIN TRY
	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END
	
	IF(@ExpenseDate IS NULL OR @ExpenseDate > GETDATE() OR @ExpenseDate < '1/1/2022')
	BEGIN
		RAISERROR('INVALID_EXPENSE_DATE', 16, 1);
	END

	SET @ExpenseDate = CAST(@ExpenseDate AS DATE);

	DECLARE @ExpenseInfo TABLE (
		[ExpenseType] NVARCHAR(25),
		[ExpenseAmount] DECIMAL(12,3)
	);

	INSERT INTO @ExpenseInfo ([ExpenseType], [ExpenseAmount])
	SELECT 'EXPENSE_GROCERY',ISNULL(SUM(ExpenseAmount), 0) 
	FROM [Transactions].[ExpenseGroceryInfo] 
	WHERE CAST(ExpenseDate AS DATE) = @ExpenseDate
	UNION ALL
	SELECT 'EXPENSE_FAMILY_WELLBEING', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseFamilyWellbeingInfo] 
	WHERE CAST(ExpenseDate AS DATE) = @ExpenseDate
	UNION ALL
	SELECT 'EXPENSE_OUTDOOR_FOOD', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseOutdoorFoodInfo] 
	WHERE CAST(ExpenseDate AS DATE) = @ExpenseDate
	UNION ALL
	SELECT 'EXPENSE_PRODUCT', ISNULL(SUM(ExpenseAmount), 0) 
	FROM [Transactions].[ExpenseProductInfo] 
	WHERE CAST(ExpenseDate AS DATE) = @ExpenseDate
	UNION ALL
	SELECT 'EXPENSE_TRAVEL', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseTravelInfo] 
	WHERE CAST(ExpenseDate AS DATE) = @ExpenseDate;

	SELECT * FROM @ExpenseInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
