CREATE TABLE [Transactions].[ExpenseMemberUtilityInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,	
	[ExpenseUtilityId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] UNIQUEIDENTIFIER NULL,
	[ModifiedDate] DATETIME2,
	[RowStatus] NVARCHAR(1) NOT NULL DEFAULT 'A'
	CONSTRAINT [PK_Transactions_ExpenseMemberUtilityInfo] PRIMARY KEY NONCLUSTERED
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_ExpenseMemberUtilityInfo_ExpenseUtilityId] FOREIGN KEY ([ExpenseUtilityId]) REFERENCES [Transactions].[ExpenseUtilityBaseInfo]([Id]),	
	CONSTRAINT [FK_ExpenseMemberUtilityInfo_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Setup].[MemberInfo]([Id]),	
	CONSTRAINT [FK_ExpenseMemberUtilityInfo_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_ExpenseMemberUtilityInfo_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [Auth].[UserInfo]([Id]),
	CONSTRAINT [FK_ExpenseMemberUtilityInfo_RowStatus] FOREIGN KEY ([RowStatus]) REFERENCES [Lookups].[RowStatusInfo]([RowStatus]),
) ON [PRIMARY]
GO
