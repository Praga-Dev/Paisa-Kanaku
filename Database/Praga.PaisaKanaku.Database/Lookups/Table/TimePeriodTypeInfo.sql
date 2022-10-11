CREATE TABLE [Lookups].[TimePeriodTypeInfo]
(
	[TimePeriodType] NVARCHAR(10) NOT NULL,
	[TimePeriodTypeValue] NVARCHAR(10) NOT NULL,
	[SequenceId] INT NOT NULL,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_TimePeriodTypeInfo] PRIMARY KEY CLUSTERED 
	(
		[TimePeriodType] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	CONSTRAINT [FK_TimePeriodTypeInfo_RowStatusInfo] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY];
