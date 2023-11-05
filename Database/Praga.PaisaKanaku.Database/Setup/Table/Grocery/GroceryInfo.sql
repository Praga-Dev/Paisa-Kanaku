CREATE TABLE [Setup].[GroceryInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	[GroceryCategory] NVARCHAR(25) NOT NULL,
	[BrandId] UNIQUEIDENTIFIER NULL,
	[MetricSystem] NVARCHAR(1) NOT NULL,
	[MeasureType] NVARCHAR(25) NOT NULL, -- TODO ADD A TABLE TO SUPPORT MULTI-MEASURE TYPE GROCERY ITEMS
	[PreferredRecurringTimePeriod] NVARCHAR(15) NOT NULL DEFAULT 'NA',
	--[Price] DECIMAL(12,3) NOT NULL, -- Added on GroceryExpenseTable
	--[Description] NVARCHAR(250),
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_GroceryInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_GroceryInfo_GroceryCategory] FOREIGN KEY ([GroceryCategory]) REFERENCES [Lookups].[GroceryCategoryInfo]([GroceryCategory]),
	CONSTRAINT [FK_GroceryInfo_Brand] FOREIGN KEY ([BrandId]) REFERENCES [Setup].[BrandInfo]([Id]),
	CONSTRAINT [FK_GroceryInfo_TimePeriodType] FOREIGN KEY ([PreferredRecurringTimePeriod]) REFERENCES [Lookups].[TimePeriodTypeInfo]([TimePeriodType]),
	CONSTRAINT [FK_GroceryInfo_MetricSystem] FOREIGN KEY ([MetricSystem]) REFERENCES [Lookups].[MetricSystemInfo]([MetricSystem]),
	CONSTRAINT [FK_GroceryInfo_MeasureType] FOREIGN KEY ([MeasureType]) REFERENCES [Lookups].[MeasureTypeInfo]([MeasureType]),
	CONSTRAINT [FK_GroceryInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_GroceryInfo_SequenceId] ON [Setup].[GroceryInfo] ([SequenceId])