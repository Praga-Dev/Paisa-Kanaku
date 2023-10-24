CREATE PROCEDURE [Transactions].[usp_V1_TempExpenseInfo_Delete]
	@TempExpenseInfoId UNIQUEIDENTIFIER,
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
	
	IF([Common].[udp_v1_ValidateGuid](@TempExpenseInfoId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_TEMP_EXPENSE_INFO_ID', 16, 1);
	END

	IF(NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[TempExpenseInfo] WHERE [Id] = @TempExpenseInfoId))
	BEGIN
		RAISERROR('INVALID_PARAM_TEMP_EXPENSE_INFO_ID_NOT_AVAILABLE', 16, 1);
	END

	UPDATE [Transactions].[TempExpenseInfo] SET [RowStatus] = 'D' WHERE [Id] = @TempExpenseInfoId

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;