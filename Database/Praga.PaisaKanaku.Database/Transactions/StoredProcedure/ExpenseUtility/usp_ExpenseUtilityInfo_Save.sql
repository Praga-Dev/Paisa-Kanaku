CREATE PROCEDURE [Transactions].[usp_ExpenseTravelInfo_Save]
	@Id UNIQUEIDENTIFIER,
	@ExpenseInfoId UNIQUEIDENTIFIER,
	@ExpenseDate DATETIME,
	@ExpenseById UNIQUEIDENTIFIER,
	@Source NVARCHAR(250),
	@Destination NVARCHAR(250),
	@TravelDate DATETIME,
	@ExpenseAmount DECIMAL(12,3),
	@TransportMode NVARCHAR(15),
	@TravelService NVARCHAR(15),
	@Description NVARCHAR(250),
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

	IF(@ExpenseDate IS NULL OR ISDATE(@ExpenseDate) = 0)
	BEGIN
		RAISERROR('INVALID_EXPENSE_DATE', 16, 1);
	END

	IF(@ExpenseAmount <= 0)
	BEGIN
		RAISERROR('INVALID_PARAM_AMOUNT', 16, 1);
	END

	DECLARE @ExpenseTravelInfoId UNIQUEIDENTIFIER = CASE WHEN (@Id IS NULL OR @Id = @EmptyGuid) THEN NEWID() ELSE @Id END;

	SET @ExpenseDate = DATEADD(dd, 0, DATEDIFF(dd, 0, @ExpenseDate))

	IF(@ExpenseInfoId IS NULL OR @ExpenseInfoId = @EmptyGuid)
	BEGIN
		IF(EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Date] = @ExpenseDate))
		BEGIN
			SELECT @ExpenseInfoId = [Id] FROM [Transactions].[ExpenseInfo] WHERE [Date] = @ExpenseDate
		END
		ELSE
		BEGIN
			SET @ExpenseInfoId = NEWID();

			INSERT INTO [Transactions].[ExpenseInfo] ([Id], [Date], [CreatedBy])
			VALUES(@ExpenseInfoId, @ExpenseDate, @LoggedInUserId)
		END
	END
	ELSE
	BEGIN
		IF(NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseInfo] WHERE [Id] = @ExpenseInfoId AND [Date] = @ExpenseDate))
		BEGIN
			RAISERROR('INVALID_PARAM_EXPENSE_INFO_ID', 16, 1);			
		END
	END


	DECLARE @ExpenseAmountResult DECIMAL(12,3);

	IF(@Id IS NULL OR @Id = @EmptyGuid OR NOT EXISTS(SELECT TOP 1 1 FROM [Transactions].[ExpenseOutdoorFoodInfo] WHERE [Id] = @Id))
	BEGIN
		INSERT INTO [Transactions].[ExpenseTravelInfo]
		([Id], [ExpenseInfoId], [ExpenseDate], [ExpenseById], [Source], [Destination], [TravelDate], [ExpenseAmount], [TransportMode], [TravelService], [Description], [CreatedBy])
		VALUES
		(@ExpenseTravelInfoId, @ExpenseInfoId, @ExpenseDate, @ExpenseById, @Source, @Destination, @TravelDate, @ExpenseAmount, @TransportMode, @TravelService, @Description, @LoggedInUserId)

		EXEC [Common].[usp_v1_Add_To_ExpenseAmount] @ExpenseInfoId = @ExpenseInfoId, @Amount = @ExpenseAmount, @Result = @ExpenseAmountResult OUTPUT;
	END
	ELSE
	BEGIN  	
		DECLARE @OldExpenseAmount DECIMAL(12,3);
		SELECT @OldExpenseAmount = [ExpenseAmount] FROM [Transactions].[ExpenseOutdoorFoodInfo] 
		WHERE [Id] = @ExpenseTravelInfoId AND [RowStatus] = 'A'

		UPDATE [Transactions].[ExpenseTravelInfo]
			SET	[ExpenseById] = @ExpenseById,
				[Source] = @Source, 				
				[Destination] = @Destination, 	
				[TravelDate] = @TravelDate,
				[ExpenseAmount] = @ExpenseAmount, 
				[TransportMode] = @TransportMode,
				[TravelService] = @TravelService,
				[Description] = @Description,
                [ModifiedBy] = @LoggedInUserId,
				[ModifiedDate] = GETUTCDATE()
			WHERE [Id] = @ExpenseTravelInfoId AND [RowStatus] = 'A';

		DECLARE @UpdatedExpenseAmount DECIMAL(12,3);
		SET @UpdatedExpenseAmount = @ExpenseAmount - @OldExpenseAmount;

		EXEC [Common].[usp_v1_Add_To_ExpenseAmount] @ExpenseInfoId = @ExpenseInfoId, @Amount = @UpdatedExpenseAmount, @Result = @ExpenseAmountResult OUTPUT;

		IF (@ExpenseAmountResult <= 0)
		BEGIN
			RAISERROR('EXPENSE_AMOUNT_NOT_UPDATED_ON_EXPENSE_INFO_TABLE', 16, 1);
		END
	END	

	SET @Result = @ExpenseTravelInfoId;

	RETURN @Response;


END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH