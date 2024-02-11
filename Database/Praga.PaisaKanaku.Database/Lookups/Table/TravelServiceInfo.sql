CREATE TABLE [Lookups].[TravelServiceInfo]
(
	[TravelService] NVARCHAR(15) NOT NULL,
	[TravelServiceValue] NVARCHAR(15) NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_TravelServiceInfo] PRIMARY KEY NONCLUSTERED 
	(
		[TravelService] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_TravelServiceInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_TravelServiceInfo_SequenceId] ON [Lookups].[TravelServiceInfo] ([SequenceId])
