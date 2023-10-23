CREATE TABLE [Lookups].[MeasureTypeInfo]
(
	[MeasureType] NVARCHAR(25) NOT NULL,
	[MeasureTypeValue] NVARCHAR(25) NOT NULL,
	[MetricSystem] NVARCHAR(1) NOT NULL,
	[SequenceId] INT NOT NULL,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_MeasureTypeInfo] PRIMARY KEY CLUSTERED 
	(
		[MeasureType] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	CONSTRAINT [FK_MeasureTypeInfo_MetricSystemInfo] FOREIGN KEY ([MetricSystem]) REFERENCES [Lookups].[MetricSystemInfo]([MetricSystem]),
	CONSTRAINT [FK_MeasureTypeInfo_RowStatusInfo] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY];