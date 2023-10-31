CREATE PROCEDURE [Common].[usp_v1_Add_To_ExpenseAmount]
	@ExpenseInfoId UNIQUEIDENTIFIER,
	@Amount DECIMAL(12, 3),
	@Result DECIMAL(12, 3) OUTPUT
AS
DECLARE @Response INT = 0;
BEGIN
	DECLARE @TotalExpense DECIMAL(12,3);

	SELECT @TotalExpense = [Amount] FROM [Transactions].[ExpenseInfo] WHERE Id = @ExpenseInfoId
	SET @TotalExpense = @TotalExpense + @Amount

	IF(@TotalExpense IS NULL)
	BEGIN
		RAISERROR('INVALID_AMOUNT', 16, 1);
	END

	UPDATE [Transactions].[ExpenseInfo]
		SET [Amount] = @TotalExpense
		WHERE [Id] = @ExpenseInfoId;

	DECLARE @EmptyGuid UNIQUEIDENTIFIER;
	SET @EmptyGuid = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);
	
	SET @Result = @TotalExpense;

	RETURN @Response;
END