CREATE PROCEDURE [Setup].[usp_V1_MemberInfo_Get_By_Id]
	@MemberInfoId UNIQUEIDENTIFIER,
	@LoggedInUserId UNIQUEIDENTIFIER

AS
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF([Common].[udp_v1_ValidateGuid](@MemberInfoId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_MEMBER_INFO_ID', 16, 1);
	END

	DECLARE @MemberInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[Name] NVARCHAR(25),
		[RelationshipType] NVARCHAR(25),
		[RelationshipTypeValue] NVARCHAR(25),
		[SequenceId] INT,
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);

	INSERT INTO @MemberInfo([Id], [Name], [RelationshipType], [RelationshipTypeValue]
	, [SequenceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT	
	MI.[Id], 
	MI.[Name], 
	RI.[RelationshipType], 
	RI.[RelationshipTypeValue], 
	MI.[SequenceId], 
	MI.[CreatedBy], 
	MI.[CreatedDate], 
	MI.[ModifiedBy], 
	MI.[ModifiedDate], 
	MI.[RowStatus]
	FROM [Setup].[MemberInfo] MI
	LEFT JOIN [Lookups].[RelationshipTypeInfo] RI ON MI.[RelationshipType] = RI.[RelationshipType]
	WHERE MI.[RowStatus] = 'A' AND MI.CreatedBy = @LoggedInUserId AND MI.[Id] = @MemberInfoId
	ORDER BY MI.[RelationshipType];

	SELECT * FROM @MemberInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
