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
		INSERT INTO [Lookups].[RowStatusInfo] ([RowStatus], [RowStatusValue]) VALUES (N'T', N'Temporary');
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
    IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'BIWEEKLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'BIWEEKLY', N'Bi-Weekly', 3, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'MONTHLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'MONTHLY', N'Monthly', 4, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'BIMONTHLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'BIMONTHLY', N'Bi-Monthly', 5, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'QUARTERLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'QUARTERLY', N'Quarterly', 6, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'SEMI_ANNUALLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'SEMI_ANNUALLY', N'Semi Annually', 7, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'ANNUALLY')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'ANNUALLY', N'Annually', 8, 'A');
	END
	IF NOT EXISTS(SELECT * FROM [Lookups].[TimePeriodTypeInfo] WHERE [TimePeriodType] LIKE N'AS_NEEDED')
	BEGIN
		INSERT INTO [Lookups].[TimePeriodTypeInfo] ([TimePeriodType], [TimePeriodTypeValue], [SequenceId], [RowStatus]) VALUES (N'AS_NEEDED', N'As Needed', 9, 'A');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'ExpenseTypeInfo') 
BEGIN
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'PRODUCT')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'PRODUCT', N'Product', 1, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'GROCERY')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'GROCERY', N'Grocery', 2, 'A');
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
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'REPAIRS')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'REPAIRS', N'Repairs', 6, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'HEALTH_CARE')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'HEALTH_CARE', N'Health Care', 7, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'SAVINGS')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'SAVINGS', N'Savings', 8, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'LEND')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'LEND', N'Lend', 9, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'TRAVEL')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'TRAVEL', N'Travel', 10, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'GROOMING')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'GROOMING', N'Grooming', 11, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'FAMILY_WELLBEING')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'FAMILY_WELLBEING', N'Family Wellbeing', 12, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'ENTERTAINMENT')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'ENTERTAINMENT', N'Entertainment', 13, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'BUSINESS_NEEDS')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'BUSINESS_NEEDS', N'Business Needs', 14, 'A');
    END  
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'UNCATEGORIZED') -- unplanned expenses like functions
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'UNCATEGORIZED', N'Uncategorized', 15, 'A');
    END
    IF NOT EXISTS(SELECT * FROM [Lookups].[ExpenseTypeInfo] WHERE [ExpenseType] LIKE N'OUTDOOR_FOOD')
    BEGIN
	    INSERT INTO [Lookups].[ExpenseTypeInfo] ([ExpenseType], [ExpenseTypeValue], [SequenceId], [RowStatus]) VALUES (N'OUTDOOR_FOOD', N'Outdoor Food', 15, 'A');
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

    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'BABY_ITEMS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'BABY_ITEMS', N'Baby Items', 1, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'BAKERY')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'BAKERY', N'Bakery', 2, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'BEVERAGES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'BEVERAGES', N'Beverages', 3, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'CEREALS_&_GRAINS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'CEREALS_&_GRAINS', N'Cereals & Grains', 4, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'CLEANING_SUPPLIES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'CLEANING_SUPPLIES', N'Cleaning Supplies', 5, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'DAIRY')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'DAIRY', N'Dairy', 6, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'DALS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'DALS', N'Dals', 7, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'DRY_FRUITS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'DRY_FRUITS', N'Dry Fruits', 8, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'FESTIVE_ITEMS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'FESTIVE_ITEMS', N'Festive Items', 9, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'FLOWERS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'FLOWERS', N'Flowers', 10, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'FROZEN_FOODS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'FROZEN_FOODS', N'Frozen Foods', 11, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'FRUITS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'FRUITS', N'Fruits', 12, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'HEALTH_CARE')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'HEALTH_CARE', N'Health Care', 13, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'KITCHENWARE')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'KITCHENWARE', N'Kitchenware', 14, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'LEAVES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'LEAVES', N'Leaves', 15, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'MASALA')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'MASALA', N'Masala', 16, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'MEAT')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'MEAT', N'Meat', 17, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'OILS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'OILS', N'Oils', 18, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'OTHER_ITEMS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'OTHER_ITEMS', N'Other items', 19, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'PACKETED_FOODS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'PACKETED_FOODS', N'Packeted Foods', 20, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'PAPER_&_WRAP')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'PAPER_&_WRAP', N'Paper & Wrap ', 21, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'PERSONAL_CARE')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'PERSONAL_CARE', N'Personal Care', 22, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'PET_CARE')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'PET_CARE', N'Pet Care', 23, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'PULSES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'PULSES', N'Pulses', 24, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'RICE')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'RICE', N'Rice', 25, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'SAUCES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'SAUCES', N'Sauces', 26, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'SEA_FOOD')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'SEA_FOOD', N'Sea Food', 27, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'SNACKS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'SNACKS', N'Snacks', 28, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'SPICES_&_CONDIMENTS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'SPICES_&_CONDIMENTS', N'Spices & Condiments', 29, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'TOOLS_&_SUPPLIES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'TOOLS_&_SUPPLIES', N'Tools & Supplies', 30, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'VEGETABLES')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'VEGETABLES', N'Vegetables', 31, 'A');
    END

    IF NOT EXISTS(SELECT 1 FROM [Lookups].[GroceryCategoryInfo] WHERE [GroceryCategory] LIKE N'READY_TO_COOK_PRODUCTS')
    BEGIN
        INSERT INTO [Lookups].[GroceryCategoryInfo] ([GroceryCategory], [GroceryCategoryValue], [SequenceId], [RowStatus]) VALUES (N'READY_TO_COOK_PRODUCTS', N'Ready to Cook Products', 32, 'A');
    END
                        
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'ProductCategoryInfo') 
BEGIN

    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'AUTOMOTIVE_PRODUCTS')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'AUTOMOTIVE_PRODUCTS', N'Automotive Products', 1, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'BUSINESS_NEEDS')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'BUSINESS_NEEDS', N'Business Needs', 2, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'ELECTRONICS')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'ELECTRONICS', N'Electronics', 3, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'FASHION')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'FASHION', N'Fashion', 4, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'GROOMING')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'GROOMING', N'Grooming', 5, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'MEDICAL')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'MEDICAL', N'Medical', 6, 'A');
    END
                        
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'SPORTS_&_OUTDOORS')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId], [RowStatus]) VALUES (N'SPORTS_&_OUTDOORS', N'Sports & Outdoors', 7, 'A');
    END

	IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'STATIONERY')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId]) VALUES (N'STATIONERY', N'Stationery', 8);
    END

	IF NOT EXISTS(SELECT 1 FROM [Lookups].[ProductCategoryInfo] WHERE [ProductCategory] LIKE N'UNCATEGORIZED')
    BEGIN
        INSERT INTO [Lookups].[ProductCategoryInfo] ([ProductCategory], [ProductCategoryValue], [SequenceId]) VALUES (N'UNCATEGORIZED', N'Uncategorized', 9);
    END
                        
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'RelationshipTypeInfo') 
BEGIN
    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'SELF')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'SELF', N'Self');
    END

    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'FAMILY')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'FAMILY', N'Family');
    END

    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'FRIENDS')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'FRIENDS', N'Friends');
    END

    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'LENDER')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'LENDER', N'Lender');
    END

    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'SELLER')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'SELLER', N'Seller');
    END

    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'OWNER')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'OWNER', N'Owner');
    END

    IF NOT EXISTS(SELECT * FROM [Lookups].[RelationshipTypeInfo] WHERE [RelationshipType] LIKE N'SERVICE_PROVIDER')
    BEGIN
        INSERT INTO [Lookups].[RelationshipTypeInfo] ([RelationshipType], [RelationshipTypeValue]) VALUES (N'SERVICE_PROVIDER', N'	Service Provider');
    END

END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'TravelServiceInfo') 
BEGIN
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'GOVERMENT')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'GOVERMENT', N'Goverment');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'IRCTC')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'IRCTC', N'IRCTC');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'METRO_RAIL')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'METRO_RAIL', N'Metro Rail');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'OLA')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'OLA', N'Ola');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'RAPIDO')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'RAPIDO', N'Rapido');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'SELF')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'SELF', N'Self');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TravelServiceInfo] WHERE [TravelService] LIKE N'UBER')
    BEGIN
	    INSERT INTO [Lookups].[TravelServiceInfo]([TravelService], [TravelServiceValue]) VALUES (N'UBER', N'Uber');
    END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Lookups' AND T.TABLE_NAME = 'TransportModeInfo') 
BEGIN
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'AEROPLANE')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'AEROPLANE', N'Aeroplane');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'BIKE')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'BIKE', N'Bike');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'BOAT')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'BOAT', N'Boat');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'BUS')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'BUS', N'Bus');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'CAB')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'CAB', N'Cab');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'TRAIN')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'TRAIN', N'Train');
    END
    IF NOT EXISTS(SELECT 1 FROM [Lookups].[TransportModeInfo] WHERE [TransportMode] LIKE N'ZIPLINE')
    BEGIN
	    INSERT INTO [Lookups].[TransportModeInfo]([TransportMode], [TransportModeValue]) VALUES (N'ZIPLINE', N'Zipline');
    END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Setup' AND T.TABLE_NAME = 'MemberInfo') 
BEGIN
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'A881317E-45DA-4CA0-A595-0A63D90169D3')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'A881317E-45DA-4CA0-A595-0A63D90169D3', N'Chithra', 'SELF', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'D2D0F25A-5FBC-4F1A-A0B2-EB1E7FF39AB3')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'D2D0F25A-5FBC-4F1A-A0B2-EB1E7FF39AB3', N'Praga', 'SELF', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END

    -----------------------------------------------------------------------------------------------------------

    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'3CCFE639-E769-4AAA-B99B-D085D154FD20')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'3CCFE639-E769-4AAA-B99B-D085D154FD20', N'Pasupathi', 'FAMILY', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'BA2600A2-6F3E-4AAA-86F5-8AC7DA348C91')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'BA2600A2-6F3E-4AAA-86F5-8AC7DA348C91', N'Sivagami', 'FAMILY', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'4670CBFB-164E-4F5C-B006-D41A5025047E')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'4670CBFB-164E-4F5C-B006-D41A5025047E', N'Mahendran', 'FAMILY', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'AB6BD0B3-1548-4EFF-8D30-111D373ECBBA')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'AB6BD0B3-1548-4EFF-8D30-111D373ECBBA', N'Sumathi', 'FAMILY', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'98C43AC4-7425-40DE-9E42-789563A50093')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'98C43AC4-7425-40DE-9E42-789563A50093', N'Sailesh', 'FAMILY', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[MemberInfo] WHERE [Id] LIKE N'7713009D-F14F-4859-A872-FE8052BEEBF0')
    BEGIN
	    INSERT INTO [Setup].[MemberInfo]([Id], [Name], [RelationshipType], [CreatedBy]) 
	    VALUES (N'7713009D-F14F-4859-A872-FE8052BEEBF0', N'Shiva', 'FAMILY', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
    END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Setup' AND T.TABLE_NAME = 'BrandInfo') 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [Setup].[BrandInfo] WHERE [Id] LIKE N'137BE833-6047-44CC-BACD-A7ACF86CAA1C')
	BEGIN
		INSERT INTO [Setup].[BrandInfo]([Id], [Name], [CreatedBy]) 
		VALUES (N'137BE833-6047-44CC-BACD-A7ACF86CAA1C', N'NA', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5');
	END
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_SCHEMA = 'Setup' AND T.TABLE_NAME = 'GroceryInfo') 
BEGIN
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5CE58892-2102-4916-AAD0-78D01C91FFB0')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5CE58892-2102-4916-AAD0-78D01C91FFB0', N'Cool drinks', N'BEVERAGES', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'715931D2-9146-4153-B268-FFA8B9F30A1D')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'715931D2-9146-4153-B268-FFA8B9F30A1D', N'Fresh juices', N'BEVERAGES', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'09B65E9A-DE51-43E6-8A9B-FF9AC9603E20')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'09B65E9A-DE51-43E6-8A9B-FF9AC9603E20', N'Soda', N'BEVERAGES', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E1BBE1F8-37D3-4C9C-8359-F3B4DCF23DB1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E1BBE1F8-37D3-4C9C-8359-F3B4DCF23DB1', N'Aval', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'78EEF341-3193-474B-B783-FB99B48B0AB8')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'78EEF341-3193-474B-B783-FB99B48B0AB8', N'Baby Corn', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'8273A62E-D3A5-4945-BA6D-87BEB38EEB95')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'8273A62E-D3A5-4945-BA6D-87BEB38EEB95', N'Kaalan', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B8344DB7-D9AF-43E0-874B-68F59E1CFD38')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B8344DB7-D9AF-43E0-874B-68F59E1CFD38', N'Maida', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'FDDAA32E-EB59-4977-AF17-762E74E21C58')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'FDDAA32E-EB59-4977-AF17-762E74E21C58', N'Maida Floor', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'21C2EA5D-AD86-4075-B7D2-39C7D48D6E71')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'21C2EA5D-AD86-4075-B7D2-39C7D48D6E71', N'Paneer', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'23CECB5F-6123-4CD8-B651-8A940AA7A6D7')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'23CECB5F-6123-4CD8-B651-8A940AA7A6D7', N'Pasta', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'C3B17305-7460-48A1-8305-34CC712AC5FF')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'C3B17305-7460-48A1-8305-34CC712AC5FF', N'PerungayaThool', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D40BC44D-80B9-41ED-B114-12BD05135FAE')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D40BC44D-80B9-41ED-B114-12BD05135FAE', N'Puli', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'6CA6CBC0-E5F4-4D07-936A-266F515CBE89')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'6CA6CBC0-E5F4-4D07-936A-266F515CBE89', N'Ragi Semiya', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'8844E6CC-5716-493D-B5A1-ABB765FD78B5')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'8844E6CC-5716-493D-B5A1-ABB765FD78B5', N'Rava', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'4B90059B-169E-4DB4-BE55-EC7DE92A0FBA')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'4B90059B-169E-4DB4-BE55-EC7DE92A0FBA', N'Semiya', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'33BD6244-8887-4D71-974A-99305AD8588A')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'33BD6244-8887-4D71-974A-99305AD8588A', N'Vellam', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'9ECE1039-0AAC-4AC6-99C8-2555CBEE54D1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'9ECE1039-0AAC-4AC6-99C8-2555CBEE54D1', N'Vendayam', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'1EEA6C93-9D0C-4695-A11D-97DA6E2E25ED')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'1EEA6C93-9D0C-4695-A11D-97DA6E2E25ED', N'Wheat Rava', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'387110C0-613F-43C5-B15E-313B1D8ACF53')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'387110C0-613F-43C5-B15E-313B1D8ACF53', N'wheat Floor', N'CEREALS_&_GRAINS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'575C9877-17E6-428F-AE39-26A4BA96730E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'575C9877-17E6-428F-AE39-26A4BA96730E', N'Dettol', N'CLEANING_SUPPLIES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'038E9BE9-2381-4450-AC2F-1E9BCDBF782D')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'038E9BE9-2381-4450-AC2F-1E9BCDBF782D', N'Floor Cleaner', N'CLEANING_SUPPLIES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'71BDAD4A-1089-4D88-A0C8-77F06FA86470')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'71BDAD4A-1089-4D88-A0C8-77F06FA86470', N'Laundry Detergent', N'CLEANING_SUPPLIES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'CD5D7CED-5C8A-491B-BAE5-AA910E48A425')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'CD5D7CED-5C8A-491B-BAE5-AA910E48A425', N'Toilet Cleaner', N'CLEANING_SUPPLIES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'92C4A7D2-A1D2-4302-AD1D-61BFFFD81EF4')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'92C4A7D2-A1D2-4302-AD1D-61BFFFD81EF4', N'Butter', N'DAIRY', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B029BD9B-AD79-4FC0-9FD5-3555B8FFB836')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B029BD9B-AD79-4FC0-9FD5-3555B8FFB836', N'Cheese', N'DAIRY', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DD57F476-7F7C-4AB2-A31D-4F2A4825EF29')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DD57F476-7F7C-4AB2-A31D-4F2A4825EF29', N'Curd', N'DAIRY', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'130635A1-DB9E-48B5-88D2-99C74366F479')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'130635A1-DB9E-48B5-88D2-99C74366F479', N'Egg', N'DAIRY', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'9FF21B40-ACE3-495E-BE54-BEFA0987E262')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'9FF21B40-ACE3-495E-BE54-BEFA0987E262', N'Ghee', N'DAIRY', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'431EEAEB-DD2C-450A-BFE3-09A65278EDD2')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'431EEAEB-DD2C-450A-BFE3-09A65278EDD2', N'Milk', N'DAIRY', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'0970857E-6CC5-4387-97E1-B9A7599CAFE6')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'0970857E-6CC5-4387-97E1-B9A7599CAFE6', N'Gundu ultam Paruppu', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'8519A50F-28B9-4496-9A4D-EFDDA383E993')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'8519A50F-28B9-4496-9A4D-EFDDA383E993', N'Kadalai Paruppu', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'417416D1-4B92-402A-9CEE-6ACACEE0C9CB')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'417416D1-4B92-402A-9CEE-6ACACEE0C9CB', N'Kollu', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F28AE52C-3CF4-463D-9FF4-5748A50A63AA')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F28AE52C-3CF4-463D-9FF4-5748A50A63AA', N'Moong Dal', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'73BEF4B2-DA32-4410-87C8-B70DA60C9EBD')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'73BEF4B2-DA32-4410-87C8-B70DA60C9EBD', N'Odacha Ulutham Paruppu', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'BCE875BA-D005-4627-9A18-11224D48604B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'BCE875BA-D005-4627-9A18-11224D48604B', N'Paccha Parupu', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'83C7AB5D-42A9-4E70-87F0-EEA074D96904')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'83C7AB5D-42A9-4E70-87F0-EEA074D96904', N'Paccha Payiru', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'EB078385-BE9C-4E1E-BF95-DC18629B3117')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'EB078385-BE9C-4E1E-BF95-DC18629B3117', N'Sundal', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'C54BDD6E-3FA0-4DB6-A713-2701141910BF')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'C54BDD6E-3FA0-4DB6-A713-2701141910BF', N'Thuvaram Paruppu', N'DALS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'2838B564-D7E3-4A49-B4E2-1FDDE6E1B56B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'2838B564-D7E3-4A49-B4E2-1FDDE6E1B56B', N'Badam', N'DRY_FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'27EED78D-984A-427F-B460-FF8DBDC50293')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'27EED78D-984A-427F-B460-FF8DBDC50293', N'Cashew nut', N'DRY_FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F199671F-DEA9-4C6B-BB05-CA3013A5C205')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F199671F-DEA9-4C6B-BB05-CA3013A5C205', N'Dates', N'DRY_FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DB466588-8F04-41E4-8E53-FE46671A0410')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DB466588-8F04-41E4-8E53-FE46671A0410', N'Dry grapes', N'DRY_FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'14E484D4-EF40-4A38-8BF6-CBA8A0082EFC')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'14E484D4-EF40-4A38-8BF6-CBA8A0082EFC', N'Pista', N'DRY_FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E6F317F9-C7D2-425D-9D2C-824CC0EA449C')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E6F317F9-C7D2-425D-9D2C-824CC0EA449C', N'Walnut', N'DRY_FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'4A3089F0-0BC6-4824-B244-DCA9E124E4F6')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'4A3089F0-0BC6-4824-B244-DCA9E124E4F6', N'Apple', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'75A62532-F4E2-480D-BBB4-565019EA9025')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'75A62532-F4E2-480D-BBB4-565019EA9025', N'Avocado', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'71221FE4-4035-4A68-ACBD-A696C0289F17')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'71221FE4-4035-4A68-ACBD-A696C0289F17', N'Banana', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DB505FD9-004D-4D91-B8C3-E42602F1EBDB')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DB505FD9-004D-4D91-B8C3-E42602F1EBDB', N'Blackberry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'0489F5C0-E5E3-4656-82F7-99439B216BEB')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'0489F5C0-E5E3-4656-82F7-99439B216BEB', N'Blackcurrant', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'2F68B141-981E-4CF2-AE74-5FAE5A34614B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'2F68B141-981E-4CF2-AE74-5FAE5A34614B', N'Blueberry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'EE1C2CB1-F583-42CA-A1C2-867638538CD1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'EE1C2CB1-F583-42CA-A1C2-867638538CD1', N'Cherry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'2308E944-F85D-444D-9DCC-3F27E2B9B556')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'2308E944-F85D-444D-9DCC-3F27E2B9B556', N'Cucumber', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D3E87BB5-8D4C-4371-86B3-E6FB1E3A2748')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D3E87BB5-8D4C-4371-86B3-E6FB1E3A2748', N'Date', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F5012F6E-745B-4386-8A99-40932933AF6E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F5012F6E-745B-4386-8A99-40932933AF6E', N'Dragonfruit', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'30E33077-0049-4147-899C-D6F367A2F451')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'30E33077-0049-4147-899C-D6F367A2F451', N'Fig', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B9E22488-D17C-4834-A41F-969A05445CA9')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B9E22488-D17C-4834-A41F-969A05445CA9', N'Gooseberry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DBAC5562-DC12-4133-A304-7449A576EFBF')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DBAC5562-DC12-4133-A304-7449A576EFBF', N'Grape', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'529664B4-2A13-49AF-B641-6AE479CC91F6')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'529664B4-2A13-49AF-B641-6AE479CC91F6', N'Guava', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'3CCD794A-3A70-4DC3-B804-41DA1EBB8169')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'3CCD794A-3A70-4DC3-B804-41DA1EBB8169', N'Jackfruit', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'EE6C09E0-A693-4790-8B5C-EA0EBFD79F4C')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'EE6C09E0-A693-4790-8B5C-EA0EBFD79F4C', N'Kiwi', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'AED221B4-213D-4EE5-9294-72DD7379A988')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'AED221B4-213D-4EE5-9294-72DD7379A988', N'Lemon', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'FCB602FD-5BD6-46A7-AE1A-1D8DA48AB59F')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'FCB602FD-5BD6-46A7-AE1A-1D8DA48AB59F', N'Mango', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'526FCABD-6F79-4668-968D-E0008457A718')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'526FCABD-6F79-4668-968D-E0008457A718', N'Mulberry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5B6D76CF-D035-49F2-A01F-74E7AD25199F')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5B6D76CF-D035-49F2-A01F-74E7AD25199F', N'Olive', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'8AA6A9CA-016A-45C8-BD23-5705109E7EC1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'8AA6A9CA-016A-45C8-BD23-5705109E7EC1', N'Orange', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E4B7F665-F765-4D2C-B696-C97927A2636B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E4B7F665-F765-4D2C-B696-C97927A2636B', N'Papaya', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'58F67457-9078-430E-9625-8BD1CC206A37')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'58F67457-9078-430E-9625-8BD1CC206A37', N'Peach', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'81DFE022-6B68-4458-BE4B-58851ECA8C78')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'81DFE022-6B68-4458-BE4B-58851ECA8C78', N'Pineapple', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'FFE723D8-C3E2-48C2-8EE5-B679B2BA93E9')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'FFE723D8-C3E2-48C2-8EE5-B679B2BA93E9', N'Plum', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DDD22648-F99D-46E4-9B17-BB55C9018DE2')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DDD22648-F99D-46E4-9B17-BB55C9018DE2', N'Pomegranate', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F7BE3926-9842-4C62-A00E-8A7D1C7AC1EF')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F7BE3926-9842-4C62-A00E-8A7D1C7AC1EF', N'Raisin', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7D2E840A-A9CB-43B1-8FAF-6C5836C745B4')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7D2E840A-A9CB-43B1-8FAF-6C5836C745B4', N'Rambutan', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'3C9C7485-E146-4930-A894-DF5CBD083775')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'3C9C7485-E146-4930-A894-DF5CBD083775', N'Raspberry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'A5B83853-EA91-47FF-A281-BC2763C2AE25')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'A5B83853-EA91-47FF-A281-BC2763C2AE25', N'Strawberry', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'40FD996D-C979-4F0F-931F-C7339E1250FA')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'40FD996D-C979-4F0F-931F-C7339E1250FA', N'Watermelon', N'FRUITS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'9AF39C8F-7537-45FE-81B5-F046C006F549')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'9AF39C8F-7537-45FE-81B5-F046C006F549', N'Face Wash', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E1A5A4FB-972D-48D3-BFB0-A3979B1B8F2B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E1A5A4FB-972D-48D3-BFB0-A3979B1B8F2B', N'Moisturizer', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'6C7D59A1-750C-4359-AB33-5ED581A5A7FC')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'6C7D59A1-750C-4359-AB33-5ED581A5A7FC', N'Shampoo', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'27C49734-DD8F-45F8-98F1-E31F78B80B7A')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'27C49734-DD8F-45F8-98F1-E31F78B80B7A', N'Soap', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'6C5F5C30-C370-4402-9F67-325A969C6428')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'6C5F5C30-C370-4402-9F67-325A969C6428', N'Sun Screen', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D99A7DE1-96BC-4195-9F9E-19C6C0283490')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D99A7DE1-96BC-4195-9F9E-19C6C0283490', N'Tooth Brush', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E0B148F9-AF48-43FB-873E-A2A2E015F70D')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E0B148F9-AF48-43FB-873E-A2A2E015F70D', N'Tooth Paste', N'HEALTH_CARE', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END    
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'29506A55-1E0C-49E4-A35D-E38818D1C039')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'29506A55-1E0C-49E4-A35D-E38818D1C039', N'Lucky Shot - Glass', N'KITCHENWARE', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'72EF6559-782C-44CD-8C88-55BD0133D8AD')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'72EF6559-782C-44CD-8C88-55BD0133D8AD', N'Lunch Box', N'KITCHENWARE', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'151B073F-4757-4A7E-B2EF-54F9AEA77BDD')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'151B073F-4757-4A7E-B2EF-54F9AEA77BDD', N'Peeler', N'KITCHENWARE', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'50EAF1D7-0386-49D8-8F86-B70D2984993C')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'50EAF1D7-0386-49D8-8F86-B70D2984993C', N'Kothamalli - Karuveppilai', N'LEAVES', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F0CB813A-57B9-4419-89FB-6451C22DB90E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F0CB813A-57B9-4419-89FB-6451C22DB90E', N'Mint', N'LEAVES', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7330128E-C64B-495F-997B-911260C31977')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7330128E-C64B-495F-997B-911260C31977', N'Spinach', N'LEAVES', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'3E760485-D256-4C23-A55E-9947FE030128')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'3E760485-D256-4C23-A55E-9947FE030128', N'Chicken Masala', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'ABE051CF-0C12-4F6E-A3EE-576A59ADA607')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'ABE051CF-0C12-4F6E-A3EE-576A59ADA607', N'Fish Masala Thool', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5CC0D140-7AAC-41F8-B450-65DB8CED0BB6')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5CC0D140-7AAC-41F8-B450-65DB8CED0BB6', N'Garam Masala', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7810D6B6-9D7B-44E2-991F-A005E0FE539B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7810D6B6-9D7B-44E2-991F-A005E0FE539B', N'Idli Podi', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'36CAC6B3-5D89-4849-87DC-37071AD8A0EF')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'36CAC6B3-5D89-4849-87DC-37071AD8A0EF', N'Kottamalli Thool', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'FA0C1127-76AF-42DE-9CC9-FE2E6A1BFF34')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'FA0C1127-76AF-42DE-9CC9-FE2E6A1BFF34', N'Manja Thool', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'0F4693EB-0FB9-4A6D-8B5F-6BCE617F82D7')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'0F4693EB-0FB9-4A6D-8B5F-6BCE617F82D7', N'Milaga Thool', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'CFDAEF83-EF1C-4014-B513-7A45669BA322')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'CFDAEF83-EF1C-4014-B513-7A45669BA322', N'Mutton Masala', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'9C5C9A96-F38B-4529-91FE-6F182F9136C0')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'9C5C9A96-F38B-4529-91FE-6F182F9136C0', N'Pepper', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B83B7E69-EF00-4B94-8555-CC3B3CC770D4')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B83B7E69-EF00-4B94-8555-CC3B3CC770D4', N'Rasam Powder', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'58D49BBE-080A-4034-81BD-BCCC4FB3715C')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'58D49BBE-080A-4034-81BD-BCCC4FB3715C', N'Sambar Powder', N'MASALA', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B6A2585E-EFA3-4A90-9F2D-2A47EA7E2D21')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B6A2585E-EFA3-4A90-9F2D-2A47EA7E2D21', N'Chicken', N'MEAT', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F10E9DAF-186A-49D8-9398-D84D3703DFB2')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F10E9DAF-186A-49D8-9398-D84D3703DFB2', N'Mutton', N'MEAT', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E432FE19-440B-43D9-A475-07DEA2CE3741')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E432FE19-440B-43D9-A475-07DEA2CE3741', N'Castor Oil', N'OILS', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'C9EA943F-3C77-44D8-88E0-40530EB3CE46')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'C9EA943F-3C77-44D8-88E0-40530EB3CE46', N'Cocout Oil', N'OILS', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7B6C8D82-4213-4E47-B5B3-B06115F1DAF8')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7B6C8D82-4213-4E47-B5B3-B06115F1DAF8', N'Deepam Oil', N'OILS', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'95A2FC3A-4589-49BE-BF1D-C85C39E58F35')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'95A2FC3A-4589-49BE-BF1D-C85C39E58F35', N'Groundnut Oil', N'OILS', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'15DF5AC5-DAFD-4BF7-A063-6441051647D5')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'15DF5AC5-DAFD-4BF7-A063-6441051647D5', N'Refined Oil', N'OILS', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'C9A2B927-A74E-49CB-9196-8147CDD7A3CD')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'C9A2B927-A74E-49CB-9196-8147CDD7A3CD', N'Sesame Oil', N'OILS', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'C84A9AD6-A3BD-4E73-9D4F-CDD14047B51B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'C84A9AD6-A3BD-4E73-9D4F-CDD14047B51B', N'Noodles', N'PACKETED_FOODS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'93CEB32A-FC20-4855-88CB-FBCF7539F4F2')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'93CEB32A-FC20-4855-88CB-FBCF7539F4F2', N'Sanitary pads', N'PERSONAL_CARE', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'722CAF15-E54D-40B6-81C5-B5AFDDE25819')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'722CAF15-E54D-40B6-81C5-B5AFDDE25819', N'Basmati arisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F5F24CE6-496D-4927-A508-B4454BEE725D')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F5F24CE6-496D-4927-A508-B4454BEE725D', N'Biryani arisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'4E6644FD-A604-4806-8A6B-4283D58F2401')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'4E6644FD-A604-4806-8A6B-4283D58F2401', N'Bullet arisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'CCB57049-F992-40E3-9EC8-C1533998869D')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'CCB57049-F992-40E3-9EC8-C1533998869D', N'Idli arisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'8E301989-4871-4BDC-85C7-4E66AFD7220F')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'8E301989-4871-4BDC-85C7-4E66AFD7220F', N'Karuppu kavuni', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'651380EC-D001-4FC3-8FC1-E77B209F2894')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'651380EC-D001-4FC3-8FC1-E77B209F2894', N'Pacharisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'FB2AE10D-62F7-42ED-8917-37582851C163')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'FB2AE10D-62F7-42ED-8917-37582851C163', N'Sappadu arisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'27C49B5B-AD4B-4737-BFE4-4C778C49AFB2')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'27C49B5B-AD4B-4737-BFE4-4C778C49AFB2', N'Seeraga samba arisi', N'RICE', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'82356EE7-974C-4F28-B3EF-1BC587D12559')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'82356EE7-974C-4F28-B3EF-1BC587D12559', N'Chilli Sauce', N'SAUCES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'F9144934-D7DF-4DD9-9EE6-9B017F50A173')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'F9144934-D7DF-4DD9-9EE6-9B017F50A173', N'Soya Sauce', N'SAUCES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'EFF06325-0C84-4204-B56C-AB1BB6A87B85')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'EFF06325-0C84-4204-B56C-AB1BB6A87B85', N'Tomato Sauce', N'SAUCES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7AA7223F-F011-4D38-B375-C796840B73C7')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7AA7223F-F011-4D38-B375-C796840B73C7', N'Fish', N'SEA_FOOD', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'73AC64AB-E480-44B7-B47B-5324FF0D97F5')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'73AC64AB-E480-44B7-B47B-5324FF0D97F5', N'Prawn', N'SEA_FOOD', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'A7A3BEA8-4FE0-429D-9BA2-A349E4CF73FF')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'A7A3BEA8-4FE0-429D-9BA2-A349E4CF73FF', N'Chocolate - Packeted', N'SNACKS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7D611F54-3579-40A6-A25A-A272CAA9E384')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7D611F54-3579-40A6-A25A-A272CAA9E384', N'Biryani leaf', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'505D6F1A-3B7D-44EF-8200-1FA314137A61')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'505D6F1A-3B7D-44EF-8200-1FA314137A61', N'Coffee Powder', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'1FCA9509-A2DF-4632-8DD4-B12C56B0D14E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'1FCA9509-A2DF-4632-8DD4-B12C56B0D14E', N'Elaikai', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D61EBF34-6F8A-4767-B75A-4F9AF59F0041')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D61EBF34-6F8A-4767-B75A-4F9AF59F0041', N'Jeeragam', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'3B500F5A-AAFA-4B55-97BD-1DDCD9BAA5A2')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'3B500F5A-AAFA-4B55-97BD-1DDCD9BAA5A2', N'Kadugu', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5B07F785-0DEA-4E24-A606-78DFC840F2D4')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5B07F785-0DEA-4E24-A606-78DFC840F2D4', N'Kasa kasa', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'8BD0A25A-AD79-496E-8879-65D93D4BDE2B')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'8BD0A25A-AD79-496E-8879-65D93D4BDE2B', N'Lavangam', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'1D282C0C-B005-4EE3-8F51-76575B8BC795')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'1D282C0C-B005-4EE3-8F51-76575B8BC795', N'Patta', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'2427F003-D549-489F-8C85-D00A9423AD65')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'2427F003-D549-489F-8C85-D00A9423AD65', N'Pepper', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E34FBCC3-378D-4928-9941-007DF3BBD562')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E34FBCC3-378D-4928-9941-007DF3BBD562', N'Salt', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'AB68E61D-F9A6-4248-875E-2680628F2CBB')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'AB68E61D-F9A6-4248-875E-2680628F2CBB', N'Sombu', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'BC81B3ED-3D1F-4DCC-B1C5-F3429837BF2F')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'BC81B3ED-3D1F-4DCC-B1C5-F3429837BF2F', N'Star', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'96F6BCA6-48DF-4FC5-A4AB-9B65886E6D72')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'96F6BCA6-48DF-4FC5-A4AB-9B65886E6D72', N'Sugar', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'7FB7A078-1074-4AF3-9FBF-D52C749FD85A')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'7FB7A078-1074-4AF3-9FBF-D52C749FD85A', N'Tea Powder', N'SPICES_&_CONDIMENTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'1D7CE8A6-A3E4-4F44-91E8-2E90D4244D86')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'1D7CE8A6-A3E4-4F44-91E8-2E90D4244D86', N'Banana flower', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5CF0A46C-F2AA-4494-AC5D-E935CDEC6F41')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5CF0A46C-F2AA-4494-AC5D-E935CDEC6F41', N'Beans', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'06C67EB8-7D7D-44F8-858C-0B2EF5DD3256')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'06C67EB8-7D7D-44F8-858C-0B2EF5DD3256', N'Beetroot', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DB47E3A4-4D5C-4C94-9C68-F8C817F0017E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DB47E3A4-4D5C-4C94-9C68-F8C817F0017E', N'Broccoli', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'AC9415DE-9F26-4872-8552-96239B91CD7E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'AC9415DE-9F26-4872-8552-96239B91CD7E', N'Cabbage', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5F1464A8-E132-4DB0-9512-1C030D45588A')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5F1464A8-E132-4DB0-9512-1C030D45588A', N'Carrot', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5925FF9E-CFC7-48D4-8C85-91D66846352A')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5925FF9E-CFC7-48D4-8C85-91D66846352A', N'Cauliflower', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B86C4DDA-A7BA-4066-8A29-7404FFBC47B6')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B86C4DDA-A7BA-4066-8A29-7404FFBC47B6', N'Chickpeas', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'A15AEE66-F85F-4ADE-8B58-27FA04448D43')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'A15AEE66-F85F-4ADE-8B58-27FA04448D43', N'Corn', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'9FEA8EF1-A128-45FF-A78D-7652932CD9CB')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'9FEA8EF1-A128-45FF-A78D-7652932CD9CB', N'DrumStick', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'65F3AC3D-10F6-4BE6-9636-5FAE7B07F1D4')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'65F3AC3D-10F6-4BE6-9636-5FAE7B07F1D4', N'Garlic', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D807BD18-9154-4A32-9F5C-66ACF61602D1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D807BD18-9154-4A32-9F5C-66ACF61602D1', N'Ginger', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'A60C289A-6A2B-4116-BEF6-225EB73C38A1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'A60C289A-6A2B-4116-BEF6-225EB73C38A1', N'Green Chilli', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'89535019-C330-4674-BB64-4E87377E62AC')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'89535019-C330-4674-BB64-4E87377E62AC', N'Green peas', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'594088BE-6E01-42F0-A75A-36482F4B8C2F')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'594088BE-6E01-42F0-A75A-36482F4B8C2F', N'Lady finger', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'3EE1C10B-C79B-40A9-96CA-003F8777CE02')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'3EE1C10B-C79B-40A9-96CA-003F8777CE02', N'Onion - Big', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'15A73C04-BF10-461B-BD0C-FFCE4275EDF1')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'15A73C04-BF10-461B-BD0C-FFCE4275EDF1', N'Onion - small', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'35A0C1A5-A3A1-4E5E-990B-28B78ED6F4A9')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'35A0C1A5-A3A1-4E5E-990B-28B78ED6F4A9', N'Peppers', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'504A7412-80F1-4BC1-BFA6-8A5E479F41CC')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'504A7412-80F1-4BC1-BFA6-8A5E479F41CC', N'Potato', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'826B31D3-78B5-4DA1-94E0-8CF25A2D1C3C')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'826B31D3-78B5-4DA1-94E0-8CF25A2D1C3C', N'Pumpkin', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'DA0EBAF5-CEDC-4792-8A18-4A3B78A511FD')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'DA0EBAF5-CEDC-4792-8A18-4A3B78A511FD', N'Radish', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'A0045DC3-8590-4CF7-8233-26114DA4FE3F')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'A0045DC3-8590-4CF7-8233-26114DA4FE3F', N'Red Chilli', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'B7B3DFAC-D0F9-4446-9BB0-6FA44A180845')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'B7B3DFAC-D0F9-4446-9BB0-6FA44A180845', N'Spring onion', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E3FE3DB5-8681-435F-82AC-223DCFF0D407')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E3FE3DB5-8681-435F-82AC-223DCFF0D407', N'Tomato', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'73B42591-12FE-4053-844A-D5054A8EF3DB')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'73B42591-12FE-4053-844A-D5054A8EF3DB', N'Turmeric', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'68B9A0FA-B9B0-4B3F-A5D7-3AD7E80A89DD')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'68B9A0FA-B9B0-4B3F-A5D7-3AD7E80A89DD', N'Vaazhai poo', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D2AC8501-2F0F-40CE-BA46-3B7B3553248E')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D2AC8501-2F0F-40CE-BA46-3B7B3553248E', N'White radish', N'VEGETABLES', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'5CD6DAF1-6C7E-404A-9445-F50C1708BF34')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'5CD6DAF1-6C7E-404A-9445-F50C1708BF34', N'Coconut', N'VEGETABLES', NULL, N'AS_NEEDED', N'C', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'E70CBF48-0B19-4F76-8050-9B21C0104827')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'E70CBF48-0B19-4F76-8050-9B21C0104827', N'Batter', N'READY_TO_COOK_PRODUCTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'D1710784-7357-484A-B8A4-7646C1CFED90')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'D1710784-7357-484A-B8A4-7646C1CFED90', N'Maggie', N'READY_TO_COOK_PRODUCTS', NULL, N'AS_NEEDED', N'W', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
    END
    IF NOT EXISTS(SELECT 1 FROM [Setup].[GroceryInfo] WHERE [Id] LIKE N'9022850D-6022-47F0-8516-57D038B99391')
    BEGIN
        INSERT INTO [Setup].[GroceryInfo]
        ([Id], [Name], [GroceryCategory], [BrandId], [PreferredRecurringTimePeriod], [MetricSystem], [CreatedBy])
        VALUES
        (N'9022850D-6022-47F0-8516-57D038B99391', N'Icecream', N'DAIRY', NULL, N'AS_NEEDED', N'L', N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5')
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