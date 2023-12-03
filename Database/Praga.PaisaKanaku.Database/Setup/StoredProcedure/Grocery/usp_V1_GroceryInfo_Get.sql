CREATE PROCEDURE [Setup].[usp_V1_GroceryInfo_Get]
@LoggedInUserId UNIQUEIDENTIFIER

AS
DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @GroceryInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[Name] NVARCHAR(25),
		[GroceryCategory] NVARCHAR(25),
		[GroceryCategoryValue] NVARCHAR(25),
		[BrandId] UNIQUEIDENTIFIER,
		[BrandName] NVARCHAR(50),
		[PreferredRecurringTimePeriod] NVARCHAR(15),
		[PreferredRecurringTimePeriodValue] NVARCHAR(15),
		[MetricSystem] NVARCHAR(1),
		[MetricSystemValue] NVARCHAR(25),
		[SequenceId] INT,
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);

	INSERT INTO @GroceryInfo([Id], [Name], [GroceryCategory], [GroceryCategoryValue], [BrandId], [BrandName], 
	[PreferredRecurringTimePeriod], [PreferredRecurringTimePeriodValue], [MetricSystem], [MetricSystemValue],
	[SequenceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT	
	GI.[Id], 
	GI.[Name], 
	PCI.[GroceryCategory], 
	PCI.[GroceryCategoryValue], 
	BI.[Id] AS [BrandId], 
	BI.[Name] AS [BrandName], 
	TPTI.[TimePeriodType] AS [PreferredRecurringTimePeriod], 
	TPTI.[TimePeriodTypeValue] AS [PreferredRecurringTimePeriodValue], 
	MSI.[MetricSystem],
	MSI.[MetricSystemValue],
	GI.[SequenceId], 
	GI.[CreatedBy], 
	GI.[CreatedDate], 
	GI.[ModifiedBy], 
	GI.[ModifiedDate], 
	GI.[RowStatus]
	FROM [Setup].[GroceryInfo] GI
	LEFT JOIN [Setup].[BrandInfo] BI ON GI.[BrandId] = BI.[Id]
	LEFT JOIN [Lookups].[GroceryCategoryInfo] PCI ON GI.[GroceryCategory] = PCI.[GroceryCategory]
	LEFT JOIN [Lookups].[MetricSystemInfo] MSI ON GI.[MetricSystem] = MSI.[MetricSystem]
	LEFT JOIN [Lookups].[TimePeriodTypeInfo] TPTI ON GI.[PreferredRecurringTimePeriod] = TPTI.[TimePeriodType]
	WHERE GI.[RowStatus] = 'A' AND GI.CreatedBy = @LoggedInUserId;

	SELECT * FROM @GroceryInfo ORDER BY [Name];

	RETURN 0
END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
