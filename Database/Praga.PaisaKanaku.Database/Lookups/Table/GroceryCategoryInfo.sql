CREATE TABLE [Lookups].[GroceryCategoryInfo]
(
	[GroceryCategory] NVARCHAR(25) NOT NULL,
	[GroceryCategoryValue] NVARCHAR(25) NOT NULL,
	[SequenceId] INT NOT NULL,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_GroceryCategoryInfo] PRIMARY KEY CLUSTERED 
	(
		[GroceryCategory] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	CONSTRAINT [FK_GroceryCategoryInfo_RowStatusInfo] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY];
