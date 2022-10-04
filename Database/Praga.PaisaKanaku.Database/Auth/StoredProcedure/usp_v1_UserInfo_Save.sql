CREATE PROCEDURE [Auth].[usp_v1_UserInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@UserName NVARCHAR(100),
	@FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
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

	DECLARE @UserId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

	IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Auth].[UserInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Auth].[UserInfo] ([Id], [UserName], [FirstName], [LastName], [CreatedBy])
		VALUES
			(@UserId, @UserName, @FirstName, @LastName, @LoggedInUserId);
	END
	ELSE IF EXISTS(SELECT TOP 1 1 FROM [Auth].[UserInfo] WHERE [Id] = @UserId AND [RowStatus] = 'A')
	BEGIN  
		UPDATE [Auth].[UserInfo]
			SET	[FirstName] = @FirstName
				,[LastName] = @LastName
				,[ModifiedBy] = @LoggedInUserId
				,[ModifiedDate] = GETUTCDATE()
			WHERE [Id] = @UserId AND [RowStatus] = 'A';
	END

	SET @Result = @UserId;

	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;
