CREATE PROCEDURE [Transactions].[usp_ExpenseGroceryInfo_Get_ById]
	@ExpenseGroceryInfoId UNIQUEIDENTIFIER,
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

	IF NOT EXISTS(SELECT 1 FROM [Transactions].[ExpenseGroceryInfo] WHERE [Id] = @ExpenseGroceryInfoId AND [RowStatus] = 'A')
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_GROCERY_INFO_ID', 16, 1);
	END

	DECLARE @ExpenseGroceryInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[ExpenseDate] DATETIME2,
		[ExpenseById] UNIQUEIDENTIFIER,
		[ExpenseByName] NVARCHAR(50),
		[GroceryInfoId] UNIQUEIDENTIFIER,
		[GroceryInfoName] NVARCHAR(25),
		[Quantity] DECIMAL(8,3),
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	-- Expense Items
	INSERT INTO @ExpenseGroceryInfo([Id], [ExpenseDate], [ExpenseById], [ExpenseByName], [GroceryInfoId], [GroceryInfoName], [Quantity], [ExpenseAmount], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[EGI].[Id],
		[EGI].[ExpenseDate],
		[EGI].[ExpenseById],
		[MI].[Name],
		[EGI].[GroceryInfoId],
		[GID].[Name],
		[EGI].[Quantity],
		[EGI].[ExpenseAmount],
		[EGI].[Description],
		[EGI].[CreatedBy],
		[EGI].[CreatedDate],
		[EGI].[ModifiedBy],
		[EGI].[ModifiedDate],
		[EGI].[RowStatus]	
	FROM [Transactions].[ExpenseGroceryInfo] EGI 
	LEFT JOIN [Setup].[MemberInfo] MI ON EGI.[ExpenseById] = MI.[Id]
	LEFT JOIN [Setup].[GroceryInfo] GID ON EGI.[GroceryInfoId] = GID.[Id]
	WHERE [EGI].[Id] = @ExpenseGroceryInfoId
	AND [EGI].[CreatedBy] = @LoggedInUserId
	AND [EGI].[RowStatus] = 'A'

	SELECT * FROM @ExpenseGroceryInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;