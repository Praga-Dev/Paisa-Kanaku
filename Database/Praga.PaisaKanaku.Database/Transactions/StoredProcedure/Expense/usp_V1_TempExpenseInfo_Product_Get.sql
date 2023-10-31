CREATE PROCEDURE [Transactions].[usp_V1_TempExpenseInfo_Product_Get]
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

	DECLARE @TempExpenseInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[MemberId] UNIQUEIDENTIFIER,
		[MemberName] NVARCHAR(25),
		[Date] DATETIME2,
		[ProductId] UNIQUEIDENTIFIER,
		[Quantity] INT,
		[Amount] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	-- Temp Items
	INSERT INTO @TempExpenseInfo([Id], [MemberId], [MemberName], [Date], [ProductId], [Quantity], [Amount], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[TEI].[Id],
		[TEI].[MemberId],
		[MI].[Name] AS [MemberName],
		[TEI].[Date],
		[TEI].[ProductId],
		[TEI].[Quantity],
		[TEI].[Amount],
		[TEI].[Description],
		[TEI].[CreatedBy],
		[TEI].[CreatedDate],
		[TEI].[ModifiedBy],
		[TEI].[ModifiedDate],
		[TEI].[RowStatus]	
	FROM [Transactions].[TempExpenseInfo] TEI 
	LEFT JOIN [Setup].[MemberInfo] MI ON TEI.[MemberId] = MI.[Id]
	WHERE [TEI].[Date] = @ExpenseDate AND [TEI].[CreatedBy] = @LoggedInUserId
	AND [TEI].[RowStatus] = 'A'


	-- Expense Items
	INSERT INTO @TempExpenseInfo([Id], [MemberId], [MemberName], [Date], [ProductId], [Quantity], [Amount], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[ERDI].[Id],
		[ERDI].[ExpenseById],
		[MI].[Name] AS [MemberName],
		[ERDI].[ExpenseDate],
		[ERDI].[ReferenceId],
		[ERDI].[Quantity],
		[ERDI].[ExpenseAmount],
		[ERDI].[Description],
		[ERDI].[CreatedBy],
		[ERDI].[CreatedDate],
		[ERDI].[ModifiedBy],
		[ERDI].[ModifiedDate],
		[ERDI].[RowStatus]	
	FROM [Transactions].[ExpenseReferenceDetailInfo] ERDI 
	LEFT JOIN [Setup].[MemberInfo] MI ON ERDI.[ExpenseById] = MI.[Id]
	WHERE [ERDI].[ExpenseDate] = @ExpenseDate AND [ERDI].[CreatedBy] = @LoggedInUserId
	AND [ERDI].[RowStatus] = 'A'

	SELECT * FROM @TempExpenseInfo ORDER BY [MemberName];

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;