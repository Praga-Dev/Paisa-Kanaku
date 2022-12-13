CREATE TABLE [Transactions].[LoanInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[LoanType] NVARCHAR(25) NOT NULL DEFAULT 'PERSONAL_LOAN',
	[LoanAmount] DECIMAL(10,2) NOT NULL,
	[InterestAmount] DECIMAL(8,2) NOT NULL,
	[LendedMemberId] UNIQUEIDENTIFIER NOT NULL,
	[BorrowedDate] DATETIME2 NOT NULL,
	[OutstandingBalance] DECIMAL(10,2) NOT NULL,
	[InterestDueDate] DATETIME2 NOT NULL,
	[OutstandingInterestAmount] DECIMAL(10,2) NOT NULL DEFAULT 0, -- Pending Interest Amount
	[LoanRepaymentDate] DATETIME2 NOT NULL,
	[LoanStatus] NVARCHAR(10) NOT NULL DEFAULT 'OPEN',
	[GracePeriodDate] DATETIME2 NULL, -- Optional
	[LateFee] DECIMAL(8,2) NOT NULL DEFAULT 0, -- Optional
	[Comments] NVARCHAR(250),
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_LoanInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_LoanInfo_MemberId] FOREIGN KEY ([LendedMemberId]) REFERENCES [Setup].[MemberInfo]([Id]),
	CONSTRAINT [FK_LoanInfo_LoanType] FOREIGN KEY ([LoanType]) REFERENCES [Lookups].[LoanTypeInfo]([LoanType]),
	CONSTRAINT [FK_LoanInfo_LoanStatus] FOREIGN KEY ([LoanStatus]) REFERENCES [Lookups].[LoanStatusInfo]([LoanStatus]),
	CONSTRAINT [FK_LoanInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_LoanInfo_SequenceId] ON [Transactions].[LoanInfo] ([SequenceId])