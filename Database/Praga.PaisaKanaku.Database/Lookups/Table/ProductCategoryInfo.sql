CREATE TABLE [Lookups].[ProductCategoryInfo]
(
	[ProductCategory] NVARCHAR(25) NOT NULL,
	[ProductCategoryValue] NVARCHAR(25) NOT NULL,
	[SequenceId] INT NOT NULL,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Lookups_ProductCategoryInfo] PRIMARY KEY CLUSTERED 
	(
		[ProductCategory] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	CONSTRAINT [FK_ProductCategoryInfo_RowStatusInfo] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY];
