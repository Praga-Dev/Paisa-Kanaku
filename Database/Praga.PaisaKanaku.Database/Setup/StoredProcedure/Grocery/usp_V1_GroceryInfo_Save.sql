CREATE PROCEDURE [Setup].[usp_V1_GroceryInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(25),
	@GroceryCategory NVARCHAR(25),
	@BrandId UNIQUEIDENTIFIER,
	@BrandName NVARCHAR(50),
	@PreferredRecurringTimePeriod NVARCHAR(15),
	@MetricSystem NVARCHAR(1),
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

	IF(@BrandId IS NULL OR @BrandId = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[BrandInfo] WHERE [Id] = @BrandId))
	BEGIN
		EXEC [Setup].[usp_V1_BrandInfo_Save] @BrandId, @BrandName, @LoggedInUserId, @BrandId OUTPUT;
	END

	
	IF (@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[GroceryInfo] WHERE [Id] = @Id))
	BEGIN 		
		DECLARE @TempGroceryInfoId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

		INSERT INTO [Setup].[GroceryInfo] 
		([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
		VALUES 
		(@TempGroceryInfoId, @Name, @GroceryCategory, @BrandId, @PreferredRecurringTimePeriod, @MetricSystem, @LoggedInUserId);
	END
	ELSE
	BEGIN
		UPDATE [Setup].[GroceryInfo]
			SET	
				[Name] = @Name,
				[GroceryCategory] = @GroceryCategory,
				[BrandId] = @BrandId,
				[PreferredRecurringTimePeriod] = @PreferredRecurringTimePeriod,
				[MetricSystem] = @MetricSystem,
				[ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @Id AND [RowStatus] = 'A';
	END	

	SET @Result = @TempGroceryInfoId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;