CREATE TABLE [Lookups].[TransportModeInfo]
(
	[TransportMode] NVARCHAR(15) NOT NULL,
	[TransportModeValue] NVARCHAR(15) NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_TransportModeInfo] PRIMARY KEY NONCLUSTERED 
	(
		[TransportMode] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_TransportModeInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_TransportModeInfo_SequenceId] ON [Lookups].[TransportModeInfo] ([SequenceId])
