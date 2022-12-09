CREATE PROCEDURE [Setup].[usp_V1_BillTypeInfo_Get_By_Id]
	@BillTypeInfoId UNIQUEIDENTIFIER,
	@LoggedInUserId UNIQUEIDENTIFIER

AS
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	SELECT * FROM [Setup].[BillTypeInfo] WHERE [Id] = @BillTypeInfoId AND [RowStatus] = 'A' AND [CreatedBy] = @LoggedInUserId;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
