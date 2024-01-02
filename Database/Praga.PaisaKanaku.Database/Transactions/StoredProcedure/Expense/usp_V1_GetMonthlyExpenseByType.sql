CREATE PROCEDURE [Transactions].[usp_V1_GetMonthlyExpenseByType]
	@Month INT,
	@Year INT,
	@LoggedInUserId UNIQUEIDENTIFIER
AS
DECLARE @Response INT = 0;

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @TempExpenseInfo TABLE(
		[TotalAmount] DECIMAL(12,3),
		[ExpenseType] NVARCHAR(50)
	);
	
	-- Expense Items
	INSERT INTO @TempExpenseInfo
	SELECT SUM([ExpenseAmount]) as [TotalAmount], 'PRODUCT' as [ExpenseType]
	FROM [Transactions].[ExpenseProductInfo]
	WHERE MONTH([ExpenseDate]) = @Month AND YEAR([ExpenseDate]) = @Year
	AND [RowStatus] = 'A'
	
	INSERT INTO @TempExpenseInfo
	SELECT SUM([ExpenseAmount]) as [TotalAmount], 'GROCERY' as [ExpenseType]
	FROM [Transactions].ExpenseGroceryInfo
	WHERE MONTH([ExpenseDate]) = @Month AND YEAR([ExpenseDate]) = @Year
	AND [RowStatus] = 'A'

	INSERT INTO @TempExpenseInfo
	SELECT ISNULL( SUM([ExpenseAmount]),0) as [TotalAmount], 'FAMILY_WELLBEING' as [ExpenseType]
	FROM [Transactions].ExpenseFamilyWellbeingInfo
	WHERE MONTH([ExpenseDate]) = @Month AND YEAR([ExpenseDate]) = @Year
	AND [RowStatus] = 'A'

	SELECT * FROM @TempExpenseInfo ORDER BY [ExpenseType];

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
