CREATE PROCEDURE [Transactions].[usp_ExpenseUtilityInfo_Get_ById]
	@ExpenseUtilityInfoId UNIQUEIDENTIFIER,
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

	IF NOT EXISTS (SELECT * FROM [Transactions].[ExpenseUtilityInfo] where [Id] = @ExpenseUtilityInfoId )
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_Utility_INFO_ID', 16, 1);
	END
	
	DECLARE @ExpenseUtilityInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[UtilityId] UNIQUEIDENTIFIER,
		[ConsumerType] NVARCHAR(25),
		[ConsumerTypeValue] NVARCHAR(25),
		[ServiceDuration] NVARCHAR(15),
		[FromDate] DATETIME2,
		[ToDate] DATETIME2,
		[ExpenseById] UNIQUEIDENTIFIER,
		[ExpenseDate] DATETIME2,
		[ExpenseAmount] DECIMAL(12,3),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	INSERT INTO @ExpenseUtilityInfo([Id], [UtilityId], [ConsumerType],
	[ConsumerTypeValue], [ServiceDuration], [FromDate], [ToDate], [ExpenseById],
	[ExpenseDate], [ExpenseAmount], [Description], [CreatedBy], [CreatedDate],
	[ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[EUBI].[Id],
		[EUBI].[UtilityId],
		[CTI].[ConsumerType],
		[CTI].[ConsumerTypeValue],
		[TPTI].[TimePeriodType],
		[EUBI].[FromDate],
		[EUBI].[ToDate],
		[EUBI].[ExpenseById],
		[EUBI].[ExpenseDate],
		[EUBI].[ExpenseAmount],
		[EUBI].[Description],
		[EUBI].[CreatedBy],
		[EUBI].[CreatedDate],
		[EUBI].[ModifiedBy],
		[EUBI].[ModifiedDate],
		[EUBI].[RowStatus]	
	FROM [Transactions].[ExpenseUtilityBaseInfo] EUBI 
	LEFT JOIN [Setup].[UtilityInfo] UI ON EUBI.[UtilityId] = UI.[Id]
	LEFT JOIN [Lookups].[ConsumerTypeInfo] CTI ON EUBI.[ConsumerType] = CTI.[ConsumerType]
	LEFT JOIN [Lookups].[TimePeriodTypeInfo] TPTI ON TPTI.[TimePeriodType] = EUBI.[ServiceDuration]
	WHERE [EUBI].[Id] = @ExpenseUtilityInfoId 
	AND [EUBI].[CreatedBy] = @LoggedInUserId
	AND [EUBI].[RowStatus] = 'A'
	ORDER BY [EUBI].[UtilityId]

	SELECT * FROM @ExpenseUtilityInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;