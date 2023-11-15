CREATE PROCEDURE [Transactions].[usp_ExpenseFamilyFundInfo_Get_ById]
	@ExpenseFamilyFundInfoId UNIQUEIDENTIFIER,
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

	IF NOT EXISTS(SELECT 1 FROM [Transactions].[ExpenseFamilyFundInfo] WHERE [Id] = @ExpenseFamilyFundInfoId AND [RowStatus] = 'A')
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_GROCERY_INFO_ID', 16, 1);
	END

	DECLARE @ExpenseFamilyFundInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[ExpenseDate] DATETIME2,
		[ExpenseById] UNIQUEIDENTIFIER,
		[ExpenseByName] NVARCHAR(50),
		[RecipientId] UNIQUEIDENTIFIER,
		[RecipientName] NVARCHAR(25),
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	INSERT INTO @ExpenseFamilyFundInfo([Id], [ExpenseDate], [ExpenseById], [ExpenseByName], [RecipientId], [RecipientName], [ExpenseAmount], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[EGI].[Id],
		[EGI].[ExpenseDate],
		[EGI].[ExpenseById],
		[MI].[Name],
		[EGI].[RecipientId],
		[RI].[Name],
		[EGI].[ExpenseAmount],
		[EGI].[Description],
		[EGI].[CreatedBy],
		[EGI].[CreatedDate],
		[EGI].[ModifiedBy],
		[EGI].[ModifiedDate],
		[EGI].[RowStatus]	
	FROM [Transactions].[ExpenseFamilyFundInfo] EGI 
	LEFT JOIN [Setup].[MemberInfo] MI ON EGI.[ExpenseById] = MI.[Id]
	LEFT JOIN [Setup].[MemberInfo] RI ON EGI.[RecipientId] = RI.[Id]
	WHERE [EGI].[Id] = @ExpenseFamilyFundInfoId
	AND [EGI].[CreatedBy] = @LoggedInUserId
	AND [EGI].[RowStatus] = 'A'

	SELECT * FROM @ExpenseFamilyFundInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;