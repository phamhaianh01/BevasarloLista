USE [BevasarloLista]
GO

/****** Object:  Table [dbo].[Items]    Script Date: 2024-12-10 05:43:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Amount] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[ForId] [int] NULL,
	[CheckedById] [int] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Users] FOREIGN KEY([ForId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Users]
GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Users1] FOREIGN KEY([CheckedById])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Users1]
GO

