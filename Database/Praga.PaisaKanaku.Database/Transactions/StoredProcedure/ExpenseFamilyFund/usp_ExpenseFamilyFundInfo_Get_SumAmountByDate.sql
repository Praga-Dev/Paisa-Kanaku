CREATE PROCEDURE [Transactions].[usp_ExpenseFamilyWellbeingInfo_Get_SumAmountByDate]
	@Month INT,
	@Year INT,
	@LoggedInUserId UNIQUEIDENTIFIER
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF([Common].[udp_v1_ValidateMonth](@Month) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_MONTH', 16, 1);
	END

	IF([Common].[udp_v1_ValidateYear](@Year) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_YEAR', 16, 1);
	END

	DECLARE @ExpenseFamilyWellbeingInfo TABLE(
		[ExpenseDate] DATETIME2,
		[TotalExpenseAmount] DECIMAL(12,3)
	);
	
	INSERT INTO @ExpenseFamilyWellbeingInfo([ExpenseDate], [TotalExpenseAmount])
	SELECT [ExpenseDate], SUM([ExpenseAmount])
	FROM [Transactions].[ExpenseFamilyWellbeingInfo]
	WHERE MONTH([ExpenseDate]) = @Month AND YEAR([ExpenseDate]) = @Year
		AND [CreatedBy] = @LoggedInUserId
		AND [RowStatus] = 'A'
	GROUP BY [ExpenseDate];

	SELECT * FROM @ExpenseFamilyWellbeingInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;