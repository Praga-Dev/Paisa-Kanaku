CREATE PROCEDURE [Common].[usp_v1_RowStatus_Update]
	@Id UNIQUEIDENTIFIER,
	@TableSchema NVARCHAR(50),
	@TableName NVARCHAR(255),
	@RowStatus NVARCHAR(1),
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;      
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END
	
	IF(@Id IS NULL OR @Id = @EmptyGuid)
	BEGIN
		RAISERROR('INVALID_PARAM_ID', 16, 1);
	END

	IF(@TableName IS NULL OR @TableName = '')
	BEGIN
		RAISERROR('INVALID_PARAM_TABLE_NAME', 16, 1);
	END

	IF(@RowStatus IS NULL OR @RowStatus = '')
	BEGIN
		RAISERROR('INVALID_PARAM_ROW_STATUS', 16, 1);
	END

	IF EXISTS (SELECT 1 FROM [INFORMATION_SCHEMA].[TABLES] T WHERE T.TABLE_SCHEMA = @TableSchema AND T.TABLE_NAME = @TableName)
	BEGIN
		DECLARE @RowStatusUpdateQuery NVARCHAR(1000);

		SET @RowStatusUpdateQuery = CONCAT('UPDATE ',@TableSchema,'.',@TableName,' SET [RowStatus] = ''',@RowStatus,''', [ModifiedBy] = ''',@LoggedInUserId,''', [ModifiedDate] = ''',GETUTCDATE(),''' WHERE [Id] = ''',@Id,'''');
		EXEC (@RowStatusUpdateQuery)
	END

	SET @Result = @Id;

	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()   
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
