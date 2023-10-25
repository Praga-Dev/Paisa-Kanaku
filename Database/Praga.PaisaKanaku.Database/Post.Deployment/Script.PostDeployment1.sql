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

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'CollateralTypeInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[CollateralTypeInfo] WHERE [CollateralType] LIKE N'JEWEL')
	BEGIN
		INSERT INTO [Lookups].[CollateralTypeInfo] ([CollateralType], [CollateralTypeValue], [SequenceId], [RowStatus]) VALUES (N'JEWEL', N'Jewel', 1, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[CollateralTypeInfo] WHERE [CollateralType] LIKE N'LAND')
	BEGIN
		INSERT INTO [Lookups].[CollateralTypeInfo] ([CollateralType], [CollateralTypeValue], [SequenceId], [RowStatus]) VALUES (N'LAND', N'Land', 2, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'MetricSystemInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MetricSystemInfo] WHERE [MetricSystem] LIKE N'C')
	BEGIN
		INSERT INTO [Lookups].[MetricSystemInfo] ([MetricSystem], [MetricSystemValue], [SequenceId], [RowStatus]) VALUES (N'C', N'Count Measure', 1, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MetricSystemInfo] WHERE [MetricSystem] LIKE N'L')
	BEGIN
		INSERT INTO [Lookups].[MetricSystemInfo] ([MetricSystem], [MetricSystemValue], [SequenceId], [RowStatus]) VALUES (N'L', N'Liquid Measure', 2, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MetricSystemInfo] WHERE [MetricSystem] LIKE N'W')
	BEGIN
		INSERT INTO [Lookups].[MetricSystemInfo] ([MetricSystem], [MetricSystemValue], [SequenceId], [RowStatus]) VALUES (N'W', N'Weight Measure', 3, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'MeasureTypeInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'KILOGRAM')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue], [MetricSystem], [SequenceId], [RowStatus]) VALUES (N'KILOGRAM', N'Kilogram', 'W', 1, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'GRAM')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue], [MetricSystem], [SequenceId], [RowStatus]) VALUES (N'GRAM', N'Gram', 'W', 2, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'LITRE')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue], [MetricSystem], [SequenceId], [RowStatus]) VALUES (N'LITRE', N'Litre', 'L', 3, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'MILLILITRE')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue], [MetricSystem], [SequenceId], [RowStatus]) VALUES (N'MILLILITRE', N'Millilitre', 'L', 4, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'UNIT')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue], [MetricSystem], [SequenceId], [RowStatus]) VALUES (N'UNIT', N'Unit', 'C', 5, 'A');
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[MeasureTypeInfo] WHERE [MeasureType] LIKE N'DOZEN')
	BEGIN
		INSERT INTO [Lookups].[MeasureTypeInfo] ([MeasureType], [MeasureTypeValue], [MetricSystem], [SequenceId], [RowStatus]) VALUES (N'DOZEN', N'Dozen', 'C', 6, 'A');
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

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'CountMeasureInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[LiquidMeasureInfo] WHERE [LiquidMeasure] LIKE N'u')
	BEGIN
		INSERT INTO [Lookups].[LiquidMeasureInfo] ([LiquidMeasure], [LiquidMeasureValue], [ConversionValue], [SequenceId], [RowStatus]) VALUES (N'u', N'Unit', 1, 1, 'A');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[LiquidMeasureInfo] WHERE [LiquidMeasure] LIKE N'dz')
	BEGIN
		INSERT INTO [Lookups].[LiquidMeasureInfo] ([LiquidMeasure], [LiquidMeasureValue], [ConversionValue], [SequenceId], [RowStatus]) VALUES (N'dz', N'Dozen', 12, 2, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'PaymentMethodInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[PaymentMethodInfo] WHERE [PaymentMethod] LIKE N'CASH')
	BEGIN
		INSERT INTO [Lookups].[PaymentMethodInfo] ([PaymentMethod], [PaymentMethodValue], [SequenceId], [RowStatus]) VALUES (N'CASH', N'Cash', 1, 'A');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[PaymentMethodInfo] WHERE [PaymentMethod] LIKE N'UPI')
	BEGIN
		INSERT INTO [Lookups].[PaymentMethodInfo] ([PaymentMethod], [PaymentMethodValue], [SequenceId], [RowStatus]) VALUES (N'UPI', N'UPI', 2, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[PaymentMethodInfo] WHERE [PaymentMethod] LIKE N'CARD')
	BEGIN
		INSERT INTO [Lookups].[PaymentMethodInfo] ([PaymentMethod], [PaymentMethodValue], [SequenceId], [RowStatus]) VALUES (N'CARD', N'Card', 3, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'TimePeriodTypeInfo') 
BEGIN
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'DAILY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'DAILY', N'Daily', 1, 'A');
	END
    IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'WEEKLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'WEEKLY', N'Weekly', 2, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'MONTHLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'MONTHLY', N'Monthly', 3, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'BIMONTHLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'BIMONTHLY', N'Bimonthly', 4, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'QUARTERLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'QUARTERLY', N'Quarterly', 5, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'SEMI_ANNUAL')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'SEMI_ANNUAL', N'Semi Annual', 6, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'ANNUAL')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'ANNUAL', N'Annual', 7, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'NA')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'NA', N'NA', 8, 'A');
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
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'LOAN_REPAYMENT') 
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'LOAN_REPAYMENT', N'Loan Repayment', 5, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'ELECTRONICS') 
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'ELECTRONICS', N'Electronics', 6, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'REPAIRS')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'REPAIRS', N'Repairs', 7, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'BUSINESS_NEEDS')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'BUSINESS_NEEDS', N'Business Needs', 8, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'UNCATEGORIZED') -- unplanned expenses like functions
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'UNCATEGORIZED', N'Uncategorized', 9, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'HEALTH_CARE')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'HEALTH_CARE', N'Health Care', 10, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'SAVINGS')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'SAVINGS', N'Savings', 11, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'LEND')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'LEND', N'Lend', 12, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'TRAVEL')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'TRAVEL', N'Travel', 13, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'GROOMING')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'GROOMING', N'Grooming', 14, 'A');
		END
		IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'FAMILY_WELLBEING')
		BEGIN
			INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'FAMILY_WELLBEING', N'Family Wellbeing', 14, 'A');
		END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'LoanTypeInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanTypeInfo] WHERE [LoanType] LIKE N'PERSONAL_LOAN')
	BEGIN
		INSERT INTO [Lookups].[LoanTypeInfo] ([LoanType], [LoanTypeValue], [SequenceId]) VALUES (N'PERSONAL_LOAN', N'Personal Loan', 1);
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanTypeInfo] WHERE [LoanType] LIKE N'GOLD_LOAN')
	BEGIN
		INSERT INTO [Lookups].[LoanTypeInfo] ([LoanType], [LoanTypeValue], [SequenceId]) VALUES (N'GOLD_LOAN', N'Gold Loan', 2);
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanTypeInfo] WHERE [LoanType] LIKE N'DOCUMENT')
	BEGIN
		INSERT INTO [Lookups].[LoanTypeInfo] ([LoanType], [LoanTypeValue], [SequenceId]) VALUES (N'DOCUMENT', N'Document', 3);
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanTypeInfo] WHERE [LoanType] LIKE N'CHECK')
	BEGIN
		INSERT INTO [Lookups].[LoanTypeInfo] ([LoanType], [LoanTypeValue], [SequenceId]) VALUES (N'CHECK', N'Check', 3);
	END
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanTypeInfo] WHERE [LoanType] LIKE N'PROMISSORY_NOTE')
	BEGIN
		INSERT INTO [Lookups].[LoanTypeInfo] ([LoanType], [LoanTypeValue], [SequenceId]) VALUES (N'PROMISSORY_NOTE', N'Promissory Note', 3);
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'LoanStatusInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanStatusInfo] WHERE [LoanStatus] LIKE N'OPEN')
	BEGIN
		INSERT INTO [Lookups].[LoanStatusInfo] ([LoanStatus], [LoanStatusValue]) VALUES (N'OPEN', N'Open');
	END

	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanStatusInfo] WHERE [LoanStatus] LIKE N'CLOSED')
	BEGIN
		INSERT INTO [Lookups].[LoanStatusInfo] ([LoanStatus], [LoanStatusValue]) VALUES (N'CLOSED', N'Closed');
	END

	IF NOT EXISTS(SELECT 1 FROM [Lookups].[LoanStatusInfo] WHERE [LoanStatus] LIKE N'HOLD')
	BEGIN
		INSERT INTO [Lookups].[LoanStatusInfo] ([LoanStatus], [LoanStatusValue]) VALUES (N'HOLD', N'Hold');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'GroceryCategoryInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'OPEN')
	BEGIN
		INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue]) VALUES (N'OPEN', N'Open');
	END
END
GO


IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Setup' AND T.TABLE_NAME = 'MemberInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'D2D0F25A-5FBC-4F1A-A0B2-EB1E7FF39AB3')
	BEGIN
		INSERT INTO [Setup].[MemberInfo]([Id], [Name], [CreatedBy]) 
		VALUES (N'D2D0F25A-5FBC-4F1A-A0B2-EB1E7FF39AB3', N'PRAGADEESHWARAN', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Setup' AND T.TABLE_NAME = 'BrandInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'137BE833-6047-44CC-BACD-A7ACF86CAA1C')
	BEGIN
		INSERT INTO [Setup].[BrandInfo]([Id], [Name], [CreatedBy]) 
		VALUES (N'137BE833-6047-44CC-BACD-A7ACF86CAA1C', N'NA', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
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