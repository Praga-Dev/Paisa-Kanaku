CREATE TABLE [Setup].[LoanPartialPrePaymentInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[LoanInfoId] UNIQUEIDENTIFIER NOT NULL,
	[PartialPrepaymentAmount] DECIMAL(10,2) NOT NULL,
	[PaidDate] DATETIME2 NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_LoanPartialPrePaymentInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_LoanPartialPrePaymentInfo_LoanInfoId] FOREIGN KEY ([LoanInfoId]) REFERENCES [Setup].[LoanInfo]([Id]),
	CONSTRAINT [FK_LoanPartialPrePaymentInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_LoanPartialPrePaymentInfo_SequenceId] ON [Setup].[LoanPartialPrePaymentInfo] ([SequenceId])
