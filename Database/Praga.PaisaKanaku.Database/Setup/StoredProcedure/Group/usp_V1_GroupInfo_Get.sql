CREATE PROCEDURE [Setup].[usp_V1_GroupInfo_Get]
	@LoggedInUserId UNIQUEIDENTIFIER

AS
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @GroupInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[Name] NVARCHAR(25),
		[SequenceId] INT,
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);

	INSERT INTO @GroupInfo([Id], [Name], [SequenceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT	
	GI.[Id], 
	GI.[Name], 
	GI.[SequenceId], 
	GI.[CreatedBy], 
	GI.[CreatedDate], 
	GI.[ModifiedBy], 
	GI.[ModifiedDate], 
	GI.[RowStatus]
	FROM [Setup].[GroupInfo] GI
	WHERE GI.[RowStatus] = 'A' AND GI.CreatedBy = @LoggedInUserId;

	SELECT * FROM @GroupInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
