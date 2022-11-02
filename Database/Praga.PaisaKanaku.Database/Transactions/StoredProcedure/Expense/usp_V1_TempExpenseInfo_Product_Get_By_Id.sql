CREATE PROCEDURE [Transactions].[usp_V1_TempExpenseInfo_Product_Get_By_Id]
	@TempExpenseId UNIQUEIDENTIFIER,
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
	
	IF([Common].[udp_v1_ValidateGuid](@TempExpenseId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_TEMP_EXPENSE_INFO_ID', 16, 1);
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
		[SequenceId] INT,
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	INSERT INTO @TempExpenseInfo([Id], [MemberId], [MemberName], [Date], [ProductId], [Quantity], [Amount], [Description], [SequenceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[TEI].[Id],
		[TEI].[MemberId],
		[MI].[Name] AS [MemberName],
		[TEI].[Date],
		[TEI].[ProductId],
		[TEI].[Quantity],
		[TEI].[Amount],
		[TEI].[Description],
		[TEI].[SequenceId],
		[TEI].[CreatedBy],
		[TEI].[CreatedDate],
		[TEI].[ModifiedBy],
		[TEI].[ModifiedDate],
		[TEI].[RowStatus]	
	FROM [Transactions].[TempExpenseInfo] TEI 
	LEFT JOIN [Setup].[MemberInfo] MI ON TEI.[MemberId] = MI.[Id]
	WHERE [TEI].[Id] = @TempExpenseId AND [TEI].[RowStatus] = 'A' AND [TEI].[CreatedBy] = @LoggedInUserId

	SELECT * FROM @TempExpenseInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;