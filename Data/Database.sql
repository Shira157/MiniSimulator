CREATE DATABASE [MiniSim]

USE [MiniSim]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 11/07/2024 11:51:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Strength] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Team] ON 
GO
INSERT [dbo].[Team] ([Id], [Name], [Strength], [Image]) VALUES (1, N'Team A', 70, N'TeamA.jpg')
GO
INSERT [dbo].[Team] ([Id], [Name], [Strength], [Image]) VALUES (2, N'Team B', 60, N'TeamB.jpg')
GO
INSERT [dbo].[Team] ([Id], [Name], [Strength], [Image]) VALUES (3, N'Team C', 50, N'TeamC.jpg')
GO
INSERT [dbo].[Team] ([Id], [Name], [Strength], [Image]) VALUES (4, N'Team D', 40, N'TeamD.jpg')
GO
SET IDENTITY_INSERT [dbo].[Team] OFF
GO
