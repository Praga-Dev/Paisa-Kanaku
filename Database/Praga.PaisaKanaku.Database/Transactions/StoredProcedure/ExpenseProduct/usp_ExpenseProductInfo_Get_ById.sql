CREATE PROCEDURE [Transactions].[usp_ExpenseInfo_Product_Get_ById]
	@ExpenseProductInfoId UNIQUEIDENTIFIER,
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

	IF NOT EXISTS(SELECT 1 FROM [Transactions].[ExpenseProductInfo] WHERE [Id] = @ExpenseProductInfoId AND [RowStatus] = 'A')
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_PRODUCT_INFO_ID', 16, 1);
	END

	DECLARE @ExpenseProductInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[MemberId] UNIQUEIDENTIFIER,
		[MemberName] NVARCHAR(25),
		[ProductInfoId] UNIQUEIDENTIFIER,
		[ProductInfoName] NVARCHAR(25),
		[Quantity] INT,
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	INSERT INTO @ExpenseProductInfo([Id], [MemberId], [MemberName], [ProductInfoId], [ProductInfoName], [Quantity], [ExpenseAmount], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[EPI].[Id],
		[EPI].[ExpenseById],
		[MI].[Name] AS [ExpenseByName],
		[EPI].[ProductInfoId],
		[PDTI].[Name],
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
	WHERE [EPI].[Id] = @ExpenseProductInfoId
	AND [EPI].[CreatedBy] = @LoggedInUserId
	AND [EPI].[RowStatus] = 'A'

	SELECT * FROM @ExpenseProductInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;