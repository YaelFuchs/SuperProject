USE [master]
GO
/****** Object:  Database [SuperDb]    Script Date: 26/03/2025 09:26:59 ******/
CREATE DATABASE [SuperDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SuperDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SuperDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SuperDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SuperDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[Branches]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[BranchProducts]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 26/03/2025 09:26:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentStatus] [nvarchar](max) NOT NULL,
	[SumForPay] [float] NOT NULL,
	[Currency] [nvarchar](max) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ShippingAddress] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[ShoppingCarts]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[ShoppingCartsItem]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 26/03/2025 09:26:59 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 26/03/2025 09:26:59 ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250310131328_addOrder', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250310161911_addVariableToOrder', N'6.0.0')
GO
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (1, N'פרש מרקט חולון', N'03-4567890', N'רחוב שנקר 40, חולון', N'holon@freshmarket.co.il', 27)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (2, N'ויקטורי אילת', N'08-4445566', N'טיילת החוף 50, אילת', N'eilat@victory.co.il', 40)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (3, N'רמי לוי שיווק השקמה חיפה', N'04-5678901', N'שדרות בן גוריון 30, חיפה', N'haifa@rami-levy.co.il', 20)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (4, N'שופרסל דיל אקספרס תל אביב', N'03-1234567', N'רחוב רוטשילד 10, תל אביב', N'telaviv@shufersal.co.il', 25)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (5, N'מגה בעיר גבעתיים', N'03-5678901', N'רחוב ויצמן 30, גבעתיים', N'givatayim@mega.co.il', 29)
INSERT [dbo].[Branches] ([Id], [Name], [Phone], [Address], [Email], [ShippingCost]) VALUES (6, N'טיב טעם הרצליה', N'09-1234567', N'רחוב סוקולוב 20, הרצליה', N'herzliya@tivtaam.co.il', 26)
SET IDENTITY_INSERT [dbo].[Branches] OFF
GO
SET IDENTITY_INSERT [dbo].[BranchProducts] ON 

INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (19, 1, 5, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (20, 2, 5, 6.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (21, 3, 5, 6.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (22, 4, 5, 7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (23, 5, 5, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (24, 6, 5, 6.7)
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
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (44, 2, 1, 9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (45, 3, 1, 8.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (46, 4, 1, 12.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (47, 5, 1, 11.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (48, 6, 1, 10.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1049, 1, 1009, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1050, 2, 1009, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1051, 3, 1009, 4.6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1052, 4, 1009, 4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1053, 5, 1009, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1054, 6, 1009, 5.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1055, 1, 1010, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1056, 2, 1010, 5.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1057, 3, 1010, 5.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1058, 4, 1010, 5.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1059, 5, 1010, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1060, 6, 1010, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1061, 1, 9, 6.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1062, 2, 9, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1063, 3, 9, 5.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1064, 5, 9, 6.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1065, 4, 9, 7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1066, 6, 9, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1067, 1, 12, 4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1068, 2, 12, 4.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1069, 3, 12, 4.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1070, 4, 12, 3.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1071, 5, 12, 4.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1072, 6, 12, 5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1073, 1, 1011, 9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1074, 2, 1011, 9.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1075, 3, 1011, 9.1)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1076, 4, 1011, 9.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1077, 5, 1011, 9.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1078, 6, 1011, 8.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1079, 1, 1012, 12)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1080, 2, 1012, 12.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1081, 3, 1012, 12.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1082, 4, 1012, 12.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1083, 5, 1012, 12.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1084, 6, 1012, 11.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1085, 1, 1013, 6.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1086, 2, 1013, 6.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1087, 3, 1013, 6.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1088, 5, 1013, 6.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1089, 4, 1013, 7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1090, 6, 1013, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1091, 1, 1014, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1092, 2, 1014, 6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1093, 3, 1014, 6.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1094, 4, 1014, 5.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1095, 5, 1014, 5.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1096, 6, 1014, 5.7)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1097, 1, 1015, 10.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1098, 2, 1015, 10)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1099, 3, 1015, 9.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1100, 4, 1015, 10.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1101, 5, 1015, 10.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1102, 6, 1015, 9.8)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1103, 1, 1016, 17)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1104, 2, 1016, 17.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1105, 3, 1016, 17.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1106, 4, 1016, 17.2)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1107, 5, 1016, 16.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1108, 6, 1016, 17)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1109, 1, 1017, 15)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1110, 2, 1017, 15.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1111, 3, 1017, 15.3)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1112, 4, 1017, 14.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1113, 5, 1017, 15.6)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1114, 6, 1017, 15.1)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1115, 1, 1018, 30)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1116, 2, 1018, 32)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1117, 3, 1018, 30.5)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1118, 4, 1018, 32.4)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1119, 5, 1018, 31.9)
INSERT [dbo].[BranchProducts] ([Id], [BranchId], [ProductId], [Price]) VALUES (1120, 6, 1018, 30.9)
SET IDENTITY_INSERT [dbo].[BranchProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'מוצרי חלב')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'ירקות ופירות')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'לחם ומאפים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'גבינות')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'חטיפים ומתוקים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (6, N'שתייה קלה')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (7, N'שימורים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (8, N'בשר ועוף')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (9, N'ביצים')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (10, N'מוצרי ניקיון')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1, N'תפוח אדום', 2, N'image (5).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (2, N'לחם לבן', 3, N'image (6).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (3, N'גבינה צהובה 28%', 4, N'image (7).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (5, N'חלב 1%', 1, N'image (9).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (9, N'מיץ תפוזים', 6, N'image (11).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (12, N'שוקולד חלב', 5, N'image (10).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1009, N'במבה אסם', 5, N'image (12).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1010, N'מלפפון', 2, N'image (13).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1011, N'מלפפון חמוץ במלח', 7, N'image (17).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1012, N'ביצים L', 9, N'image (8).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1013, N'פלפל אדום', 2, N'image (15).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1014, N'בצל יבש', 2, N'image (16).png', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1015, N'גרעיני תירס מתוק', 7, N'image (18).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1016, N'סנו ג''אוול מרסס לימון', 10, N'image (19).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1017, N'נוזל רצפות בייבי לאב', 10, N'image (20).png', 0)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [ImageUrl], [UnitOfMeasure]) VALUES (1018, N'ירכי עוף טרי', 8, N'image (14).png', 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'ROLE_USER')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'ROLE_ADMIN')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'ROLE_MANAGER')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (3, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (4, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 2)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 3)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (1, N'manager1234', N'manager123@gmail.com', N'$2a$12$4MY0lcrs5IWZU8KBCGFhfuhU5426RPLml9UPzt8oxAB0rAOHeFc0u', N'Rotchild', N'0533195821')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (2, N'yael', N'ya@gmail.com', N'$2a$12$Gqnlx4LLyo3MJZ4uDwFH6.LfrgtYk8y5Rg9bzQ.q3GQ1AaVzJ3Xy.', N'rot', N'053-319-5821')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (3, N'yaeli', N'y@gmail.com', N'$2a$12$KdqdABXceUPpVVJJZ9SZ1OAOhA2gz3JdoNtuGHsUHkH7R8cpvahSO', N'rot', N'053-319-5821')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [Address], [Phone]) VALUES (4, N'aw', N'we@gmail.com', N'$2a$12$ERySNl3gCLKMPYCBhEfLNe4GioXREdMxmHphCuW7kmXuxIGdqpTTe', N'as', N'053-319-5821')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_BranchProducts_BranchId]    Script Date: 26/03/2025 09:27:00 ******/
CREATE NONCLUSTERED INDEX [IX_BranchProducts_BranchId] ON [dbo].[BranchProducts]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchProducts_ProductId]    Script Date: 26/03/2025 09:27:00 ******/
CREATE NONCLUSTERED INDEX [IX_BranchProducts_ProductId] ON [dbo].[BranchProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 26/03/2025 09:27:00 ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCartsItem_ProductId]    Script Date: 26/03/2025 09:27:00 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCartsItem_ProductId] ON [dbo].[ShoppingCartsItem]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCartsItem_ShoppingCartId]    Script Date: 26/03/2025 09:27:00 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCartsItem_ShoppingCartId] ON [dbo].[ShoppingCartsItem]
(
	[ShoppingCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 26/03/2025 09:27:00 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [CustomerId]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [ShippingAddress]
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
