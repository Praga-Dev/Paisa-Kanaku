CREATE PROCEDURE [Transactions].[usp_ExpenseInfo_Product_Get_ByDate]
	@ExpenseDate DATETIME,
	@LoggedInUserId UNIQUEIDENTIFIER
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	DECLARE @ExpenseProductInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[ExpenseDate] DATETIME2,
		[ExpenseById] UNIQUEIDENTIFIER,
		[ExpenseByName] NVARCHAR(50),
		[ProductInfoId] UNIQUEIDENTIFIER,
		[ProductInfoName] NVARCHAR(25),
		[ProductPrice] DECIMAL(12,3),
		[Quantity] INT,
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	-- Expense Items
	INSERT INTO @ExpenseProductInfo([Id], [ExpenseDate], [ExpenseById], [ExpenseByName], [ProductInfoId], [ProductInfoName], [ProductPrice], [Quantity], [ExpenseAmount], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[EPI].[Id],
		[EPI].[ExpenseDate],
		[EPI].[ExpenseById],
		[MI].[Name] AS [ExpenseByName],
		[EPI].[ProductInfoId],
		[PDTI].[Name],
		[EPI].[ProductPrice],
		[EPI].[Quantity],
		[EPI].[ExpenseAmount],
		[EPI].[Description],
		[EPI].[CreatedBy],
		[EPI].[CreatedDate],
		[EPI].[ModifiedBy],
		[EPI].[ModifiedDate],
		[EPI].[RowStatus]	
	FROM [Transactions].[ExpenseProductInfo] EPI 
	LEFT JOIN [Setup].[MemberInfo] MI ON EPI.[ExpenseById] = MI.[Id]
	LEFT JOIN [Setup].[ProductInfo] PDTI ON EPI.[ProductInfoId] = PDTI.[Id]
	WHERE [EPI].[ExpenseDate] = @ExpenseDate 
	AND [EPI].[CreatedBy] = @LoggedInUserId
	AND [EPI].[RowStatus] = 'A'
	ORDER BY [MI].[Name]

	SELECT * FROM @ExpenseProductInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;