CREATE PROCEDURE [Setup].[usp_V1_MemberInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@ManagesExpense BIT, 
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

	IF (@Id = @EmptyGuid AND EXISTS(SELECT TOP 1 1 FROM [Setup].[MemberInfo] WHERE [Name] LIKE @Name))
	BEGIN
		RAISERROR('INVALID_PARAM_MEMBER_ALREADY_EXIST', 16, 1);
	END

	DECLARE @MemberId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[MemberInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[MemberInfo] ([Id], [Name], [ManagesExpense], [CreatedBy])
		VALUES (@MemberId, @Name, @ManagesExpense, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[MemberInfo]
			SET	[Name] = @Name,
				[ManagesExpense] = @ManagesExpense, 
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @MemberId AND [RowStatus] = 'A';
	END

	SET @Result = @MemberId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;