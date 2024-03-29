﻿CREATE PROCEDURE [Setup].[usp_OutdoorFoodVendorInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
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

	IF (@Id = @EmptyGuid AND EXISTS(SELECT TOP 1 1 FROM [Setup].[OutdoorFoodVendorInfo] WHERE [Name] LIKE @Name))
	BEGIN
		RAISERROR('INVALID_PARAM_OUTDOOR_FOOD_ALREADY_EXIST', 16, 1);
	END


    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[OutdoorFoodVendorInfo] WHERE [Id] = @Id))
	BEGIN
		DECLARE @OutdoorFoodId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

		INSERT INTO [Setup].[OutdoorFoodVendorInfo] ([Id], [Name], [CreatedBy])
		VALUES (@OutdoorFoodId, @Name, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[OutdoorFoodVendorInfo]
			SET	[Name] = @Name,
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @OutdoorFoodId AND [RowStatus] = 'A';
	END

	SET @Result = @OutdoorFoodId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;