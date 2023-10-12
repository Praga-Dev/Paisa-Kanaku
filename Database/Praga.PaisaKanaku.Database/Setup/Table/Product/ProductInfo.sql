CREATE TABLE [Setup].[ProductInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	[ProductCategoryId] UNIQUEIDENTIFIER NOT NULL,
	[BrandId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseType] NVARCHAR(25) NOT NULL,
	[Price] DECIMAL(12,3) NOT NULL,
	[Description] NVARCHAR(250),
	[PreferredRecurringTimePeriod] NVARCHAR(15) NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_ProductInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ProductInfo_Product] FOREIGN KEY ([ProductCategoryId]) REFERENCES [Setup].[ProductCategoryInfo]([Id]),
	CONSTRAINT [FK_ProductInfo_Brand] FOREIGN KEY ([BrandId]) REFERENCES [Setup].[BrandInfo]([Id]),
	CONSTRAINT [FK_ProductInfo_ExpenseType] FOREIGN KEY ([ExpenseType]) REFERENCES [Lookups].[ExpenseTypeInfo]([ExpenseType]),
	CONSTRAINT [FK_ProductInfo_TimePeriodType] FOREIGN KEY ([PreferredRecurringTimePeriod]) REFERENCES [Lookups].[TimePeriodTypeInfo]([TimePeriodType]),
	CONSTRAINT [FK_ProductInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_ProductInfo_SequenceId] ON [Setup].[ProductInfo] ([SequenceId])