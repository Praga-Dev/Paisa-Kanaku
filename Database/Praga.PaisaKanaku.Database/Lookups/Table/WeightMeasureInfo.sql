CREATE TABLE [Lookups].[WeightMeasureInfo]
(
	[WeightMeasure] NVARCHAR(2) NOT NULL,
	[WeightMeasureValue] NVARCHAR(15) NOT NULL,
	[ConversionValue] DECIMAL(6,2) NOT NULL,
	[SequenceId] INT NOT NULL,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_WeightMeasureInfo] PRIMARY KEY CLUSTERED 
	(
		[WeightMeasure] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	CONSTRAINT [FK_WeightMeasureInfo_RowStatusInfo] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY];
