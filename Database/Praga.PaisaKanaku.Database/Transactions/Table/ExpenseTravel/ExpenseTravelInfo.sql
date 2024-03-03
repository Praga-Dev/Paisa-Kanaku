CREATE TABLE [Transactions].[ExpenseTravelInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseInfoId] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseById] UNIQUEIDENTIFIER NOT NULL,
	[ExpenseDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[Source] NVARCHAR(250) NOT NULL,
	[Destination] NVARCHAR(250) NOT NULL,
	[TravelDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ExpenseAmount] DECIMAL(12,3) NOT NULL,
	[TransportMode] NVARCHAR(15),
	[TravelService] NVARCHAR(15),
	[Description] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_ExpenseTravelInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ExpenseTravelInfo_ExpenseInfo] FOREIGN KEY ([ExpenseInfoId]) REFERENCES [Transactions].[ExpenseInfo]([Id]),
	CONSTRAINT [FK_ExpenseTravelInfo_TransportModeInfo] FOREIGN KEY ([TransportMode]) REFERENCES [Lookups].[TransportModeInfo]([TransportMode]),
	CONSTRAINT [FK_ExpenseTravelInfo_TravelServiceInfo] FOREIGN KEY ([TravelService]) REFERENCES [Lookups].[TravelServiceInfo]([TravelService]),
	CONSTRAINT [FK_ExpenseTravelInfo_MemberInfo] FOREIGN KEY ([ExpenseById]) REFERENCES [Setup].[MemberInfo]([Id]),
	CONSTRAINT [FK_ExpenseTravelInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
