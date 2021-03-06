--USE [master]
--GO
--/****** Object:  Database [Homelibrary]    Script Date: 10/06/2017 00:34:56 ******/
--CREATE DATABASE [Homelibrary]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'Library', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Library.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'Library_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Library_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
--GO
--ALTER DATABASE [Homelibrary] SET COMPATIBILITY_LEVEL = 120
--GO
--IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
--begin
--EXEC [Homelibrary].[dbo].[sp_fulltext_database] @action = 'enable'
--end
--GO
--ALTER DATABASE [Homelibrary] SET ANSI_NULL_DEFAULT OFF 
--GO
--ALTER DATABASE [Homelibrary] SET ANSI_NULLS OFF 
--GO
--ALTER DATABASE [Homelibrary] SET ANSI_PADDING OFF 
--GO
--ALTER DATABASE [Homelibrary] SET ANSI_WARNINGS OFF 
--GO
--ALTER DATABASE [Homelibrary] SET ARITHABORT OFF 
--GO
--ALTER DATABASE [Homelibrary] SET AUTO_CLOSE ON 
--GO
--ALTER DATABASE [Homelibrary] SET AUTO_SHRINK OFF 
--GO
--ALTER DATABASE [Homelibrary] SET AUTO_UPDATE_STATISTICS ON 
--GO
--ALTER DATABASE [Homelibrary] SET CURSOR_CLOSE_ON_COMMIT OFF 
--GO
--ALTER DATABASE [Homelibrary] SET CURSOR_DEFAULT  GLOBAL 
--GO
--ALTER DATABASE [Homelibrary] SET CONCAT_NULL_YIELDS_NULL OFF 
--GO
--ALTER DATABASE [Homelibrary] SET NUMERIC_ROUNDABORT OFF 
--GO
--ALTER DATABASE [Homelibrary] SET QUOTED_IDENTIFIER OFF 
--GO
--ALTER DATABASE [Homelibrary] SET RECURSIVE_TRIGGERS OFF 
--GO
--ALTER DATABASE [Homelibrary] SET  ENABLE_BROKER 
--GO
--ALTER DATABASE [Homelibrary] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
--GO
--ALTER DATABASE [Homelibrary] SET DATE_CORRELATION_OPTIMIZATION OFF 
--GO
--ALTER DATABASE [Homelibrary] SET TRUSTWORTHY OFF 
--GO
--ALTER DATABASE [Homelibrary] SET ALLOW_SNAPSHOT_ISOLATION OFF 
--GO
--ALTER DATABASE [Homelibrary] SET PARAMETERIZATION SIMPLE 
--GO
--ALTER DATABASE [Homelibrary] SET READ_COMMITTED_SNAPSHOT ON 
--GO
--ALTER DATABASE [Homelibrary] SET HONOR_BROKER_PRIORITY OFF 
--GO
--ALTER DATABASE [Homelibrary] SET RECOVERY SIMPLE 
--GO
--ALTER DATABASE [Homelibrary] SET  MULTI_USER 
--GO
--ALTER DATABASE [Homelibrary] SET PAGE_VERIFY CHECKSUM  
--GO
--ALTER DATABASE [Homelibrary] SET DB_CHAINING OFF 
--GO
--ALTER DATABASE [Homelibrary] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
--GO
--ALTER DATABASE [Homelibrary] SET TARGET_RECOVERY_TIME = 0 SECONDS 
--GO
--ALTER DATABASE [Homelibrary] SET DELAYED_DURABILITY = DISABLED 
--GO
--USE [Homelibrary]
--GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBook]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBook](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ISBN] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.TBook] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBookCheckout]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBookCheckout](
	[BookCheckoutId] [int] IDENTITY(1,1) NOT NULL,
	[BookOnShelfId] [int] NOT NULL,
	[Comment] [nvarchar](2000) NULL,
	[CheckedOutAt] [datetime] NOT NULL,
	[ReturnOn] [datetime] NULL,
	[BorrowerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.TBookCheckout] PRIMARY KEY CLUSTERED 
(
	[BookCheckoutId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBookOnShelf]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBookOnShelf](
	[BookOnShelfId] [int] IDENTITY(1,1) NOT NULL,
	[BookShelfId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.TBookOnShelf] PRIMARY KEY CLUSTERED 
(
	[BookOnShelfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBookShelf]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBookShelf](
	[BookShelfId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.TBookShelf] PRIMARY KEY CLUSTERED 
(
	[BookShelfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBorrower]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBorrower](
	[BorrowerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.TBorrower] PRIMARY KEY CLUSTERED 
(
	[BorrowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TUser]    Script Date: 10/06/2017 00:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_dbo.TUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_BookOnShelfId]    Script Date: 10/06/2017 00:34:56 ******/
CREATE NONCLUSTERED INDEX [IX_BookOnShelfId] ON [dbo].[TBookCheckout]
(
	[BookOnShelfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BorrowerId]    Script Date: 10/06/2017 00:34:56 ******/
CREATE NONCLUSTERED INDEX [IX_BorrowerId] ON [dbo].[TBookCheckout]
(
	[BorrowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TBookCheckout]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TBookCheckout_dbo.TBookOnShelf_BookOnShelfId] FOREIGN KEY([BookOnShelfId])
REFERENCES [dbo].[TBookOnShelf] ([BookOnShelfId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TBookCheckout] CHECK CONSTRAINT [FK_dbo.TBookCheckout_dbo.TBookOnShelf_BookOnShelfId]
GO
ALTER TABLE [dbo].[TBookCheckout]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TBookCheckout_dbo.TBorrower_BorrowerId] FOREIGN KEY([BorrowerId])
REFERENCES [dbo].[TBorrower] ([BorrowerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TBookCheckout] CHECK CONSTRAINT [FK_dbo.TBookCheckout_dbo.TBorrower_BorrowerId]
GO
USE [master]
GO
ALTER DATABASE [Homelibrary] SET  READ_WRITE 
GO
