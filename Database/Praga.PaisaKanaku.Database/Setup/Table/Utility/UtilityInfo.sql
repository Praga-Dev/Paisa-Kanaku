CREATE TABLE [Setup].[UtilityInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[ConsumerType] NVARCHAR(25) NOT NULL,
	[RecurringType] NVARCHAR(15) NOT NULL,
	[IsEssential] BIT DEFAULT 0,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_UtilityInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_UtilityInfo_ConsumerType] FOREIGN KEY ([ConsumerType]) REFERENCES [Lookups].[ConsumerTypeInfo]([ConsumerType]),
	CONSTRAINT [FK_UtilityInfo_RecurringType] FOREIGN KEY ([RecurringType]) REFERENCES [Lookups].[TimePeriodTypeInfo]([TimePeriodType]),
	CONSTRAINT [FK_UtilityInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus]),

) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_UtilityInfo_SequenceId] ON [Setup].[UtilityInfo] ([SequenceId])
