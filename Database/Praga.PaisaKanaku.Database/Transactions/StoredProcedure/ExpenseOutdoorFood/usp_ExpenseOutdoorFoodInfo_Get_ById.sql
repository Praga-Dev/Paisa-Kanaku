﻿CREATE PROCEDURE [Transactions].[usp_ExpenseOutdoorFoodInfo_Get_ById]
	@ExpenseOutdoorFoodInfoId UNIQUEIDENTIFIER,
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

	DECLARE @ExpenseOutdoorFoodInfo TABLE(
		[Id] UNIQUEIDENTIFIER,
		[ExpenseDate] DATETIME2,
		[ExpenseById] UNIQUEIDENTIFIER,
		[ExpenseByName] NVARCHAR(50),
		[OutdoorFoodVendorId] UNIQUEIDENTIFIER,
		[OutdoorFoodVendorName] NVARCHAR(25),
		[ExpenseAmount] DECIMAL(12,3),
		[BillImageUrl] NVARCHAR(250),
		[Description] NVARCHAR(250),
		[CreatedBy] UNIQUEIDENTIFIER,
		[CreatedDate] DATETIME2,
		[ModifiedBy] UNIQUEIDENTIFIER,
		[ModifiedDate] DATETIME2,
		[RowStatus] NVARCHAR(1)
	);
	
	INSERT INTO @ExpenseOutdoorFoodInfo([Id], [ExpenseDate], [ExpenseById], [ExpenseByName], [OutdoorFoodVendorId], [OutdoorFoodVendorName], [ExpenseAmount], [BillImageUrl], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [RowStatus])
	SELECT 
		[EOFI].[Id],
		[EOFI].[ExpenseDate],
		[EOFI].[ExpenseById],
		[MI].[Name],
		[EOFI].[OutdoorFoodVendorId],
		[OFVI].[Name],
		[EOFI].[ExpenseAmount],
		[EOFI].[BillImageUrl],
		[EOFI].[Description],
		[EOFI].[CreatedBy],
		[EOFI].[CreatedDate],
		[EOFI].[ModifiedBy],
		[EOFI].[ModifiedDate],
		[EOFI].[RowStatus]	
	FROM [Transactions].[ExpenseOutdoorFoodInfo] EOFI 
	LEFT JOIN [Setup].[MemberInfo] MI ON EOFI.[ExpenseById] = MI.[Id]
	LEFT JOIN [Setup].[OutdoorFoodVendorInfo] OFVI ON EOFI.[OutdoorFoodVendorId] = OFVI.[Id]
	WHERE [EOFI].[Id] = @ExpenseOutdoorFoodInfoId 
	AND [EOFI].[CreatedBy] = @LoggedInUserId
	AND [EOFI].[RowStatus] = 'A'
	ORDER BY [MI].[Name]

	SELECT * FROM @ExpenseOutdoorFoodInfo;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;