CREATE PROCEDURE [Setup].[usp_V1_LenderInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@EmailAddress NVARCHAR(50),
	@MobileNumber NVARCHAR(10),
	@Address NVARCHAR(100),
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
set @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY 

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF(@Name IS NULL OR @Name = '')
	BEGIN
		RAISERROR('INVALID_PARAM_NAME', 16, 1);
	END

	IF(@MobileNumber IS NULL OR @MobileNumber = '')
	BEGIN
		RAISERROR('INVALID_PARAM_MOBILE_NUMBER', 16, 1);
	END

	IF(@Address IS NULL OR @Address = '')
	BEGIN
		RAISERROR('INVALID_PARAM_ADDRESS', 16, 1);
	END

	IF ((@Id IS NULL OR @Id = @EmptyGuid) AND 
		((EXISTS(SELECT TOP 1 1 FROM [Setup].[LenderInfo] WHERE [Name] LIKE @Name) OR 
		(NOT (@EmailAddress IS NULL OR @EmailAddress = '') AND EXISTS(SELECT TOP 1 1 FROM [Setup].[LenderInfo] WHERE [EmailAddress] LIKE @EmailAddress)) OR 
		EXISTS(SELECT TOP 1 1 FROM [Setup].[LenderInfo] WHERE [MobileNumber] LIKE @MobileNumber))))
	BEGIN
		RAISERROR('INVALID_PARAM_LENDER_ALREADY_EXIST', 16, 1);
	END


	DECLARE @LenderId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[LenderInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[LenderInfo] ([Id], [Name], [EmailAddress], [MobileNumber], [Address], [CreatedBy])
		VALUES (@LenderId, @Name, @EmailAddress, @MobileNumber, @Address, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[LenderInfo]
			SET	[Name] = @Name,
				[EmailAddress] = @EmailAddress,
				[MobileNumber] = @MobileNumber,
				[Address] = @Address,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @LenderId AND [RowStatus] = 'A';
	END

	SET @Result = @LenderId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;