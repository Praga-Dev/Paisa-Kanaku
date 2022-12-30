CREATE TABLE [Transactions].[CollateralInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(250) NOT NULL, -- TODO ASSET : IMG / PDF
	[CollateralType] NVARCHAR(25) NOT NULL,
	[EstimatedValue] DECIMAL(10,2) NOT NULL,
	[CollateralOwnerInfoId] UNIQUEIDENTIFIER NOT NULL,
	[Comments] NVARCHAR(250) NULL, 
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_CollateralInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_CollateralInfo_CollateralType] FOREIGN KEY ([CollateralType]) REFERENCES [Lookups].[CollateralTypeInfo]([CollateralType]),
	CONSTRAINT [FK_CollateralInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus]),
	CONSTRAINT [FK_CollateralInfo_CollateralOwnerInfoId] FOREIGN KEY ([CollateralOwnerInfoId]) REFERENCES [Transactions].[CollateralOwnerInfo]([Id])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_CollateralInfo_SequenceId] ON [Transactions].[CollateralInfo] ([SequenceId])