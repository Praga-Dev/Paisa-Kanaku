CREATE TABLE [Transactions].[ExpenseOutdoorFoodInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseInfoId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ExpenseById] UNIQUEIDENTIFIER NOT NULL,
	[OutdoorFoodVendorId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseAmount] DECIMAL(12,3) NOT NULL,
	[Description] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_ExpenseOutdoorFoodInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ExpenseOutdoorFoodInfo_ExpenseInfo] FOREIGN KEY ([ExpenseInfoId]) REFERENCES [Transactions].[ExpenseInfo]([Id]),
	CONSTRAINT [FK_ExpenseOutdoorFoodInfo_MemberInfo] FOREIGN KEY ([ExpenseById]) REFERENCES [Setup].[MemberInfo]([Id]),
	CONSTRAINT [FK_ExpenseOutdoorFoodInfo_OutdoorFoodVendorInfo] FOREIGN KEY ([OutdoorFoodVendorId]) REFERENCES [Setup].[OutdoorFoodVendorInfo]([Id]),
	CONSTRAINT [FK_ExpenseOutdoorFoodInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
