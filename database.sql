USE [master]
GO
/****** Object:  Database [SuperDb]    Script Date: 20/03/2025 01:06:52 ******/
CREATE DATABASE [SuperDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SuperDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SuperDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SuperDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SuperDb_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SuperDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SuperDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SuperDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SuperDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SuperDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SuperDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SuperDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [SuperDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SuperDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SuperDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SuperDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SuperDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SuperDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SuperDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SuperDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SuperDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SuperDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SuperDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SuperDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SuperDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SuperDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SuperDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SuperDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SuperDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SuperDb] SET RECOVERY FULL 
GO
ALTER DATABASE [SuperDb] SET  MULTI_USER 
GO
ALTER DATABASE [SuperDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SuperDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SuperDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SuperDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SuperDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SuperDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SuperDb', N'ON'
GO
ALTER DATABASE [SuperDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [SuperDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SuperDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[ShippingCost] [int] NOT NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchProducts]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_BranchProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ShippingAddress] [nvarchar](max) NOT NULL,
	[PaymentStatus] [nvarchar](max) NOT NULL,
	[SumForPay] [decimal](18, 2) NOT NULL,
	[Currency] [nvarchar](max) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[UnitOfMeasure] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCarts]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCarts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCartsItem]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCartsItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShoppingCartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
 CONSTRAINT [PK_ShoppingCartsItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20/03/2025 01:06:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250309171445_try', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250309224818_Create', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250310183922_order', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250310185256_changeOrder', N'6.0.0')
GO
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (1, N'פרש מרקט חולון', N'03-4567890', N'רחוב שנקר 40, חולון', N'holon@freshmarket.co.il', 26)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (2, N'ויקטורי אילת', N'08-4445566', N'טיילת החוף 50, אילת', N'eilat@victory.co.il', 40)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (3, N'רמי לוי שיווק השקמה חיפה', N'04-5678901', N'שדרות בן גוריון 30, חיפה', N'haifa@rami-levy.co.il', 20)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (4, N'שופרסל דיל אקספרס תל אביב', N'03-1234567', N'רחוב רוטשילד 10, תל אביב', N'telaviv@shufersal.co.il', 25)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (5, N'מגה בעיר גבעתיים', N'03-5678901', N'רחוב ויצמן 30, גבעתיים', N'givatayim@mega.co.il', 29)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (6, N'טיב טעם הרצליה', N'09-1234567', N'רחוב סוקולוב 20, הרצליה', N'herzliya@tivtaam.co.il', 0)
SET IDENTITY_INSERT [dbo].[Branches] OFF
GO
SET IDENTITY_INSERT [dbo].[BranchProducts] ON 

INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1, 1, 8, 4.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (2, 2, 8, 4.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (3, 3, 8, 4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (4, 4, 8, 3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (5, 5, 8, 2.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (6, 6, 8, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (7, 1, 7, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (8, 2, 7, 6.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (9, 3, 7, 5.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (10, 4, 7, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (11, 5, 7, 8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (12, 6, 7, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (13, 1, 6, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (14, 2, 6, 7.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (15, 3, 6, 5.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (16, 4, 6, 7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (17, 5, 6, 8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (18, 6, 6, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (19, 1, 5, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (20, 2, 5, 6.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (21, 3, 5, 6.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (22, 4, 5, 7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (23, 5, 5, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (24, 6, 5, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (25, 1, 4, 11)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (26, 2, 4, 12.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (27, 3, 4, 12.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (28, 4, 4, 13)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (29, 5, 4, 13.6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (30, 6, 4, 12.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (31, 1, 3, 25)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (32, 2, 3, 24.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (33, 3, 3, 24.6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (34, 4, 3, 24.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (35, 5, 3, 25.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (36, 6, 3, 25.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (37, 1, 2, 8.6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (38, 2, 2, 8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (39, 3, 2, 8.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (40, 4, 2, 8.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (41, 5, 2, 8.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (42, 6, 2, 8.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (43, 1, 1, 9.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (44, 2, 1, 10)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (45, 3, 1, 8.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (46, 4, 1, 12.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (47, 5, 1, 11.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (48, 6, 1, 10.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (49, 1, 22, 25.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (50, 2, 22, 24)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (51, 3, 22, 26.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (52, 4, 22, 25.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (53, 5, 22, 26)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (54, 6, 22, 25.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (55, 1, 21, 17)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (56, 2, 21, 17.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (57, 3, 21, 17.1)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (58, 4, 21, 18)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (59, 5, 21, 18.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (60, 6, 21, 16.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (61, 1, 20, 5.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (62, 2, 20, 5.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (63, 3, 20, 5.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (64, 4, 20, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (65, 5, 20, 4.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (66, 6, 20, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (67, 1, 19, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (68, 2, 19, 5.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (69, 3, 19, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (70, 4, 19, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (71, 5, 19, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (72, 6, 19, 4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (73, 1, 18, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (74, 2, 18, 4.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (75, 3, 18, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (76, 4, 18, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (77, 5, 18, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (78, 6, 18, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (79, 1, 17, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (80, 2, 17, 5.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (81, 3, 17, 5.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (82, 4, 17, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (83, 5, 17, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (84, 6, 17, 5.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (85, 1, 16, 50)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (86, 2, 16, 54)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (87, 3, 16, 52.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (88, 4, 16, 54.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (89, 5, 16, 55)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (90, 6, 16, 50)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (91, 1, 15, 5.6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (92, 2, 15, 5.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (93, 3, 15, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (94, 4, 15, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (95, 5, 15, 5.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (96, 6, 15, 5.3)
SET IDENTITY_INSERT [dbo].[BranchProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'מוצרי חלב')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'ירקות ופירות')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'לחם ומאפים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'גבינות')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (6, N'חטיפים ומתוקים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (7, N'שתייה קלה')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (8, N'שימורים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (9, N'בשר ועוף')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (10, N'ביצים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (11, N'מוצרי ניקיון')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (1, 4, N'string', N'Pending', CAST(57.40 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-10T18:46:24.5406679' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (2, 4, N'string', N'Pending', CAST(162.60 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-10T18:48:25.2184992' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (3, 4, N'string', N'Pending', CAST(162.00 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-10T18:49:00.3804372' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (4, 4, N'string', N'Pending', CAST(57.40 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-10T18:53:34.7864359' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (5, 4, N'string', N'Pending', CAST(57.40 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-10T18:59:02.4611376' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (6, 4, N'string', N'Pending', CAST(57.40 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-10T20:29:30.1352018' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (7, 13, N'm', N'Pending', CAST(75.00 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-11T03:19:17.8351374' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (8, 1, N'finkel', N'Pending', CAST(97.20 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-16T11:47:23.1774733' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (9, 13, N'm', N'Pending', CAST(54.80 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-19T22:58:33.8931930' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [CustomerId], [ShippingAddress], [PaymentStatus], [SumForPay], [Currency], [OrderDate]) VALUES (10, 13, N'm', N'Pending', CAST(54.80 AS Decimal(18, 2)), N'ILS', CAST(N'2025-03-19T22:58:37.9338336' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1, N'תפוח אדום', 2, N'image (15).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (2, N'לחם לבן', 3, N'image (14).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (3, N'גבינה צהובה 38 אחוז', 4, N'image (13).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (4, N'ביצים L', 10, N'image (12).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (5, N'חלב 1 אחוז', 1, N'image (11).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (6, N'שוקולד חלב', 6, N'image (10).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (7, N'מיץ תפוזים', 7, N'image (9).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (8, N'במבה אוסם', 6, N'image (8).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (15, N'מלפפון', 2, N'image (20).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (16, N'ירכי עוף טרי', 9, N'image (19).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (17, N'פלפל אדום', 2, N'image (18).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (18, N'בצל יבש', 2, N'image (17).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (19, N'מלפפון חמוץ', 8, N'image (16).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (20, N'גרעיני תירס מתוק', 8, N'image (21).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (21, N'סנו ג''אוול מרסס לימון', 11, N'image (22).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (22, N'נוזל רצפות בייבי לאב', 11, N'image (23).png', 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'ROLE_USER')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'ROLE_ADMIN')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'ROLE_MANAGER')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingCarts] ON 

INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (2, 0)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (4, 0)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (5, 0)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (6, 0)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (8, 0)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (9, 0)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (16, 13)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (17, 1)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (18, 1)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (19, 1)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (20, 1)
INSERT [dbo].[ShoppingCarts] ([Id], [UserId]) VALUES (21, 13)
SET IDENTITY_INSERT [dbo].[ShoppingCarts] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingCartsItem] ON 

INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (7, 2, 2, 2)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (8, 2, 3, 3)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (9, 2, 4, 4)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (16, 9, 2, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (28, 16, 2, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (29, 16, 3, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (30, 17, 4, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (31, 17, 3, 3)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (32, 17, 2, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (33, 17, 7, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (34, 18, 3, 2)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (35, 18, 2, 2)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (36, 18, 1, 3)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (37, 18, 7, 2)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (38, 19, 2, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (39, 19, 3, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (40, 20, 2, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (41, 20, 1, 1)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (42, 20, 3, 3)
INSERT [dbo].[ShoppingCartsItem] ([Id], [ShoppingCartId], [ProductId], [Quantity]) VALUES (43, 16, 1, 2)
SET IDENTITY_INSERT [dbo].[ShoppingCartsItem] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (3, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (4, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (6, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (8, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (9, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (10, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (11, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (12, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (13, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (14, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (15, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (13, 2)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (13, 3)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (1, N'michal', N'm@m', N'$2a$12$5xTDTmBGQhyBKSv5/VG/4e3Qg1ZQlpUVlEwXIjoMsCvg4MwYLHEJy', N'finkel', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (2, N'852', N'm@m', N'$2a$12$D7Ws1rgNtsUslF1NAftb2OT6820itw5.tqO1aq5da7dpElHMpKcgS', N'petach', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (3, N'.;', N'n@w', N'$2a$12$EB6cNBgpiLtj8W/czpvjZ.STR4QbChI23ThEOpjSq.5W5Eh4TM3jq', N'njn', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (4, N'string', N'string', N'$2a$12$Wrwp9E1KXV5XFYhBENwK.OxEtSx.jUY7l5YmZX8iazQTvz7A7YTsu', N'string', N'string')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (6, N'', N'', N'$2a$12$h346mqX3ybQSZ17E4snL3umKqf1yKWEPfV4LWngdJ3jvoVpbGUCR.', N'', N'')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (8, N'njn', N'v@v', N'$2a$12$lz2u/Y0UqvTtD2xn5T0MRunobCiVISAW03InY.l8RKu2olv1dLp2e', N'kk', N'0541192870')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (9, N'nj6', N'n@c', N'$2a$12$QVu94MIVC9nD5tE57qY/nOOFD/40bQHBCHKVxRm8Lq2Bs8KLny4gS', N'mk', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (10, N'צל', N'x@x', N'$2a$12$eTk.pREuKQK0a9OiyF90be12W/gdoIPksmjtgcBxGCcqyn9CyEH02', N'mkm', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (11, N'mk', N'c@c', N'$2a$12$H1zlP09ORUg052OnakcaJODzb2QG9InSb3s6pHPN2WOiXN/oVAHYi', N';;;;', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (12, N'njk', N'v@d', N'$2a$12$HoSQxs9lIrZuikRSiZEfDeK9iKZtSHkDYLLKb0Y.VPD84i7uIdY1G', N'lml', N'0547730604')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (13, N'manager1234', N'manager@m', N'$2a$12$UVMCIqjZkSwAj3hU4ln2G.MVMmsZCpUzMl29tiTKbnWAEjKl3x2TW', N'm', N'054-7730-604')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (14, N'yael', N'y@y', N'$2a$12$FggE8XcPR5pNwD4bAcnzEujmmRbcvPnd566V18z1ctHjxtuxDxd9a', N',l', N'0583230290')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (15, N'strnning', N'string', N'$2a$12$p6hCjSu.C.VMSGjXQsxo0Oynt0ruMt0Q/BWMFji3UKex5Nc967suy', N'string', N'string')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_BranchProducts_BranchId]    Script Date: 20/03/2025 01:06:53 ******/
CREATE NONCLUSTERED INDEX [IX_BranchProducts_BranchId] ON [dbo].[BranchProducts]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchProducts_ProductId]    Script Date: 20/03/2025 01:06:53 ******/
CREATE NONCLUSTERED INDEX [IX_BranchProducts_ProductId] ON [dbo].[BranchProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 20/03/2025 01:06:53 ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCartsItem_ProductId]    Script Date: 20/03/2025 01:06:53 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCartsItem_ProductId] ON [dbo].[ShoppingCartsItem]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCartsItem_ShoppingCartId]    Script Date: 20/03/2025 01:06:53 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCartsItem_ShoppingCartId] ON [dbo].[ShoppingCartsItem]
(
	[ShoppingCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 20/03/2025 01:06:53 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BranchProducts]  WITH CHECK ADD  CONSTRAINT [FK_BranchProducts_Branches_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BranchProducts] CHECK CONSTRAINT [FK_BranchProducts_Branches_BranchId]
GO
ALTER TABLE [dbo].[BranchProducts]  WITH CHECK ADD  CONSTRAINT [FK_BranchProducts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BranchProducts] CHECK CONSTRAINT [FK_BranchProducts_Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ShoppingCartsItem]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCartsItem_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ShoppingCartsItem] CHECK CONSTRAINT [FK_ShoppingCartsItem_Products_ProductId]
GO
ALTER TABLE [dbo].[ShoppingCartsItem]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCartsItem_ShoppingCarts_ShoppingCartId] FOREIGN KEY([ShoppingCartId])
REFERENCES [dbo].[ShoppingCarts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShoppingCartsItem] CHECK CONSTRAINT [FK_ShoppingCartsItem_ShoppingCarts_ShoppingCartId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [SuperDb] SET  READ_WRITE 
GO
