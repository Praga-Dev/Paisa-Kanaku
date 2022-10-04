CREATE PROCEDURE [Setup].[usp_V1_ProductInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
set @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @ProductId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[ProductInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[ProductInfo] ([Id], [Name], [CreatedBy])
		VALUES (@ProductId, @Name, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[ProductInfo]
			SET	[Name] = @Name,
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @ProductId AND [RowStatus] = 'A';
	END

	SET @Result = @ProductId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;