﻿CREATE PROCEDURE [Setup].[usp_V1_ProductInfo_Get]
@LoggedInUserId UNIQUEIDENTIFIER

AS
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @ProductInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[Name] NVARCHAR(25),
		[ProductCategory] NVARCHAR(25),
		[ProductCategoryValue] NVARCHAR(25),
		[BrandId] UNIQUEIDENTIFIER,
		[BrandName] NVARCHAR(50),
		[Price] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[PreferredRecurringTimePeriod] NVARCHAR(15),
		[PreferredRecurringTimePeriodValue] NVARCHAR(15),
		[SequenceId] INT,
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);

	INSERT INTO @ProductInfo([Id], [Name], [ProductCategory], [ProductCategoryValue], [BrandId], [BrandName],
	[Price], [Description], [PreferredRecurringTimePeriod], [PreferredRecurringTimePeriodValue], [SequenceId], [CreatedBy], [CreatedDate], 
	[ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT	
	PI.[Id], 
	PI.[Name], 
	PCI.[ProductCategory], 
	PCI.[ProductCategoryValue], 
	BI.[Id] AS [BrandId], 
	BI.[Name] AS [BrandName], 
	PPI.[Price], 
	PI.[Description], 
	TPTI.[TimePeriodType] AS [PreferredRecurringTimePeriod], 
	TPTI.[TimePeriodTypeValue] AS [PreferredRecurringTimePeriodValue], 
	PI.[SequenceId], 
	PI.[CreatedBy], 
	PI.[CreatedDate], 
	PI.[ModifiedBy], 
	PI.[ModifiedDate], 
	PI.[RowStatus]
	FROM [Setup].[ProductInfo] PI
	LEFT JOIN [Setup].[BrandInfo] BI ON PI.[BrandId] = BI.[Id]
	LEFT JOIN [Setup].[ProductPriceInfo] PPI ON PI.[Id] = PPI.[ProductInfoId]
	LEFT JOIN [Lookups].[ProductCategoryInfo] PCI ON PI.[ProductCategory] = PCI.[ProductCategory]
	LEFT JOIN [Lookups].[TimePeriodTypeInfo] TPTI ON PI.[PreferredRecurringTimePeriod] = TPTI.[TimePeriodType]
	WHERE PI.[RowStatus] = 'A' AND PPI.RowStatus = 'A' AND PI.CreatedBy = @LoggedInUserId
	ORDER BY PI.[Name];

	SELECT * FROM @ProductInfo ORDER BY [Name];

	RETURN 0
END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
