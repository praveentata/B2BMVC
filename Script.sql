USE [master]
GO
/****** Object:  Database [B2B]    Script Date: 9/29/2016 9:22:38  ******/
CREATE DATABASE [B2B]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'B2B', FILENAME = N'C:\Users\HP\B2B.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'B2B_log', FILENAME = N'C:\Users\HP\B2B_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [B2B].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [B2B] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [B2B] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [B2B] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [B2B] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [B2B] SET ARITHABORT OFF 
GO
ALTER DATABASE [B2B] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [B2B] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [B2B] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [B2B] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [B2B] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [B2B] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [B2B] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [B2B] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [B2B] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [B2B] SET  DISABLE_BROKER 
GO
ALTER DATABASE [B2B] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [B2B] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [B2B] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [B2B] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [B2B] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [B2B] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [B2B] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [B2B] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [B2B] SET  MULTI_USER 
GO
ALTER DATABASE [B2B] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [B2B] SET DB_CHAINING OFF 
GO
ALTER DATABASE [B2B] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [B2B] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [B2B] SET DELAYED_DURABILITY = DISABLED 
GO
USE [B2B]
GO
/****** Object:  Table [dbo].[po]    Script Date: 9/29/2016 9:22:38  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[po](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PO_ID] [int] NULL,
	[user_id] [int] NOT NULL,
	[status] [varchar](255) NULL,
	[product_id] [int] NULL,
	[SupplierId] [int] NULL,
	[ProductName] [varchar](255) NULL,
	[ProductDescription] [varchar](255) NULL,
	[Image] [varchar](255) NULL,
	[Price] [int] NULL,
	[shopping_date] [date] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[products]    Script Date: 9/29/2016 9:22:38  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[products](
	[ID] [int] NOT NULL,
	[ProductName] [varchar](255) NULL,
	[ProductDescription] [varchar](255) NULL,
	[Image] [varchar](255) NULL,
	[Price] [int] NULL,
	[SupplierId] [int] NULL,
	[SupplierName] [varchar](100) NULL,
 CONSTRAINT [PK_Products_ProductID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[shopping_cart]    Script Date: 9/29/2016 9:22:38  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[shopping_cart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[product_id] [int] NULL,
	[SupplierId] [int] NULL,
	[ProductName] [varchar](255) NULL,
	[ProductDescription] [varchar](255) NULL,
	[Image] [varchar](255) NULL,
	[Price] [int] NULL,
	[shopping_date] [date] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 9/29/2016 9:22:38  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] NOT NULL,
	[SupplierName] [varchar](255) NULL,
	[IsSelected] [bit] NULL,
 CONSTRAINT [PK_Supplier_SupplierId] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[supplier_inv]    Script Date: 9/29/2016 9:22:38  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supplier_inv](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PO_ID] [int] NULL,
	[user_id] [int] NOT NULL,
	[status] [varchar](255) NULL,
	[product_id] [int] NULL,
	[SupplierId] [int] NULL,
	[ProductName] [varchar](255) NULL,
	[ProductDescription] [varchar](255) NULL,
	[Image] [varchar](255) NULL,
	[Price] [int] NULL,
	[shopping_date] [date] NULL,
	[quantity] [int] NULL,
	[InvoiceID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRegistration]    Script Date: 9/29/2016 9:22:38  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRegistration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[last_name] [varchar](255) NULL,
	[first_name] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[confirm_password] [varchar](255) NULL,
	[user_name] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[user_role] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [B2B] SET  READ_WRITE 
GO
