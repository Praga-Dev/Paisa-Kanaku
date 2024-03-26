CREATE PROCEDURE [Setup].[usp_V1_UtilityInfo_Get_By_Id]
	@UtilityInfoId UNIQUEIDENTIFIER,
	@LoggedInUserId UNIQUEIDENTIFIER

AS
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF([Common].[udp_v1_ValidateGuid](@UtilityInfoId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_MEMBER_INFO_ID', 16, 1);
	END

	DECLARE @UtilityInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[Name] NVARCHAR(50),
		[ConsumerType] NVARCHAR(25),
		[ConsumerTypeValue] NVARCHAR(25),
		[RecurringType] NVARCHAR(15),
		[RecurringTypeValue] NVARCHAR(15),
		[IsEssential] BIT,
		[SequenceId] INT,
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);

	INSERT INTO @UtilityInfo([Id], [Name], [ConsumerType], [ConsumerTypeValue], [RecurringType], [RecurringTypeValue],
	[IsEssential], [SequenceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT	
	UI.[Id], 
	UI.[Name], 
	CI.[ConsumerType], 
	CI.[ConsumerTypeValue], 
	UI.[RecurringType],
	TPTI.[TimePeriodType],
	UI.[IsEssential],
	UI.[SequenceId], 
	UI.[CreatedBy], 
	UI.[CreatedDate], 
	UI.[ModifiedBy], 
	UI.[ModifiedDate], 
	UI.[RowStatus]
	FROM [Setup].[UtilityInfo] UI
	LEFT JOIN [Lookups].[ConsumerTypeInfo] CI ON UI.[ConsumerType] = CI.[ConsumerType]
	LEFT JOIN [Lookups].[TimePeriodTypeInfo] TPTI ON TPTI.[TimePeriodType] = UI.[RecurringType]
	WHERE UI.[RowStatus] = 'A' AND UI.CreatedBy = @LoggedInUserId AND UI.[Id] = @UtilityInfoId
	ORDER BY UI.[ConsumerType];

	
	SELECT * FROM @UtilityInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
