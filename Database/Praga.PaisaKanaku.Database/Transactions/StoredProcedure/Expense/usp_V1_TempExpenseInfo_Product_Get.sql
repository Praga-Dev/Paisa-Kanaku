CREATE PROCEDURE [Transactions].[usp_V1_TempExpenseInfo_Product_Get]
	@ExpenseBy UNIQUEIDENTIFIER,
	@ExpenseDate DATETIME,
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
	
	SELECT * FROM [Transactions].[TempExpenseInfo] 
	WHERE [MemberId] = @ExpenseBy AND [Date] = @ExpenseDate AND [RowStatus] = 'A' AND CreatedBy = @LoggedInUserId

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;