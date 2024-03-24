CREATE TABLE [Transactions].[ExpenseUtilityBaseInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,	
	[UtilityId] UNIQUEIDENTIFIER NOT NULL,
	[ConsumerType] NVARCHAR(25) NOT NULL,
	[ServiceDuration] NVARCHAR(15) NOT NULL,
	[FromDate] DATETIME2 NULL,
	[ToDate] DATETIME2 NULL,
	[ExpenseById] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ExpenseAmount] DECIMAL(12,3) NOT NULL, -- The Rate in the product may differ from the actual expense
	[Description] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_ExpenseUtilityBaseInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_UtilityId] FOREIGN KEY ([UtilityId]) REFERENCES [Setup].[UtilityInfo]([Id]),	
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_ConsumerType] FOREIGN KEY ([ConsumerType]) REFERENCES [Lookups].[ConsumerTypeInfo]([ConsumerType]),
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_ServiceDuration] FOREIGN KEY ([ServiceDuration]) REFERENCES [Lookups].[TimePeriodTypeInfo]([TimePeriodType]),
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_ExpenseById] FOREIGN KEY ([ExpenseById]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_ExpenseUtilityBaseInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus]),
) ON [PRIMARY]
GO
