CREATE PROCEDURE [Setup].[usp_ProductPriceInfo_Register]
	@ProductInfoId UNIQUEIDENTIFIER,
	@Price DECIMAL(12,3),
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

	IF(@ProductInfoId IS NULL OR @ProductInfoId = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[ProductInfo] WHERE [Id] = @ProductInfoId))
	BEGIN
		RAISERROR('INVALID_PARAM_PRODUCT_INFO_ID_IN_USP_PRODUCT_PRICE_INFO_REGISTER', 16, 1);		
	END

	IF(@Price <= 0)
	BEGIN
		RAISERROR('INVALID_PARAM_PRICE', 16, 1);
	END

	DECLARE @OldPrice DECIMAL(12,3);

	SELECT TOP 1 @OldPrice = [Price] FROM [Setup].[ProductPriceInfo]
	WHERE [ProductInfoId] = @ProductInfoId AND [RowStatus] = 'A'
	ORDER BY [SequenceId] DESC

	IF(@OldPrice IS NULL OR @OldPrice != @Price)
	BEGIN 		
		
		UPDATE [Setup].[ProductPriceInfo] SET [RowStatus] = 'I' WHERE [ProductInfoId] = @ProductInfoId		

		DECLARE @TempProductPriceInfoId UNIQUEIDENTIFIER = NEWID();

		INSERT INTO [Setup].[ProductPriceInfo] 
		([Id], [ProductInfoId], [Price], [CreatedBy])
		VALUES 
		(@TempProductPriceInfoId, @ProductInfoId, @Price, @LoggedInUserId);
	END

	SET @Result = @TempProductPriceInfoId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;