CREATE PROCEDURE [Transactions].[usp_ExpenseTravelInfo_Get_ById]
	@ExpenseTravelInfoId UNIQUEIDENTIFIER,
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

	IF NOT EXISTS (SELECT * FROM [Transactions].[ExpenseTravelInfo] where [Id] = @ExpenseTravelInfoId )
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_TRAVEL_INFO_ID', 16, 1);
	END

	DECLARE @ExpenseTravelInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[ExpenseDate] DATETIME2,
		[ExpenseById] UNIQUEIDENTIFIER,
		[ExpenseByName] NVARCHAR(50),
		[Source] NVARCHAR(250),
		[Destination] NVARCHAR(250),
		[ExpenseAmount] DECIMAL(12,3),
		[TransportMode] NVARCHAR(15),
		[TransportModeValue] NVARCHAR(15),
		[TravelService] NVARCHAR(15),
		[TravelServiceValue] NVARCHAR(15),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	INSERT INTO @ExpenseTravelInfo([Id], [ExpenseDate], [ExpenseById], [ExpenseByName], [Source], [Destination], [ExpenseAmount], [TransportMode], [TransportModeValue], [TravelService], [TravelServiceValue], [Description],[CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[ETI].[Id],
		[ETI].[ExpenseDate],
		[ETI].[ExpenseById],
		[MI].[Name],
		[ETI].[Source],
		[ETI].[Destination],
		[ETI].[ExpenseAmount],
		[ETI].[TransportMode],
		[ETI].[TransportMode],
		[ETI].[TravelService],
		[ETI].[TravelService],
		[ETI].[Description],
		[ETI].[CreatedBy],
		[ETI].[CreatedDate],
		[ETI].[ModifiedBy],
		[ETI].[ModifiedDate],
		[ETI].[RowStatus]	
	FROM [Transactions].[ExpenseTravelInfo] ETI 
	LEFT JOIN [Setup].[MemberInfo] MI ON ETI.[ExpenseById] = MI.[Id]
	LEFT JOIN [Lookups].[TransportModeInfo] TMI ON ETI.[TransportMode] = TMI.[TransportMode]
	LEFT JOIN [Lookups].[TravelServiceInfo] TSI ON ETI.[TravelService] = TSI.[TravelService]
	WHERE [ETI].[Id] = @ExpenseTravelInfoId 
	AND [ETI].[CreatedBy] = @LoggedInUserId
	AND [ETI].[RowStatus] = 'A'
	ORDER BY [MI].[Name]

	SELECT * FROM @ExpenseTravelInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;