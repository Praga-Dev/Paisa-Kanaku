CREATE PROCEDURE [Lookups].[usp_v1_MeasureTypeInfo_By_GroceryInfoId_Get]
	@GroceryInfoId UNIQUEIDENTIFIER,
	@LoggedInUserId UNIQUEIDENTIFIER
AS

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[GroceryInfo] WHERE Id = @GroceryInfoId)
	BEGIN
		RAISERROR('INVALID_PARAM_GROCERY_INFO_ID', 16, 1);
	END

	DECLARE @MetricSystem NVARCHAR(1);
	SELECT @MetricSystem = [MetricSystem] FROM [Setup].[GroceryInfo] WHERE Id = @GroceryInfoId;

	SELECT * FROM [Lookups].[MeasureTypeInfo]
	WHERE [MetricSystem] = @MetricSystem AND [RowStatus] = 'A'
	ORDER BY [SequenceId] ASC;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
