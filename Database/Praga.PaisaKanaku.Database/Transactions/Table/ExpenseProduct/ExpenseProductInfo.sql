CREATE TABLE [Transactions].[ExpenseProductInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseInfoId] UNIQUEIDENTIFIER NOT NULL,
	[ProductInfoId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseById] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[Quantity] INT NOT NULL DEFAULT 1, -- If Applicable else 1
	[ExpenseAmount] DECIMAL(12,3) NOT NULL, -- The Rate in the product may differ from the actual expense
	[Description] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_ExpenseProductInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ExpenseProductInfo_ExpenseInfo] FOREIGN KEY ([ExpenseInfoId]) REFERENCES [Transactions].[ExpenseInfo]([Id]),
	CONSTRAINT [FK_ExpenseProductInfo_ProductInfo] FOREIGN KEY ([ProductInfoId]) REFERENCES [Setup].[ProductInfo]([Id]),
	CONSTRAINT [FK_ExpenseProductInfo_MemberInfo] FOREIGN KEY ([ExpenseById]) REFERENCES [Setup].[MemberInfo]([Id]),
	CONSTRAINT [FK_ExpenseProductInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
