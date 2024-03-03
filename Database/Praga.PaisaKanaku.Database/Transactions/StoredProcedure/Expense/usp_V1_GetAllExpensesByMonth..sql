CREATE PROCEDURE [Transactions].[usp_V1_GetAllExpensesByMonth]
	@ExpenseMonth TINYINT,
	@ExpenseYear INT,
	@LoggedInUserId UNIQUEIDENTIFIER
AS
BEGIN TRY
	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @CURR_DATE DATETIME2 = GETDATE();
	DECLARE @CURR_YEAR INT = YEAR(@CURR_DATE);
	DECLARE @CURR_MONTH TINYINT= MONTH(@CURR_DATE);

	IF(@ExpenseYear IS NULL OR @ExpenseYear > @CURR_YEAR OR @ExpenseYear < 2022)
	BEGIN
		RAISERROR('INVALID_EXPENSE_YEAR', 16, 1);
	END
	
	IF(@ExpenseMonth IS NULL OR @ExpenseMonth > 12 OR @ExpenseMonth < 1)
	BEGIN
		RAISERROR('INVALID_EXPENSE_MONTH', 16, 1);
	END

	IF(@ExpenseYear = @CURR_YEAR AND @ExpenseMonth > @CURR_MONTH)
	BEGIN
		RAISERROR('ERROR_FUTURE_DATE_IS_NOT_VALID', 16, 1);
	END

	DECLARE @ExpenseInfo TABLE (
		[ExpenseType] NVARCHAR(25),
		[ExpenseAmount] DECIMAL(12,3)
	);

	INSERT INTO @ExpenseInfo ([ExpenseType], [ExpenseAmount])
	SELECT 'EXPENSE_GROCERY', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseGroceryInfo] 
	WHERE MONTH(ExpenseDate) = @ExpenseMonth AND YEAR(ExpenseDate) = @ExpenseYear
	
	UNION ALL
	SELECT 'EXPENSE_FAMILY_WELLBEING', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseFamilyWellbeingInfo] 
	WHERE MONTH(ExpenseDate) = @ExpenseMonth AND YEAR(ExpenseDate) = @ExpenseYear
	
	UNION ALL
	SELECT 'EXPENSE_OUTDOOR_FOOD', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseOutdoorFoodInfo] 
	WHERE MONTH(ExpenseDate) = @ExpenseMonth AND YEAR(ExpenseDate) = @ExpenseYear
	
	UNION ALL
	SELECT 'EXPENSE_PRODUCT', ISNULL(SUM(ExpenseAmount), 0) 
	FROM [Transactions].[ExpenseProductInfo] 
	WHERE MONTH(ExpenseDate) = @ExpenseMonth AND YEAR(ExpenseDate) = @ExpenseYear
	
	UNION ALL
	SELECT 'EXPENSE_TRAVEL', ISNULL(SUM(ExpenseAmount), 0)
	FROM [Transactions].[ExpenseTravelInfo] 
	WHERE MONTH(ExpenseDate) = @ExpenseMonth AND YEAR(ExpenseDate) = @ExpenseYear;

	SELECT * FROM @ExpenseInfo;


END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
