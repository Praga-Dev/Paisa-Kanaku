CREATE TABLE [Transactions].[ExpenseReferenceDetailInfo](
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseInfoId] UNIQUEIDENTIFIER NOT NULL,
	[ReferenceId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseBy] UNIQUEIDENTIFIER NOT NULL,
	[DateOfExpense] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ExpenseAmount] DECIMAL(12,3) NOT NULL, -- The Rate in the product may differ from the actual expense
	[Description] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_ExpenseReferenceDetailInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ExpenseReferenceDetailInfo_ExpenseInfo] FOREIGN KEY ([ExpenseInfoId]) REFERENCES [Transactions].[ExpenseInfo]([Id]),
	CONSTRAINT [FK_ExpenseReferenceDetailInfo_MemberInfo] FOREIGN KEY ([ExpenseBy]) REFERENCES [Setup].[MemberInfo]([Id]),
	CONSTRAINT [FK_ExpenseReferenceDetailInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO