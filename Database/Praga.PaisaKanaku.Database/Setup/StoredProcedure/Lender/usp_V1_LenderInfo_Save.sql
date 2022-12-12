CREATE PROCEDURE [Setup].[usp_V1_LenderInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@EmailAddress NVARCHAR(50),
	@MobileNumber NVARCHAR(10),
	@Address NVARCHAR(100),
	--@PrincipalAmount DECIMAL(10,2),
	--@InterestAmount DECIMAL(10,2),
	--@OutstandingBalance DECIMAL(10,2),
	--@LendedMemberId UNIQUEIDENTIFIER,
	--@LendDate DATETIME2,
	--@Comments NVARCHAR(50),
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

	-- TODO Same for Email and Name
	IF (@Id = @EmptyGuid AND EXISTS(SELECT TOP 1 1 FROM [Setup].[LenderInfo] WHERE [MobileNumber] LIKE @MobileNumber))
	BEGIN
		RAISERROR('INVALID_PARAM_LENDER_ALREADY_EXIST', 16, 1);
	END

	-- TODO Validation

	DECLARE @LenderId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

    IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Setup].[LenderInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Setup].[LenderInfo] ([Id], [Name], [EmailAddress], [MobileNumber], [Address], [CreatedBy])
		VALUES (@LenderId, @Name, @EmailAddress, @MobileNumber, @Address, @LoggedInUserId);
		--INSERT INTO [Setup].[LenderInfo] ([Id], [Name], [PrincipalAmount], [InterestAmount], 
		--[OutstandingBalance], [LendedMemberId], [LendDate], [Comments], [CreatedBy])
		--VALUES (@LenderId, @Name, @PrincipalAmount, @InterestAmount, @OutstandingBalance, 
		--@LendedMemberId, @LendDate, @Comments, @LoggedInUserId);
	END
	ELSE
	BEGIN  
		UPDATE [Setup].[LenderInfo]
			SET	[Name] = @Name,
				[EmailAddress] = @EmailAddress,
				[MobileNumber] = @MobileNumber,
				[Address] = @Address,
                --[PrincipalAmount] = @PrincipalAmount,
                --[InterestAmount] = @InterestAmount,
                --[OutstandingBalance] = @OutstandingBalance,
                --[LendedMemberId] = @LendedMemberId,
                --[LendDate] = @LendDate,
                --[Comments] = @Comments,
				[ModifiedDate] = GETUTCDATE()
		WHERE [Id] = @LenderId AND [RowStatus] = 'A';
	END

	SET @Result = @LenderId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	-- Raise Exception  
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;