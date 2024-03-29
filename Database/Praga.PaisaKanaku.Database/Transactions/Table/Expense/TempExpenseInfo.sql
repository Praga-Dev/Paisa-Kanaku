﻿CREATE TABLE [Transactions].[TempExpenseInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
	[Date] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ProductId] UNIQUEIDENTIFIER NOT NULL, -- ReferenceId, because there are multiple types of expense
	[Quantity] INT NOT NULL DEFAULT 1,  -- If Applicable else 1
	[Amount] DECIMAL(12,3) NOT NULL,
	[Description] NVARCHAR(250),
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_TempExpenseInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_TempExpenseInfo_Product] FOREIGN KEY ([ProductId]) REFERENCES [Setup].[ProductInfo]([Id]),
	CONSTRAINT [FK_TempExpenseInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_TempExpenseInfo_SequenceId] ON [Transactions].[TempExpenseInfo] ([SequenceId])
