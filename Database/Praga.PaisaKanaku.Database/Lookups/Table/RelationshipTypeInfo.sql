CREATE TABLE [Lookups].[RelationshipTypeInfo]
(
	[RelationshipType] NVARCHAR(25) NOT NULL,
	[RelationshipTypeValue] NVARCHAR(25) NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_RelationshipTypeInfo] PRIMARY KEY NONCLUSTERED 
	(
		[RelationshipType] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_RelationshipTypeInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_RelationshipTypeInfo_SequenceId] ON [Lookups].[RelationshipTypeInfo] ([SequenceId])
