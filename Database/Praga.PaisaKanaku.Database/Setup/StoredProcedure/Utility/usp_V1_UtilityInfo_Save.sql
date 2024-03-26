CREATE PROCEDURE [Setup].[usp_V1_UtilityInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@ConsumerType NVARCHAR(25),
	@RecurringType NVARCHAR(15),
	@IsEssential BIT,
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

	IF (@Id = @EmptyGuid AND EXISTS(SELECT TOP 1 1 FROM [Setup].[UtilityInfo] WHERE [Name] LIKE @Name))
	BEGIN
		RAISERROR('INVALID_PARAM_MEMBER_ALREADY_EXIST', 16, 1);
	END

	DECLARE @UtilityId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[UtilityInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[UtilityInfo] ( [Id], [Name], [ConsumerType], [RecurringType], [IsEssential], [CreatedBy])
		VALUES (@UtilityId, @Name, @ConsumerType, @RecurringType, @IsEssential, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[UtilityInfo]
			SET	[Name] = @Name,
				[ConsumerType] = @ConsumerType,
				[RecurringType] = @RecurringType,
				[IsEssential] = @IsEssential,	
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @UtilityId AND [RowStatus] = 'A';
	END

	SET @Result = @UtilityId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;