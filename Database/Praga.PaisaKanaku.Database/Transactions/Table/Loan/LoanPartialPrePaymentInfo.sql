CREATE TABLE [Transactions].[LoanPartialPrepaymentInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[LoanInfoId] UNIQUEIDENTIFIER NOT NULL, 
	[PartialPrepaymentAmount] DECIMAL(10,2) NOT NULL, -- TODO add triggers to loaninfo to update outstanding amount  in LoanInfo
	[PaidDate] DATETIME2 NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_LoanPartialPrepaymentInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_LoanPartialPrepaymentInfo_LoanInfoId] FOREIGN KEY ([LoanInfoId]) REFERENCES [Transactions].[LoanInfo]([Id]),
	CONSTRAINT [FK_LoanPartialPrepaymentInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_LoanPartialPrepaymentInfo_SequenceId] ON [Transactions].[LoanPartialPrepaymentInfo] ([SequenceId])