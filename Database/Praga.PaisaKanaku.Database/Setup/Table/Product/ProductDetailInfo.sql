CREATE TABLE [Setup].[ProductDetailInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[ProductId] UNIQUEIDENTIFIER NOT NULL,
	[BrandId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseType] NVARCHAR(25) NOT NULL,
	[Price] DECIMAL(12,3) NOT NULL,
	[Description] NVARCHAR(250),
	[PreferredRecurringTimePeriod] NVARCHAR(10) NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_ProductDetailInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ProductDetailInfo_Product] FOREIGN KEY ([ProductId]) REFERENCES [Setup].[ProductInfo]([Id]),
	CONSTRAINT [FK_ProductDetailInfo_Brand] FOREIGN KEY ([BrandId]) REFERENCES [Setup].[BrandInfo]([Id]),
	CONSTRAINT [FK_ProductDetailInfo_ExpenseType] FOREIGN KEY ([ExpenseType]) REFERENCES [Lookups].[ExpenseTypeInfo]([ExpenseType]),
	CONSTRAINT [FK_ProductDetailInfo_TimePeriod] FOREIGN KEY ([PreferredRecurringTimePeriod]) REFERENCES [Lookups].[TimePeriodInfo]([TimePeriod]),
	CONSTRAINT [FK_ProductDetailInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_ProductDetailInfo_SequenceId] ON [Setup].[ProductDetailInfo] ([SequenceId])