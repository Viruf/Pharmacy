USE [master]
GO
/****** Object:  Database [Pharmacy]    Script Date: 16.11.2023 23:13:14 ******/
CREATE DATABASE [Pharmacy]
GO
USE [Pharmacy]
GO
/****** Object:  Table [dbo].[Lots]    Script Date: 16.11.2023 23:13:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lots](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[StorageID] [int] NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_ProductParties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pharmacies]    Script Date: 16.11.2023 23:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pharmacies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PharmacyName] [nvarchar](100) NULL,
	[PharmacyAddres] [nvarchar](300) NULL,
	[PharmacyPhones] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 16.11.2023 23:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Storages]    Script Date: 16.11.2023 23:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Storages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PharmacyID] [int] NULL,
	[StorageName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Storages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lots] ADD  CONSTRAINT [DF_ProductParties_ProductQuantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Storages]  WITH CHECK ADD  CONSTRAINT [FK_Storages_Storages] FOREIGN KEY([ID])
REFERENCES [dbo].[Storages] ([ID])
GO
ALTER TABLE [dbo].[Storages] CHECK CONSTRAINT [FK_Storages_Storages]
GO
USE [master]
GO
ALTER DATABASE [Pharmacy] SET  READ_WRITE 
GO
