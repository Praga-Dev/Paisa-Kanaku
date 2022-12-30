CREATE PROCEDURE [Transactions].[usp_V1_LoanInfo_Save] -- CREATE LOAN
	@LoanType NVARCHAR(25),
	@LoanAmount DECIMAL(10,2),
	@InterestAmount DECIMAL(10,2),
	@MemberInfoId UNIQUEIDENTIFIER,
	@CollateralInfoId UNIQUEIDENTIFIER,
	@LenderInfoId UNIQUEIDENTIFIER,
	@BorrowedDate DATETIME2,
	@InterestDueDateOfEachMonth TINYINT,
	@LoanRepaymentDate DATETIME2,
	@GracePeriodDate DATETIME2,
	@LateFee DECIMAL(10,2),
	@Comments NVARCHAR(250),
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

DECLARE @UtcCurrentDate DATETIME2;
SET @UtcCurrentDate = GETUTCDATE()

BEGIN TRY 

	BEGIN
	
	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF(@LoanAmount <= 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOAN_AMOUNT', 16, 1);
	END

	IF(@InterestAmount <= 0 OR @LoanAmount <= @InterestAmount)
	BEGIN
		RAISERROR('INVALID_PARAM_INTEREST_AMOUNT', 16, 1);
	END

	IF(@BorrowedDate > @UtcCurrentDate)
	BEGIN
		RAISERROR('INVALID_PARAM_BORROWED_DATE', 16, 1);
	END

	IF(@InterestDueDateOfEachMonth <= 0 OR @InterestDueDateOfEachMonth > 31)
	BEGIN
		RAISERROR('INVALID_PARAM_INTEREST_DUE_DATE_OF_EACH_MONTH', 16, 1);
	END
		
	IF(@LoanRepaymentDate <= @BorrowedDate)
	BEGIN
		RAISERROR('INVALID_PARAM_LOAN_REPAYMENT_DATE', 16, 1);
	END

	IF(@GracePeriodDate IS NOT NULL AND @GracePeriodDate <= @LoanRepaymentDate)
	BEGIN
		RAISERROR('INVALID_PARAM_GRACE_PERIOD_DATE', 16, 1);
	END

	IF(@LateFee  IS NOT NULL AND @LateFee < 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LATE_FEE', 16, 1);
	END

	END

	DECLARE @LoanId UNIQUEIDENTIFIER =  NEWID();

	INSERT INTO [Transactions].[LoanInfo] ([Id], [LoanType], [LoanAmount], [InterestAmount], [MemberInfoId], [CollateralInfoId],
	[LenderInfoId], [BorrowedDate], [OutstandingBalance], [InterestDueDateOfEachMonth], [OutstandingInterestAmount], 
	[LoanRepaymentDate], [GracePeriodDate], [LateFee], [Comments], [CreatedBy])
	VALUES (@LoanId, @LoanType, @LoanAmount, @InterestAmount, @MemberInfoId, @CollateralInfoId, 
	@LenderInfoId, @BorrowedDate, @LoanAmount, @InterestDueDateOfEachMonth, @InterestAmount, 
	@LoanRepaymentDate, @GracePeriodDate, @LateFee, @Comments, @LoggedInUserId);
	
	SET @Result = @LoanId;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;