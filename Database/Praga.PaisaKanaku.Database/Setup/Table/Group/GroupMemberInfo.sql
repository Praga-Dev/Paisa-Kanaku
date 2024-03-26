CREATE TABLE [Setup].[GroupMemberInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[GroupId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
	[SequenceId] INT NOT NULL IDENTITY,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Setup_GroupMemberInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_GroupMemberInfo_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Setup].[GroupInfo]([Id]),
	CONSTRAINT [FK_GroupMemberInfo_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Setup].[MemberInfo]([Id]),	
	CONSTRAINT [FK_GroupMemberInfo_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_GroupMemberInfo_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_GroupMemberInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus]),
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [IX_GroupMemberInfo_SequenceId] ON [Setup].[GroupMemberInfo] ([SequenceId])
