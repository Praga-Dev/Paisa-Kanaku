CREATE TABLE [Transactions].[LoanInterestPaymentInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[LoanInfoId] UNIQUEIDENTIFIER NOT NULL, 
	[PaidInterestAmount] DECIMAL(10,2) NOT NULL, -- TODO Trigger, If Pending Need to update on Outstanding Interest Amount in LoanInfo TBL
	[PaidDate] DATETIME2 NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_LoanInterestPaymentInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_LoanInterestPaymentInfo_LoanInfoId] FOREIGN KEY ([LoanInfoId]) REFERENCES [Transactions].[LoanInfo]([Id]),
	CONSTRAINT [FK_LoanInterestPaymentInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_LoanInterestPaymentInfo_SequenceId] ON [Transactions].[LoanInterestPaymentInfo] ([SequenceId])