﻿CREATE PROCEDURE [dbo].[usp_v1_MetricSystemInfo_Get]
	@LoggedInUserId UNIQUEIDENTIFIER
AS

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	SELECT * FROM [Lookups].[MetricSystemInfo]
	WHERE [RowStatus] = 'A'
	ORDER BY [SequenceId] ASC;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;