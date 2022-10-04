CREATE PROCEDURE [Setup].[usp_V1_ProductDetailInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@ProductId UNIQUEIDENTIFIER,
	@ProductName NVARCHAR(50),
	@BrandId UNIQUEIDENTIFIER,
	@BrandName NVARCHAR(50),
	@ExpenseType NVARCHAR(25),
	@Price DECIMAL(12,3),
	@Description NVARCHAR(250),
	@RecurringTimePeriod NVARCHAR(10),
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

	DECLARE @TempBrandId UNIQUEIDENTIFIER;

	IF(@BrandId IS NULL OR @BrandId = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[BrandInfo] WHERE [Id] = @BrandId))
	BEGIN
		EXEC [Setup].[usp_V1_BrandInfo_Save] @BrandId, @BrandName, @LoggedInUserId, @TempBrandId OUTPUT;
	END

	DECLARE @TempProductId UNIQUEIDENTIFIER = CASE WHEN (@ProductId IS NULL OR @ProductId = @EmptyGuid) THEN NEWID() ELSE @ProductId END;

	IF(@ProductId IS NULL OR @ProductId = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[ProductInfo] WHERE [Id] = @ProductId))
	BEGIN
		EXEC [Setup].[usp_V1_ProductInfo_Save] @ProductId, @ProductName, @LoggedInUserId, @TempProductId OUTPUT;
	END

	DECLARE @TempProductDetailId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

	IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[BrandInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[ProductDetailInfo] 
		([Id], [ProductId], [BrandId], [ExpenseType], [Price], [Description], [RecurringTimePeriod], [CreatedBy])
		VALUES 
		(@TempProductDetailId, @TempProductId, @TempBrandId, @ExpenseType, @Price, @Description, @RecurringTimePeriod, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[ProductDetailInfo]
			SET	
				[ProductId] = @TempProductId,
				[BrandId] = @TempBrandId,
				[ExpenseType] = @ExpenseType,
				[Price] = @Price,
				[Description] = @Description,
				[RecurringTimePeriod] = @RecurringTimePeriod,
				[ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @TempProductDetailId AND [RowStatus] = 'A';
	END	

	SET @Result = @TempProductDetailId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;