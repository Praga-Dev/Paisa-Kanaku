﻿CREATE PROCEDURE [Setup].[usp_V1_RepairTypeInfo_Save]
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

	IF (@Id = @EmptyGuid AND EXISTS(SELECT TOP 1 1 FROM [Setup].[RepairTypeInfo] WHERE [Name] LIKE @Name))
	BEGIN
		RAISERROR('INVALID_PARAM_REPAIR_TYPE_ALREADY_EXIST', 16, 1);
	END

	DECLARE @RepairTypeId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[RepairTypeInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[RepairTypeInfo] ([Id], [Name], [CreatedBy])
		VALUES (@RepairTypeId, @Name, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[RepairTypeInfo]
			SET	[Name] = @Name,
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @RepairTypeId AND [RowStatus] = 'A';
	END

	SET @Result = @RepairTypeId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;