/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'RowStatusInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[RowStatusInfo] WHERE [RowStatus] LIKE N'A')
	BEGIN
		INSERT INTO [Lookups].[RowStatusInfo] ([RowStatus], [RowStatusValue]) VALUES (N'A', N'Active');
	END

	IF NOT EXISTS(SELECT 1 FROM [Lookups].[RowStatusInfo] WHERE [RowStatus] LIKE N'I')
	BEGIN
		INSERT INTO [Lookups].[RowStatusInfo] ([RowStatus], [RowStatusValue]) VALUES (N'I', N'Inactive');
	END

	IF NOT EXISTS(SELECT 1 FROM [Lookups].[RowStatusInfo] WHERE [RowStatus] LIKE N'T')
	BEGIN
		INSERT INTO [Lookups].[RowStatusInfo] ([RowStatus], [RowStatusValue]) VALUES (N'T', N'Trashed');
	END

    IF NOT EXISTS(SELECT 1 FROM [Lookups].[RowStatusInfo] WHERE [RowStatus] LIKE N'D')
	BEGIN
		INSERT INTO [Lookups].[RowStatusInfo] ([RowStatus], [RowStatusValue]) VALUES (N'D', N'Deleted');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'MeasureTypeInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'W')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue]) VALUES (N'L', N'WeightMeasure');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'L')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue]) VALUES (N'L', N'LiquidMeasure');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'WeightMeasureInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[WeightMeasureInfo] WHERE [WeightMeasure] LIKE N'kg')
	BEGIN
		INSERT INTO [Lookups].[WeightMeasureInfo] ([WeightMeasure], [WeightMeasureValue], [ConversionValue], [SequenceId], [RowStatus]) VALUES (N'kg', N'Kilogram', 1, 1, 'A');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[WeightMeasureInfo] WHERE [WeightMeasure] LIKE N'g')
	BEGIN
		INSERT INTO [Lookups].[WeightMeasureInfo] ([WeightMeasure], [WeightMeasureValue], [ConversionValue], [SequenceId], [RowStatus]) VALUES (N'g', N'Gram', 	0.001, 2, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'LiquidMeasureInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[LiquidMeasureInfo] WHERE [LiquidMeasure] LIKE N'l')
	BEGIN
		INSERT INTO [Lookups].[LiquidMeasureInfo] ([LiquidMeasure], [LiquidMeasureValue], [ConversionValue], [SequenceId], [RowStatus]) VALUES (N'l', N'Liter', 1, 1, 'A');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[LiquidMeasureInfo] WHERE [LiquidMeasure] LIKE N'ml')
	BEGIN
		INSERT INTO [Lookups].[LiquidMeasureInfo] ([LiquidMeasure], [LiquidMeasureValue], [ConversionValue], [SequenceId], [RowStatus]) VALUES (N'ml', N'Milliliter', 0.001, 2, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'PaymentMethodInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[PaymentMethodInfo] WHERE [PaymentMethod] LIKE N'CASH')
	BEGIN
		INSERT INTO [Lookups].[PaymentMethodInfo] ([PaymentMethod], [PaymentMethodValue]) VALUES (N'CASH', N'Cash');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[PaymentMethodInfo] WHERE [PaymentMethod] LIKE N'UPI')
	BEGIN
		INSERT INTO [Lookups].[PaymentMethodInfo] ([PaymentMethod], [PaymentMethodValue]) VALUES (N'UPI', N'UPI');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[PaymentMethodInfo] WHERE [PaymentMethod] LIKE N'CARD')
	BEGIN
		INSERT INTO [Lookups].[PaymentMethodInfo] ([PaymentMethod], [PaymentMethodValue]) VALUES (N'CARD', N'Card');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'TimePeriodInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodInfo] WHERE [TimePeriod] LIKE N'DAILY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodInfo] ([TimePeriod], [TimePeriodValue]) VALUES (N'DAILY', N'Daily');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodInfo] WHERE [TimePeriod] LIKE N'WEEKLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodInfo] ([TimePeriod], [TimePeriodValue]) VALUES (N'WEEKLY', N'Weekly');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodInfo] WHERE [TimePeriod] LIKE N'MONTHLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodInfo] ([TimePeriod], [TimePeriodValue]) VALUES (N'MONTHLY', N'Monthly');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodInfo] WHERE [TimePeriod] LIKE N'BIMONTHLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodInfo] ([TimePeriod], [TimePeriodValue]) VALUES (N'BIMONTHLY', N'Bimonthly');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodInfo] WHERE [TimePeriod] LIKE N'NA')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodInfo] ([TimePeriod], [TimePeriodValue]) VALUES (N'NA', N'NA');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'ExpenseTypeInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'ENTERTAINMENT')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'ENTERTAINMENT', N'Entertainment', 1, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'HOUSEHOLD')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'HOUSEHOLD', N'Household', 2, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'BILLS')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'BILLS', N'Bills', 3, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'LOAN_INTEREST')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'LOAN_INTEREST', N'Loan Interest', 4, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'ELECTRONICS') 
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'ELECTRONICS', N'Electronics', 5, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'REPAIRS')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'REPAIRS', N'Repairs', 6, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'BUSINESS_NEEDS')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'BUSINESS_NEEDS', N'Business Needs', 7, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'UNCATEGORIZED') -- unplanned expenses like functions
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'UNCATEGORIZED', N'Uncategorized', 8, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'MEDICAL')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'MEDICAL', N'Medical', 9, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'SAVINGS')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'SAVINGS', N'Savings', 10, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'LEND')
	BEGIN
		INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'Lend', N'Lend', 11, 'A');
	END
END
GO


IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Auth' AND T.TABLE_NAME = 'UserInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Auth].[UserInfo] WHERE [Id] LIKE N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
	BEGIN
		INSERT INTO [Auth].[UserInfo]([Id], [UserName], [FirstName], [LastName], [CreatedBy]) 
		VALUES (N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5', N'PRAGADEESHWARAN', N'PRAGADEESHWARAN', N'PASUPATHI', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
	END
END
GO