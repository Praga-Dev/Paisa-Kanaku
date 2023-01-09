CREATE PROCEDURE [Setup].[usp_V1_ProductInfo_BulkSave]
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	DECLARE @ProductCount INT = 1;
 
	WHILE @ProductCount < 1000
	BEGIN
		DECLARE @Name VARCHAR(25);
		SELECT @Name = CONVERT(VARCHAR(255), NEWID())

		DECLARE @Price DECIMAL(12,3)
		SELECT @Price = ROUND(RAND() * 10000, 3);
		
		DECLARE @BrandId UNIQUEIDENTIFIER;
		SELECT TOP 1 @BrandId = Id FROM [Setup].[BrandInfo] ORDER BY NEWID()
		
		--DECLARE @MemberId UNIQUEIDENTIFIER;
		--SELECT TOP 1 @MemberId= Id FROM [Setup].[MemberInfo] ORDER BY NEWID()

		DECLARE @ProductCategoryId UNIQUEIDENTIFIER;
		SELECT TOP 1 @ProductCategoryId= Id FROM [Setup].[ProductCategoryInfo] ORDER BY NEWID()

		EXEC [Setup].[usp_V1_ProductInfo_Save] @EmptyGuid, @Name, @ProductCategoryId, NULL, @BrandId, NULL, 
		'BUSINESS_NEEDS', @Price, @Name, 'BIMONTHLY', @LoggedInUserId, @BrandId OUTPUT;
		SET @ProductCount = @ProductCount + 1;
	END;

	SET @Result = @EmptyGuid;
	RETURN @Response;
END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;