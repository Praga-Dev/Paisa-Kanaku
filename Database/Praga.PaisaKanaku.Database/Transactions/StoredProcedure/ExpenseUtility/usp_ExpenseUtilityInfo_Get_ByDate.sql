CREATE PROCEDURE [Transactions].[usp_ExpenseUtilityInfo_Get_ByDate]
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

	DECLARE @ExpenseUtilityInfo TABLE(
	[Id] UNIQUEIDENTIFIER,
	[UtilityId] UNIQUEIDENTIFIER,
	[UtilityName] NVARCHAR(25),
	[ConsumerId] UNIQUEIDENTIFIER,
	[ConsumerName] NVARCHAR(25),
	[ConsumerType] NVARCHAR(25),
	[ConsumerTypeValue] NVARCHAR(25),
	[ServiceDuration] NVARCHAR(15),
	[ServiceDurationValue] NVARCHAR(15),
	[FromDate] DATETIME2,
	[ToDate] DATETIME2,
	[ExpenseById] UNIQUEIDENTIFIER,
	[ExpenseByName] NVARCHAR(25),
	[ExpenseDate] DATETIME2,
	[ExpenseAmount] DECIMAL(12,3),
	[Description] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER,
	[CreatedDate] DATETIME2,
	[ModifiedBy] UNIQUEIDENTIFIER,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1)
    );



    INSERT INTO @ExpenseUtilityInfo([Id], [UtilityId], [UtilityName], [ConsumerId], [ConsumerName]
    , [ConsumerType], [ConsumerTypeValue], [ServiceDuration], [ServiceDurationValue], [FromDate], [ToDate]
    , [ExpenseById], [ExpenseByName], [ExpenseDate], [ExpenseAmount], [Description]
    , [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
    SELECT 
        [EUBI].[Id],
        [EUBI].[UtilityId],
        [UI].[Name],
        CASE WHEN [EUBI].[ConsumerType] = 'INDIVIDUAL' 
        THEN 
            (SELECT [Id] FROM [Setup].[MemberInfo] 
                WHERE [Id] = (SELECT TOP 1 [MemberId] FROM [Transactions].[ExpenseMemberUtilityInfo] 
                WHERE [ExpenseUtilityId] = [EUBI].[Id]))  
        ELSE 
            (SELECT [Id] FROM [Setup].[GroupInfo] 
                WHERE [Id] = (SELECT TOP 1 [GroupId] FROM [Transactions].[ExpenseGroupUtilityInfo] 
                WHERE [ExpenseUtilityId] = [EUBI].[Id]))
        END,
        CASE WHEN [EUBI].[ConsumerType] = 'INDIVIDUAL' 
        THEN 
            (SELECT [Name] FROM [Setup].[MemberInfo] 
                WHERE [Id] = (SELECT TOP 1 [MemberId] FROM [Transactions].[ExpenseMemberUtilityInfo] 
                WHERE [ExpenseUtilityId] = [EUBI].[Id]))
        ELSE 
            (SELECT [Name] FROM [Setup].[GroupInfo] 
                WHERE [Id] = (SELECT TOP 1 [GroupId] FROM [Transactions].[ExpenseGroupUtilityInfo] 
                WHERE [ExpenseUtilityId] = [EUBI].[Id]))
        END,
        [EUBI].[ConsumerType],
        [CTI].[ConsumerTypeValue],
        [EUBI].[ServiceDuration],
        [TPTI].[TimePeriodTypeValue],
        [EUBI].[FromDate],
        [EUBI].[ToDate],
        [EUBI].[ExpenseById],
        [MI].[Name],
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
        LEFT JOIN [Lookups].[TimePeriodTypeInfo] TPTI ON EUBI.[ServiceDuration] = TPTI.[TimePeriodType]
        LEFT JOIN [Setup].[MemberInfo] MI ON EUBI.[ExpenseById] = MI.[Id]
        WHERE [EUBI].[ExpenseDate] = @ExpenseDate 
        AND [EUBI].[CreatedBy] = @LoggedInUserId
        AND [EUBI].[RowStatus] = 'A'
    ORDER BY [EUBI].[CreatedDate]

	SELECT * FROM @ExpenseUtilityInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;