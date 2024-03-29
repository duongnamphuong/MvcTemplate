USE [Authorize]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Uname] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HashSaltBase64] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HashTypeId] [int] NOT NULL,
	[PasswordHashBase64] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsTwoFactor] [bit] NOT NULL,
	[TwoFactorSecretBase32] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clipboard](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Content] [nvarchar](max) COLLATE Japanese_90_BIN2 NOT NULL,
 CONSTRAINT [PK_Clipboard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HashType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_HashType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenIssued](
	[AccountId] [bigint] NOT NULL,
	[Val] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IssuedAtUtc] [datetime] NOT NULL,
	[ExpireAtUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC,
	[Val] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (7, N'HMACMD5')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (8, N'HMACRIPEMD160')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (9, N'HMACSHA1')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (10, N'HMACSHA256')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (11, N'HMACSHA384')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (12, N'HMACSHA512')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (1, N'MD5')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (2, N'RIPEMD160')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (3, N'SHA1')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (4, N'SHA256')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (5, N'SHA384')
INSERT [dbo].[HashType] ([Id], [Name]) VALUES (6, N'SHA512')
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [UC_AccountUname] UNIQUE NONCLUSTERED 
(
	[Uname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[HashType] ADD  CONSTRAINT [UC_HashTypeName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_HashType] FOREIGN KEY([HashTypeId])
REFERENCES [dbo].[HashType] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_HashType]
GO
ALTER TABLE [dbo].[Clipboard]  WITH CHECK ADD  CONSTRAINT [FK_Clipboard_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Clipboard] CHECK CONSTRAINT [FK_Clipboard_Account]
GO
ALTER TABLE [dbo].[TokenIssued]  WITH CHECK ADD  CONSTRAINT [FK_Token_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[TokenIssued] CHECK CONSTRAINT [FK_Token_Account]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [CONDITION_TWO_FACTOR_VS_SECRET_BASE32] CHECK  (([IsTwoFactor]=(1) AND [TwoFactorSecretBase32] IS NOT NULL OR [IsTwoFactor]=(0) AND [TwoFactorSecretBase32] IS NULL))
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [CONDITION_TWO_FACTOR_VS_SECRET_BASE32]
GO
