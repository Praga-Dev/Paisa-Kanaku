CREATE PROCEDURE [Setup].[usp_V1_ProductInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(25),
	@ProductCategory NVARCHAR(25),
	@BrandId UNIQUEIDENTIFIER,
	@BrandName NVARCHAR(50),
	@ExpenseType NVARCHAR(25),
	@Price DECIMAL(12,3),
	@Description NVARCHAR(250),
	@PreferredRecurringTimePeriod NVARCHAR(10),
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

	
	IF (@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[ProductInfo] WHERE [Id] = @Id))
	BEGIN 		
		DECLARE @TempProductInfoId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

		INSERT INTO [Setup].[ProductInfo] 
		([Id], [Name], [ProductCategory], [BrandId], [ExpenseType], [Price], [Description], [PreferredRecurringTimePeriod], [CreatedBy])
		VALUES 
		(@TempProductInfoId, @Name, @ProductCategory, @BrandId, @ExpenseType, @Price, @Description, @PreferredRecurringTimePeriod, @LoggedInUserId);
	END
	ELSE
	BEGIN
		UPDATE [Setup].[ProductInfo]
			SET	
				[Name] = @Name,
				[ProductCategory] = @ProductCategory,
				[BrandId] = @BrandId,
				[ExpenseType] = @ExpenseType,
				[Price] = @Price,
				[Description] = @Description,
				[PreferredRecurringTimePeriod] = @PreferredRecurringTimePeriod,
				[ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @Id AND [RowStatus] = 'A';
	END	

	SET @Result = @TempProductInfoId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;