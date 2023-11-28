CREATE PROCEDURE [Transactions].[usp_V1_Expense_Delete_By_Type]
	@Id UNIQUEIDENTIFIER,
	@ExpenseType NVARCHAR(25),
	@LoggedInUserId UNIQUEIDENTIFIER,
	@Result UNIQUEIDENTIFIER OUTPUT
AS
DECLARE @Response INT = 0;
DECLARE @ExpenseAmount DECIMAL(12,3) = 0;
DECLARE @ExpenseId UNIQUEIDENTIFIER;

DECLARE @EmptyGuid UNIQUEIDENTIFIER;
SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

BEGIN TRY

	IF([Auth].[udp_v1_ValidateAccount](@LoggedInUserId) = 0)
	BEGIN
		RAISERROR('INVALID_PARAM_LOGGED_IN_USER_ID', 16, 1);
	END

	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE @ExpenseType)
	BEGIN
		RAISERROR('INVALID_PARAM_EXPENSE_TYPE', 16, 1);
	END

	IF(@ExpenseType LIKE 'PRODUCT')
	BEGIN
		IF NOT EXISTS(SELECT * FROM [Transactions].[ExpenseProductInfo] WHERE [Id] = @Id)
		BEGIN
			RAISERROR('INVALID_PARAM_ID', 16, 1);
		END

		SELECT @ExpenseId = [ExpenseInfoId], @ExpenseAmount = [ExpenseAmount] FROM [Transactions].[ExpenseProductInfo] WHERE [Id] = @Id;
		UPDATE [Transactions].[ExpenseProductInfo] SET [RowStatus] = 'D' WHERE [Id] = @Id
	END
	ELSE IF(@ExpenseType LIKE 'GROCERY')
	BEGIN
		IF NOT EXISTS(SELECT * FROM [Transactions].[ExpenseGroceryInfo] WHERE [Id] = @Id)
		BEGIN
			RAISERROR('INVALID_PARAM_ID', 16, 1);
		END

		SELECT @ExpenseId = [ExpenseInfoId], @ExpenseAmount = [ExpenseAmount] FROM [Transactions].[ExpenseGroceryInfo] WHERE [Id] = @Id;
		UPDATE [Transactions].[ExpenseGroceryInfo] SET [RowStatus] = 'D' WHERE [Id] = @Id
	END
	ELSE IF(@ExpenseType LIKE 'FAMILY_WELLBEING')
	BEGIN
		IF NOT EXISTS(SELECT * FROM [Transactions].[ExpenseFamilyFundInfo] WHERE [Id] = @Id)
		BEGIN
			RAISERROR('INVALID_PARAM_ID', 16, 1);
		END

		SELECT @ExpenseId = [ExpenseInfoId], @ExpenseAmount = [ExpenseAmount] FROM [Transactions].[ExpenseFamilyFundInfo] WHERE [Id] = @Id;
		UPDATE [Transactions].[ExpenseFamilyFundInfo] SET [RowStatus] = 'D' WHERE [Id] = @Id
	END

	IF([Common].[udp_v1_ValidateGuid](@ExpenseId) = 0)
	BEGIN
		RAISERROR('ERROR_EXPENSE_INFO_ID', 16, 1);
	END

	DECLARE @TotalExpenseAmount DECIMAL(12,3);
	SELECT @TotalExpenseAmount = [Amount] FROM [Transactions].[ExpenseInfo] WHERE [Id] = @ExpenseId;

	IF(@TotalExpenseAmount < @ExpenseAmount)
	BEGIN
		RAISERROR('ERROR_TOTAL_EXPENSE_AMOUNT_MINIMUM_THAN_EXPENSE_AMOUNT', 16, 1);
	END

	UPDATE [Transactions].[ExpenseInfo] SET [Amount] = (@TotalExpenseAmount - @ExpenseAmount)  WHERE [Id] = @ExpenseId;

	SET @Result = @Id;
	RETURN @Response;

END TRY  
BEGIN CATCH  
	DECLARE @ErrorNumber INT = ERROR_NUMBER();  
	DECLARE @ErrorMessage NVARCHAR(1000) = ERROR_MESSAGE()
     
	RAISERROR('%s', 16, 1, @ErrorMessage)  
END CATCH;