﻿CREATE TABLE [Setup].[LenderInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[PrincipalAmount] DECIMAL(10,2) NOT NULL,
	[InterestAmount] DECIMAL(8,2) NOT NULL,
	[OutstandingBalance] DECIMAL(10,2) NOT NULL,
	[LendedMemberId] UNIQUEIDENTIFIER NOT NULL,
	[LendDate] DATETIME2 NOT NULL,
	[Comments] NVARCHAR(250),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_LenderInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_LenderInfo_MemberId] FOREIGN KEY ([LendedMemberId]) REFERENCES [Setup].[MemberInfo]([Id]),
	CONSTRAINT [FK_LenderInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus])
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_BrandInfo_SequenceId] ON [Setup].[LenderInfo] ([SequenceId])