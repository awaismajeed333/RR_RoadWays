USE [master]
GO
/****** Object:  Database [RRRoadwaysDB]    Script Date: 23/02/2021 10:55:27 PM ******/
CREATE DATABASE [RRRoadwaysDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RRRoadwaysDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RRRoadwaysDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RRRoadwaysDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RRRoadwaysDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RRRoadwaysDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RRRoadwaysDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RRRoadwaysDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RRRoadwaysDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RRRoadwaysDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RRRoadwaysDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RRRoadwaysDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RRRoadwaysDB] SET  MULTI_USER 
GO
ALTER DATABASE [RRRoadwaysDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RRRoadwaysDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RRRoadwaysDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RRRoadwaysDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RRRoadwaysDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [RRRoadwaysDB]
GO
/****** Object:  Table [dbo].[City]    Script Date: 23/02/2021 10:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 23/02/2021 10:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ContactPerson] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Station]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Station](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[OwnerName] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[StationType] [varchar](50) NULL,
	[CityId] [int] NULL,
 CONSTRAINT [PK_Pump] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleNumber] [varchar](50) NOT NULL,
	[OwnerName] [varchar](50) NOT NULL,
	[OwnerContactNumber] [varchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Voucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleNumber] [int] NULL,
	[Month] [varchar](50) NOT NULL,
	[UpDate] [date] NULL,
	[UpFrom] [int] NULL,
	[UpTo] [int] NULL,
	[UpAmount] [decimal](18, 0) NULL,
	[DownReturnDate] [date] NULL,
	[DownFrom] [int] NULL,
	[DownTo] [int] NULL,
	[DownDescription] [varchar](50) NULL,
	[DownAmount] [decimal](18, 0) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [int] NULL,
	[OilShopId] [int] NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VoucherDieselDetails]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherDieselDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerialNo] [int] NOT NULL,
	[Date] [date] NULL,
	[StationId] [int] NULL,
	[Litre] [int] NULL,
	[Rate] [decimal](18, 0) NULL,
	[OilAndOthers] [decimal](18, 0) NULL,
	[Amount] [decimal](18, 0) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[VoucherId] [int] NULL,
 CONSTRAINT [PK_VoucherDieselDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VoucherOthersExpenses]    Script Date: 23/02/2021 10:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VoucherOthersExpenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerialNo] [int] NOT NULL,
	[OthersExpense] [varchar](50) NULL,
	[Amount] [decimal](18, 0) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[VoucherId] [int] NULL,
 CONSTRAINT [PK_VoucherOthersExpenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Station]  WITH CHECK ADD  CONSTRAINT [FK_Station_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Station] CHECK CONSTRAINT [FK_Station_City]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_City] FOREIGN KEY([UpTo])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_City]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_City1] FOREIGN KEY([DownFrom])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_City1]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_City2] FOREIGN KEY([DownTo])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_City2]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Company] FOREIGN KEY([UpFrom])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Company]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Station] FOREIGN KEY([OilShopId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Station]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Users] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Users]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Vehicle1] FOREIGN KEY([VehicleNumber])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Vehicle1]
GO
ALTER TABLE [dbo].[VoucherDieselDetails]  WITH CHECK ADD  CONSTRAINT [FK_VoucherDieselDetails_Station] FOREIGN KEY([StationId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[VoucherDieselDetails] CHECK CONSTRAINT [FK_VoucherDieselDetails_Station]
GO
ALTER TABLE [dbo].[VoucherDieselDetails]  WITH CHECK ADD  CONSTRAINT [FK_VoucherDieselDetails_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO
ALTER TABLE [dbo].[VoucherDieselDetails] CHECK CONSTRAINT [FK_VoucherDieselDetails_Voucher]
GO
ALTER TABLE [dbo].[VoucherOthersExpenses]  WITH CHECK ADD  CONSTRAINT [FK_VoucherOthersExpenses_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO
ALTER TABLE [dbo].[VoucherOthersExpenses] CHECK CONSTRAINT [FK_VoucherOthersExpenses_Voucher]
GO
USE [master]
GO
ALTER DATABASE [RRRoadwaysDB] SET  READ_WRITE 
GO
