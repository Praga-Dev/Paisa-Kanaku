CREATE PROCEDURE [Auth].[usp_v1_User_Info_Get]
	@Id UNIQUEIDENTIFIER  = NULL,
	@LoggedInUserId UNIQUEIDENTIFIER,
	@TotalCount INT OUTPUT
AS

-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	SET @TotalCount = 0;
		
		SELECT  UI.[Id]
			  ,UI.[SequenceId]
			  ,UI.[UserName]
			  ,UI.[FirstName]
			  ,UI.[LastName]
			  ,UI.[CreatedBy] AS [CreatedById]
			  ,UI.[CreatedDate]
			  ,UI.[ModifiedBy] AS [ModifiedById]
			  ,UI.[ModifiedDate]
			  ,UI.[RowStatus]
		  FROM [Auth].[UserInfo] UI
		  WHERE [RowStatus] = N'A'

		SET @TotalCount = (SELECT COUNT(*) FROM [Auth].[UserInfo] WHERE [RowStatus] = N'A');

	RETURN 0
END TRY
BEGIN CATCH
	DECLARE @ErrorNumber INT = ERROR_NUMBER();
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE() 
	  
	-- Raise Exception
	RAISERROR('%s', 16, 1, @ErrorMessage)
END CATCH
