USE [dbAirSparkle]
GO
/****** Object:  Schema [HangFire]    Script Date: 2024/4/12 上午 11:15:48 ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[AirBookSeats]    Script Date: 2024/4/12 上午 11:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirBookSeats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirFlightSeatId] [int] NOT NULL,
	[TicketInvoicingDetailId] [int] NOT NULL,
	[ReservationTime] [datetime2](7) NOT NULL,
	[TransferPaymentId] [int] NULL,
	[HandlingFee] [decimal](18, 2) NULL,
	[SeatAssignmentNum] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_AirBookSeats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirCabinRules]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirCabinRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirCabinId] [int] NOT NULL,
	[CabinCode] [nvarchar](10) NOT NULL,
	[Priority] [int] NOT NULL,
	[ShipmentWeight] [int] NOT NULL,
	[ShipmentCount] [int] NOT NULL,
	[CarryBagCount] [int] NOT NULL,
	[CarryBagWeight] [int] NOT NULL,
	[PreSelectedSeat] [bit] NOT NULL,
	[AccumulatedMile] [float] NOT NULL,
	[MileUpgrade] [bit] NOT NULL,
	[TicketVaildity] [int] NOT NULL,
	[RefundFee] [int] NOT NULL,
	[NoShowFee] [int] NOT NULL,
	[FreeWifi] [bit] NOT NULL,
 CONSTRAINT [PK_AirCabinRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirCabins]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirCabins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CabinClass] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_AirCabins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirCabinSeats]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirCabinSeats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirTypeId] [int] NOT NULL,
	[AirCabinId] [int] NOT NULL,
	[SeatNum] [int] NULL,
 CONSTRAINT [PK_AirCabinSeats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirFlightManagements]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirFlightManagements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlightCode] [nvarchar](15) NOT NULL,
	[DepartureAirportId] [int] NOT NULL,
	[ArrivalAirportId] [int] NOT NULL,
	[DepartureTerminal] [nvarchar](15) NULL,
	[ArrivalTerminal] [nvarchar](15) NULL,
	[DepartureTime] [time](7) NOT NULL,
	[ArrivalTime] [time](7) NOT NULL,
	[DayofWeek] [nvarchar](15) NOT NULL,
	[Mile] [int] NOT NULL,
	[CrossDay] [int] NOT NULL,
 CONSTRAINT [PK_AirFlightManagements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirFlights]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirFlights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirOwnId] [int] NOT NULL,
	[AirFlightManagementId] [int] NOT NULL,
	[ScheduledDeparture] [datetime2](7) NOT NULL,
	[ScheduledArrival] [datetime2](7) NOT NULL,
	[AirFlightSaleStatusId] [int] NOT NULL,
 CONSTRAINT [PK_AirFlights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirFlightSaleStatuses]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirFlightSaleStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_AirFlightSaleStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirFlightSeats]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirFlightSeats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirFlightId] [int] NOT NULL,
	[AirCabinId] [int] NOT NULL,
	[SeatNum] [varchar](10) NOT NULL,
	[IsSeated] [bit] NOT NULL,
 CONSTRAINT [PK_AirFlightSeats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirFlightStatuses]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirFlightStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_AirFlightStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirMeals]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirMeals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AirCabinId] [int] NOT NULL,
	[MealContent] [nvarchar](300) NULL,
	[Image] [nvarchar](3000) NULL,
	[ImageBit] [varbinary](100) NULL,
	[Category] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_AirMeals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirOwns]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirOwns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirTypeId] [int] NOT NULL,
	[RegistrationNum] [nvarchar](30) NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[ManufactureYear] [int] NOT NULL,
 CONSTRAINT [PK_AirOwns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_AirOwns] UNIQUE NONCLUSTERED 
(
	[RegistrationNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirPorts]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirPorts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Lata] [varchar](10) NOT NULL,
	[Gps] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](20) NOT NULL,
	[City] [nvarchar](20) NOT NULL,
	[AirPortName] [nvarchar](20) NOT NULL,
	[TimeArea] [int] NOT NULL,
	[Zone] [int] NOT NULL,
	[CityIntroduction] [nvarchar](60) NOT NULL,
	[Img] [nvarchar](2000) NULL,
	[Continent] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_AirPorts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirTakeOffStatuses]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirTakeOffStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirFlightId] [int] NOT NULL,
	[ActualDepartureTime] [datetime2](7) NOT NULL,
	[ActualArrivalTime] [datetime2](7) NOT NULL,
	[AirFlightStatusId] [int] NOT NULL,
 CONSTRAINT [PK_AirTakeOffStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirTypes]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlightModel] [nvarchar](30) NOT NULL,
	[TotalSeat] [int] NOT NULL,
	[MaxMile] [int] NOT NULL,
	[MaxWeight] [int] NOT NULL,
	[ManufactureCompany] [nvarchar](30) NOT NULL,
	[Introduction] [nvarchar](1000) NULL,
	[Img] [nvarchar](300) NULL,
 CONSTRAINT [PK_AirTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_AirTypes] UNIQUE NONCLUSTERED 
(
	[FlightModel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AriTicketPrices]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AriTicketPrices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirCabinRuleId] [int] NOT NULL,
	[AirFlightManagementId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_AriTicketPrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campaigns]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaigns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignType] [nvarchar](20) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Campaigns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsCouponAirFlights]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsCouponAirFlights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsCouponId] [int] NOT NULL,
	[AirFlightId] [int] NOT NULL,
 CONSTRAINT [PK_CampaignsCouponAirFlights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsCouponMembers]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsCouponMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsCouponId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
 CONSTRAINT [PK_CampaignsCouponMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsCoupons]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsCoupons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignId] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[DiscountValue] [decimal](15, 4) NOT NULL,
	[DiscountQuantity] [int] NOT NULL,
	[AvailableQuantity] [int] NOT NULL,
	[MinimumOrderValue] [int] NULL,
	[MaximumDiscountAmount] [int] NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[DisplayDescription] [bit] NOT NULL,
	[MemberCriteria] [nvarchar](4000) NULL,
	[AirFlightsCriteria] [nvarchar](4000) NULL,
	[Image] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CampaignsCoupons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsDiscountMembers]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsDiscountMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsDiscountId] [int] NOT NULL,
	[MembersId] [int] NOT NULL,
 CONSTRAINT [PK_CampaignsDiscountMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsDiscounts]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsDiscounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignId] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[DiscountValue] [decimal](15, 4) NOT NULL,
	[Value] [decimal](15, 4) NOT NULL,
	[BundleSKUs] [decimal](15, 4) NULL,
	[MemberCriteria] [nvarchar](4000) NULL,
	[TFItemsCriteria] [nvarchar](4000) NULL,
	[Image] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CampaignsDiscounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsDiscountStatusNotifications]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsDiscountStatusNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsDiscountId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[IsReadByEmail] [bit] NOT NULL,
	[IsReadByBell] [bit] NOT NULL,
	[NotificationTime] [datetime2](7) NULL,
	[ReadTime] [datetime2](7) NULL,
	[NotiHeader] [varchar](max) NULL,
	[NotiBody] [varchar](max) NULL,
	[Url] [varchar](max) NULL,
	[CompanyStaffsId] [int] NULL,
 CONSTRAINT [PK_CampaignsDiscountStatusNotifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsDiscountTFItems]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsDiscountTFItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsDiscountId] [int] NOT NULL,
	[TFItemId] [int] NOT NULL,
 CONSTRAINT [PK_CampaignsDiscountTFItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsFlightCouponsUsageHistory]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsFlightCouponsUsageHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsCouponId] [int] NOT NULL,
	[TicketId] [int] NOT NULL,
	[UsedDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](20) NULL,
	[DateCreated] [datetime2](7) NULL,
	[OriginalPrice] [int] NOT NULL,
	[DiscountAmount] [int] NOT NULL,
	[DiscountedPrice] [int] NOT NULL,
 CONSTRAINT [PK_CampaignsFlightCouponsUsageHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignsTFDiscountUsageHistory]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignsTFDiscountUsageHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignsDiscountsId] [int] NOT NULL,
	[TFReservelistId] [int] NOT NULL,
	[UsedDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](20) NULL,
	[DateCreated] [datetime2](7) NULL,
	[OriginalPrice] [int] NOT NULL,
	[DiscountAmount] [int] NOT NULL,
	[DiscountedPrice] [int] NOT NULL,
 CONSTRAINT [PK_CampaignsTFDiscountUsageHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyDepartments]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyDepartments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[OrderBy] [int] NULL,
 CONSTRAINT [PK_CompanyDepartments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyJobs]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyJobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyDepartmentId] [int] NOT NULL,
	[JobTitle] [nvarchar](10) NOT NULL,
	[Rank] [int] NOT NULL,
 CONSTRAINT [PK_CompanyJobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyStaffLoginLogs]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyStaffLoginLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyStaffId] [int] NOT NULL,
	[Logintime] [datetime2](7) NOT NULL,
	[LogoutTime] [datetime2](7) NULL,
	[IPAddress] [nchar](17) NOT NULL,
	[LoginStatus] [bit] NOT NULL,
 CONSTRAINT [PK_CompanyStaffLoginLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyStaffs]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyStaffs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account] [nchar](10) NOT NULL,
	[Password] [nchar](100) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[LastName] [nvarchar](10) NOT NULL,
	[CompanyJobId] [int] NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
	[RegistrationTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CompanyStaffs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyStaffsChangePasswordLogs]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyStaffsChangePasswordLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyStaffId] [int] NOT NULL,
	[Password] [nchar](16) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CompanyStaffsChangePasswordLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChineseName] [nvarchar](20) NOT NULL,
	[EnglishName] [nvarchar](20) NOT NULL,
	[OrderBy] [int] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuggageOrders]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuggageOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TicketInvoicingDetailId] [int] NOT NULL,
	[LuggageId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[OrderTime] [datetime] NOT NULL,
	[TransferPaymentsId] [int] NOT NULL,
	[OrderStatus] [nvarchar](10) NOT NULL,
	[LuggageNum] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_LuggageOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Luggages]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Luggages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirFlightManagementsId] [int] NOT NULL,
	[OriginalPrice] [int] NOT NULL,
	[BookPrice] [int] NOT NULL,
 CONSTRAINT [PK_Luggages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberClasses]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[ClassOrder] [int] NOT NULL,
	[MileageStart] [int] NOT NULL,
	[MileageEnd] [int] NOT NULL,
 CONSTRAINT [PK_MemberClasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberLoginLogs]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberLoginLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[Logintime] [datetime2](7) NOT NULL,
	[LogoutTime] [datetime2](7) NULL,
	[IPAddress] [nchar](17) NOT NULL,
	[LoginStatus] [bit] NOT NULL,
 CONSTRAINT [PK_MemberLoginLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberClassId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Account] [nvarchar](16) NULL,
	[Password] [nvarchar](200) NULL,
	[ChineseLastName] [nvarchar](10) NULL,
	[ChineseFirstName] [nvarchar](10) NULL,
	[EnglishLastName] [nvarchar](30) NOT NULL,
	[EnglishFirstName] [nvarchar](30) NOT NULL,
	[Gender] [bit] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[TotalMileage] [int] NOT NULL,
	[PassportNumber] [nvarchar](10) NOT NULL,
	[PassportExpiryDate] [date] NOT NULL,
	[RegistrationTime] [datetime2](7) NOT NULL,
	[LastPasswordChangeTime] [datetime2](7) NOT NULL,
	[IsAllow] [bit] NOT NULL,
	[ConfirmCode] [nvarchar](100) NULL,
	[PasswordSalt] [nchar](100) NULL,
	[GoogleId] [nchar](100) NULL,
	[LineId] [nchar](100) NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageBoxes]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageBoxes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[ProcessedTime] [datetime2](7) NULL,
 CONSTRAINT [PK_MessageBoxes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageBoxId] [int] NOT NULL,
	[SendTime] [datetime2](7) NOT NULL,
	[MessageContent] [nvarchar](3000) NOT NULL,
	[MemberId] [int] NULL,
	[CompanyStaffId] [int] NULL,
	[IsReaded] [bit] NOT NULL,
	[ReadedTime] [datetime2](7) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MileageDetails]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MileageDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MermberIsd] [int] NOT NULL,
	[TotalMile] [int] NOT NULL,
	[OriginalMile] [int] NOT NULL,
	[ChangeMile] [int] NOT NULL,
	[FinalMile] [int] NOT NULL,
	[MileValidity] [datetime] NOT NULL,
	[MileReason] [nvarchar](20) NOT NULL,
	[OrderNumber] [varchar](20) NOT NULL,
	[ChangeTime] [datetime] NOT NULL,
 CONSTRAINT [PK_MileageDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MileApply]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MileApply](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Change] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[ChoseId] [int] NOT NULL,
	[Final] [int] NULL,
 CONSTRAINT [PK_MileApply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MileOrders]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MileOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NOT NULL,
	[Price] [int] NULL,
	[OrderTime] [datetime] NOT NULL,
	[TransferPaymentId] [int] NOT NULL,
	[OrderStatus] [nchar](10) NOT NULL,
	[MileNum] [nchar](10) NOT NULL,
	[MemberId] [int] NOT NULL,
 CONSTRAINT [PK_MileOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionGroups]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[Ddescribe] [nvarchar](100) NOT NULL,
	[Criteria] [nvarchar](max) NULL,
 CONSTRAINT [PK_PermissionGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionGroupsAddStaffs]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionGroupsAddStaffs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionGroupsId] [int] NOT NULL,
	[CompanyStaffsId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionGroupsAddStaffs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionPageInfos]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionPageInfos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageNumber] [nchar](50) NOT NULL,
	[PageName] [nvarchar](10) NOT NULL,
	[PageDescription] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PermissionPageInfos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionSettings]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionGroupsId] [int] NOT NULL,
	[PermissionPageInfoId] [int] NOT NULL,
	[ViewPermission] [bit] NOT NULL,
	[EditPermission] [bit] NOT NULL,
	[CreatePermission] [bit] NOT NULL,
	[DeletePermission] [bit] NOT NULL,
 CONSTRAINT [PK_PermissionSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeatAreas]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SeatGroup] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_SeatAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeatGroups]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirTypeId] [int] NOT NULL,
	[SeatAreaId] [int] NOT NULL,
	[SeatNum] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_SeatGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFCategories]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TFCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFItems]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TFCategoriesId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SerialNumber] [nvarchar](30) NOT NULL,
	[Image] [nvarchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsPublished] [bit] NOT NULL,
 CONSTRAINT [PK_TFItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFOrderlists]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFOrderlists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TicketDetailsId] [int] NOT NULL,
	[TFItemsId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[TFOrderStatusID] [int] NULL,
 CONSTRAINT [PK_TFOrderlists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFOrderStatuses]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFOrderStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaidTime] [datetime2](7) NULL,
	[FulfilledTime] [datetime2](7) NULL,
	[CancelledTime] [datetime2](7) NULL,
	[RefundTime] [datetime2](7) NULL,
	[StockedTime] [datetime2](7) NULL,
	[Paid] [bit] NULL,
	[Fulfilled] [bit] NULL,
	[Cancelled] [bit] NULL,
	[Refund] [bit] NULL,
	[Stocked] [bit] NULL,
	[TFReserveId] [int] NULL,
 CONSTRAINT [PK_TForderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFReservelists]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFReservelists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TFReserveId] [int] NOT NULL,
	[TFItemsId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[Discount] [int] NULL,
	[TotalPrice] [int] NOT NULL,
 CONSTRAINT [PK_TFReservelists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFReserves]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFReserves](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[TicketDetailsId] [int] NOT NULL,
	[Discount] [int] NULL,
	[TotalPrice] [int] NOT NULL,
	[TransferPaymentId] [int] NOT NULL,
	[TFOrderStatusID] [int] NULL,
 CONSTRAINT [PK_TFReserves] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFWishlists]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFWishlists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[TFItemsId] [int] NOT NULL,
 CONSTRAINT [PK_TFWishlists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketCancels]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketCancels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[TicketCancelStatusId] [int] NOT NULL,
	[TransferRefundId] [int] NOT NULL,
	[OriginalOrderAmount] [decimal](18, 2) NOT NULL,
	[TotalHandlingFee] [decimal](18, 2) NOT NULL,
	[ActualRefundAmount] [decimal](18, 2) NOT NULL,
	[ApplicationTime] [datetime2](7) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[RefundFeeDetail] [nvarchar](100) NOT NULL,
	[ProcessingTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TicketCancels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketCancelStatuses]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketCancelStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_TicketCancelStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketDetails]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[AirCabinRuleId] [int] NOT NULL,
	[TypeofPassengerId] [int] NOT NULL,
	[TicketAmount] [decimal](18, 2) NOT NULL,
	[AccruedMile] [int] NOT NULL,
	[BookRef] [nvarchar](10) NULL,
 CONSTRAINT [PK_TicketDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketInvoicingDetails]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketInvoicingDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TicketDetailId] [int] NOT NULL,
	[AirFlightSeatId] [int] NOT NULL,
	[LastName] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[CountryId] [int] NOT NULL,
	[DateofBirth] [datetime2](7) NOT NULL,
	[Gender] [bit] NOT NULL,
	[PassportNum] [nvarchar](10) NOT NULL,
	[PassportExpirationDate] [datetime2](7) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[TicketNum] [nvarchar](13) NOT NULL,
	[AirMealId] [int] NOT NULL,
	[CheckInStatus] [bit] NOT NULL,
	[CheckInTime] [datetime2](7) NULL,
 CONSTRAINT [PK_TicketInvoicingDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[AirFlightId] [int] NOT NULL,
	[OrderNum] [nvarchar](15) NOT NULL,
	[TotalPayableAmount] [decimal](18, 2) NOT NULL,
	[ActualPaidAmount] [decimal](18, 2) NOT NULL,
	[OrderTime] [datetime2](7) NOT NULL,
	[TransferPaymentId] [int] NOT NULL,
	[IsEstablished] [bit] NOT NULL,
	[MileRedemption] [int] NULL,
	[TotalGeneratedMile] [int] NOT NULL,
	[IsInvoiced] [bit] NOT NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferMethods]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferMethods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TransferPaymentIMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferPayments]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransferMethodsId] [int] NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[PaymentAmount] [decimal](10, 2) NOT NULL,
	[Info] [nvarchar](1000) NULL,
 CONSTRAINT [PK_TransferPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferRefunds]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferRefunds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransferIMethodsId] [int] NOT NULL,
	[RefundDate] [datetime2](7) NOT NULL,
	[RefundtAmount] [decimal](10, 2) NOT NULL,
	[TransferPaymentsId] [int] NOT NULL,
 CONSTRAINT [PK_TransferRefunds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeofPassengers]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeofPassengers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PassengerType] [nvarchar](5) NOT NULL,
	[MinAge] [int] NOT NULL,
	[MaxAge] [int] NOT NULL,
	[PricePercent] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TypeofPassengers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_HangFire_Counter] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 2024/4/12 上午 11:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TFOrderStatuses] ADD  CONSTRAINT [DF_TForderStatus_PaidTime]  DEFAULT (getdate()) FOR [PaidTime]
GO
ALTER TABLE [dbo].[TFOrderStatuses] ADD  CONSTRAINT [DF_TForderStatus_FulfilledTime]  DEFAULT (getdate()) FOR [FulfilledTime]
GO
ALTER TABLE [dbo].[TFOrderStatuses] ADD  CONSTRAINT [DF_TForderStatus_CancelledTime]  DEFAULT (getdate()) FOR [CancelledTime]
GO
ALTER TABLE [dbo].[TFOrderStatuses] ADD  CONSTRAINT [DF_TForderStatus_RefundTime]  DEFAULT (getdate()) FOR [RefundTime]
GO
ALTER TABLE [dbo].[TransferPayments] ADD  CONSTRAINT [DF_TransferPayments_PaymentDate]  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[AirBookSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirBookSeats_AirFlightSeats] FOREIGN KEY([AirFlightSeatId])
REFERENCES [dbo].[AirFlightSeats] ([Id])
GO
ALTER TABLE [dbo].[AirBookSeats] CHECK CONSTRAINT [FK_AirBookSeats_AirFlightSeats]
GO
ALTER TABLE [dbo].[AirBookSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirBookSeats_TicketInvoicingDetails] FOREIGN KEY([TicketInvoicingDetailId])
REFERENCES [dbo].[TicketInvoicingDetails] ([Id])
GO
ALTER TABLE [dbo].[AirBookSeats] CHECK CONSTRAINT [FK_AirBookSeats_TicketInvoicingDetails]
GO
ALTER TABLE [dbo].[AirBookSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirBookSeats_TransferPayments] FOREIGN KEY([TransferPaymentId])
REFERENCES [dbo].[TransferPayments] ([Id])
GO
ALTER TABLE [dbo].[AirBookSeats] CHECK CONSTRAINT [FK_AirBookSeats_TransferPayments]
GO
ALTER TABLE [dbo].[AirCabinRules]  WITH CHECK ADD  CONSTRAINT [FK_AirCabinRules_AirCabins] FOREIGN KEY([AirCabinId])
REFERENCES [dbo].[AirCabins] ([Id])
GO
ALTER TABLE [dbo].[AirCabinRules] CHECK CONSTRAINT [FK_AirCabinRules_AirCabins]
GO
ALTER TABLE [dbo].[AirCabinSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirCabinSeats_AirCabins] FOREIGN KEY([AirCabinId])
REFERENCES [dbo].[AirCabins] ([Id])
GO
ALTER TABLE [dbo].[AirCabinSeats] CHECK CONSTRAINT [FK_AirCabinSeats_AirCabins]
GO
ALTER TABLE [dbo].[AirCabinSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirCabinSeats_AirTypes] FOREIGN KEY([AirTypeId])
REFERENCES [dbo].[AirTypes] ([Id])
GO
ALTER TABLE [dbo].[AirCabinSeats] CHECK CONSTRAINT [FK_AirCabinSeats_AirTypes]
GO
ALTER TABLE [dbo].[AirFlightManagements]  WITH CHECK ADD  CONSTRAINT [FK_AirFlightManagements_AirPorts] FOREIGN KEY([DepartureAirportId])
REFERENCES [dbo].[AirPorts] ([Id])
GO
ALTER TABLE [dbo].[AirFlightManagements] CHECK CONSTRAINT [FK_AirFlightManagements_AirPorts]
GO
ALTER TABLE [dbo].[AirFlightManagements]  WITH CHECK ADD  CONSTRAINT [FK_AirFlightManagements_AirPorts1] FOREIGN KEY([ArrivalAirportId])
REFERENCES [dbo].[AirPorts] ([Id])
GO
ALTER TABLE [dbo].[AirFlightManagements] CHECK CONSTRAINT [FK_AirFlightManagements_AirPorts1]
GO
ALTER TABLE [dbo].[AirFlights]  WITH CHECK ADD  CONSTRAINT [FK_AirFlights_AirFlightManagements] FOREIGN KEY([AirFlightManagementId])
REFERENCES [dbo].[AirFlightManagements] ([Id])
GO
ALTER TABLE [dbo].[AirFlights] CHECK CONSTRAINT [FK_AirFlights_AirFlightManagements]
GO
ALTER TABLE [dbo].[AirFlights]  WITH CHECK ADD  CONSTRAINT [FK_AirFlights_AirFlightSaleStatuses] FOREIGN KEY([AirFlightSaleStatusId])
REFERENCES [dbo].[AirFlightSaleStatuses] ([Id])
GO
ALTER TABLE [dbo].[AirFlights] CHECK CONSTRAINT [FK_AirFlights_AirFlightSaleStatuses]
GO
ALTER TABLE [dbo].[AirFlights]  WITH CHECK ADD  CONSTRAINT [FK_AirFlights_AirOwns] FOREIGN KEY([AirOwnId])
REFERENCES [dbo].[AirOwns] ([Id])
GO
ALTER TABLE [dbo].[AirFlights] CHECK CONSTRAINT [FK_AirFlights_AirOwns]
GO
ALTER TABLE [dbo].[AirFlightSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirFlightSeats_AirCabins] FOREIGN KEY([AirCabinId])
REFERENCES [dbo].[AirCabins] ([Id])
GO
ALTER TABLE [dbo].[AirFlightSeats] CHECK CONSTRAINT [FK_AirFlightSeats_AirCabins]
GO
ALTER TABLE [dbo].[AirFlightSeats]  WITH CHECK ADD  CONSTRAINT [FK_AirFlightSeats_AirFlights] FOREIGN KEY([AirFlightId])
REFERENCES [dbo].[AirFlights] ([Id])
GO
ALTER TABLE [dbo].[AirFlightSeats] CHECK CONSTRAINT [FK_AirFlightSeats_AirFlights]
GO
ALTER TABLE [dbo].[AirMeals]  WITH CHECK ADD  CONSTRAINT [FK_AirMeals_AirCabins] FOREIGN KEY([AirCabinId])
REFERENCES [dbo].[AirCabins] ([Id])
GO
ALTER TABLE [dbo].[AirMeals] CHECK CONSTRAINT [FK_AirMeals_AirCabins]
GO
ALTER TABLE [dbo].[AirOwns]  WITH CHECK ADD  CONSTRAINT [FK_AirOwns_AirTypes] FOREIGN KEY([AirTypeId])
REFERENCES [dbo].[AirTypes] ([Id])
GO
ALTER TABLE [dbo].[AirOwns] CHECK CONSTRAINT [FK_AirOwns_AirTypes]
GO
ALTER TABLE [dbo].[AirTakeOffStatuses]  WITH CHECK ADD  CONSTRAINT [FK_AirTakeOffStatuses_AirFlights] FOREIGN KEY([AirFlightId])
REFERENCES [dbo].[AirFlights] ([Id])
GO
ALTER TABLE [dbo].[AirTakeOffStatuses] CHECK CONSTRAINT [FK_AirTakeOffStatuses_AirFlights]
GO
ALTER TABLE [dbo].[AirTakeOffStatuses]  WITH CHECK ADD  CONSTRAINT [FK_AirTakeOffStatuses_AirFlightStatuses] FOREIGN KEY([AirFlightStatusId])
REFERENCES [dbo].[AirFlightStatuses] ([Id])
GO
ALTER TABLE [dbo].[AirTakeOffStatuses] CHECK CONSTRAINT [FK_AirTakeOffStatuses_AirFlightStatuses]
GO
ALTER TABLE [dbo].[AriTicketPrices]  WITH CHECK ADD  CONSTRAINT [FK_AriTicketPrices_AirCabinRules] FOREIGN KEY([AirCabinRuleId])
REFERENCES [dbo].[AirCabinRules] ([Id])
GO
ALTER TABLE [dbo].[AriTicketPrices] CHECK CONSTRAINT [FK_AriTicketPrices_AirCabinRules]
GO
ALTER TABLE [dbo].[AriTicketPrices]  WITH CHECK ADD  CONSTRAINT [FK_AriTicketPrices_AirFlightManagements] FOREIGN KEY([AirFlightManagementId])
REFERENCES [dbo].[AirFlightManagements] ([Id])
GO
ALTER TABLE [dbo].[AriTicketPrices] CHECK CONSTRAINT [FK_AriTicketPrices_AirFlightManagements]
GO
ALTER TABLE [dbo].[CampaignsCouponAirFlights]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsCouponAirFlights_AirFlights] FOREIGN KEY([AirFlightId])
REFERENCES [dbo].[AirFlights] ([Id])
GO
ALTER TABLE [dbo].[CampaignsCouponAirFlights] CHECK CONSTRAINT [FK_CampaignsCouponAirFlights_AirFlights]
GO
ALTER TABLE [dbo].[CampaignsCouponAirFlights]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsCouponAirFlights_CampaignsCoupons] FOREIGN KEY([CampaignsCouponId])
REFERENCES [dbo].[CampaignsCoupons] ([Id])
GO
ALTER TABLE [dbo].[CampaignsCouponAirFlights] CHECK CONSTRAINT [FK_CampaignsCouponAirFlights_CampaignsCoupons]
GO
ALTER TABLE [dbo].[CampaignsCouponMembers]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsCouponMembers_CampaignsCoupons] FOREIGN KEY([CampaignsCouponId])
REFERENCES [dbo].[CampaignsCoupons] ([Id])
GO
ALTER TABLE [dbo].[CampaignsCouponMembers] CHECK CONSTRAINT [FK_CampaignsCouponMembers_CampaignsCoupons]
GO
ALTER TABLE [dbo].[CampaignsCouponMembers]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsCouponMembers_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[CampaignsCouponMembers] CHECK CONSTRAINT [FK_CampaignsCouponMembers_Members]
GO
ALTER TABLE [dbo].[CampaignsCoupons]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsCoupons_Campaigns] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaigns] ([Id])
GO
ALTER TABLE [dbo].[CampaignsCoupons] CHECK CONSTRAINT [FK_CampaignsCoupons_Campaigns]
GO
ALTER TABLE [dbo].[CampaignsDiscountMembers]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountMembers_CampaignsDiscounts] FOREIGN KEY([CampaignsDiscountId])
REFERENCES [dbo].[CampaignsDiscounts] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountMembers] CHECK CONSTRAINT [FK_CampaignsDiscountMembers_CampaignsDiscounts]
GO
ALTER TABLE [dbo].[CampaignsDiscountMembers]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountMembers_Members] FOREIGN KEY([MembersId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountMembers] CHECK CONSTRAINT [FK_CampaignsDiscountMembers_Members]
GO
ALTER TABLE [dbo].[CampaignsDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscounts_Campaigns] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaigns] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscounts] CHECK CONSTRAINT [FK_CampaignsDiscounts_Campaigns]
GO
ALTER TABLE [dbo].[CampaignsDiscountStatusNotifications]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountStatusNotifications_CampaignsDiscountStatusNotifications] FOREIGN KEY([CampaignsDiscountId])
REFERENCES [dbo].[CampaignsDiscounts] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountStatusNotifications] CHECK CONSTRAINT [FK_CampaignsDiscountStatusNotifications_CampaignsDiscountStatusNotifications]
GO
ALTER TABLE [dbo].[CampaignsDiscountStatusNotifications]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountStatusNotifications_CompanyStaffs] FOREIGN KEY([CompanyStaffsId])
REFERENCES [dbo].[CompanyStaffs] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountStatusNotifications] CHECK CONSTRAINT [FK_CampaignsDiscountStatusNotifications_CompanyStaffs]
GO
ALTER TABLE [dbo].[CampaignsDiscountStatusNotifications]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountStatusNotifications_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountStatusNotifications] CHECK CONSTRAINT [FK_CampaignsDiscountStatusNotifications_Members]
GO
ALTER TABLE [dbo].[CampaignsDiscountTFItems]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountTFItems_CampaignsDiscounts] FOREIGN KEY([CampaignsDiscountId])
REFERENCES [dbo].[CampaignsDiscounts] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountTFItems] CHECK CONSTRAINT [FK_CampaignsDiscountTFItems_CampaignsDiscounts]
GO
ALTER TABLE [dbo].[CampaignsDiscountTFItems]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsDiscountTFItems_TFItems] FOREIGN KEY([TFItemId])
REFERENCES [dbo].[TFItems] ([Id])
GO
ALTER TABLE [dbo].[CampaignsDiscountTFItems] CHECK CONSTRAINT [FK_CampaignsDiscountTFItems_TFItems]
GO
ALTER TABLE [dbo].[CampaignsFlightCouponsUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsFlightCouponsUsageHistory_CampaignsCoupons] FOREIGN KEY([CampaignsCouponId])
REFERENCES [dbo].[CampaignsCoupons] ([Id])
GO
ALTER TABLE [dbo].[CampaignsFlightCouponsUsageHistory] CHECK CONSTRAINT [FK_CampaignsFlightCouponsUsageHistory_CampaignsCoupons]
GO
ALTER TABLE [dbo].[CampaignsFlightCouponsUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsFlightCouponsUsageHistory_Tickets1] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([Id])
GO
ALTER TABLE [dbo].[CampaignsFlightCouponsUsageHistory] CHECK CONSTRAINT [FK_CampaignsFlightCouponsUsageHistory_Tickets1]
GO
ALTER TABLE [dbo].[CampaignsTFDiscountUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsTFDiscountUsageHistory_CampaignsDiscounts] FOREIGN KEY([CampaignsDiscountsId])
REFERENCES [dbo].[CampaignsDiscounts] ([Id])
GO
ALTER TABLE [dbo].[CampaignsTFDiscountUsageHistory] CHECK CONSTRAINT [FK_CampaignsTFDiscountUsageHistory_CampaignsDiscounts]
GO
ALTER TABLE [dbo].[CampaignsTFDiscountUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsTFDiscountUsageHistory_TFReservelists] FOREIGN KEY([TFReservelistId])
REFERENCES [dbo].[TFReservelists] ([Id])
GO
ALTER TABLE [dbo].[CampaignsTFDiscountUsageHistory] CHECK CONSTRAINT [FK_CampaignsTFDiscountUsageHistory_TFReservelists]
GO
ALTER TABLE [dbo].[CompanyJobs]  WITH CHECK ADD  CONSTRAINT [FK_CompanyJobs_CompanyDepartments] FOREIGN KEY([CompanyDepartmentId])
REFERENCES [dbo].[CompanyDepartments] ([Id])
GO
ALTER TABLE [dbo].[CompanyJobs] CHECK CONSTRAINT [FK_CompanyJobs_CompanyDepartments]
GO
ALTER TABLE [dbo].[CompanyStaffLoginLogs]  WITH CHECK ADD  CONSTRAINT [FK_CompanyStaffLoginLogs_CompanyStaffs] FOREIGN KEY([CompanyStaffId])
REFERENCES [dbo].[CompanyStaffs] ([Id])
GO
ALTER TABLE [dbo].[CompanyStaffLoginLogs] CHECK CONSTRAINT [FK_CompanyStaffLoginLogs_CompanyStaffs]
GO
ALTER TABLE [dbo].[CompanyStaffs]  WITH CHECK ADD  CONSTRAINT [FK_CompanyStaffs_CompanyJobs] FOREIGN KEY([CompanyJobId])
REFERENCES [dbo].[CompanyJobs] ([Id])
GO
ALTER TABLE [dbo].[CompanyStaffs] CHECK CONSTRAINT [FK_CompanyStaffs_CompanyJobs]
GO
ALTER TABLE [dbo].[CompanyStaffsChangePasswordLogs]  WITH CHECK ADD  CONSTRAINT [FK_CompanyStaffsChangePasswordLogs_CompanyStaffs] FOREIGN KEY([CompanyStaffId])
REFERENCES [dbo].[CompanyStaffs] ([Id])
GO
ALTER TABLE [dbo].[CompanyStaffsChangePasswordLogs] CHECK CONSTRAINT [FK_CompanyStaffsChangePasswordLogs_CompanyStaffs]
GO
ALTER TABLE [dbo].[LuggageOrders]  WITH CHECK ADD  CONSTRAINT [FK_LuggageOrders_Luggages] FOREIGN KEY([LuggageId])
REFERENCES [dbo].[Luggages] ([Id])
GO
ALTER TABLE [dbo].[LuggageOrders] CHECK CONSTRAINT [FK_LuggageOrders_Luggages]
GO
ALTER TABLE [dbo].[LuggageOrders]  WITH CHECK ADD  CONSTRAINT [FK_LuggageOrders_TicketInvoicingDetails] FOREIGN KEY([TicketInvoicingDetailId])
REFERENCES [dbo].[TicketInvoicingDetails] ([Id])
GO
ALTER TABLE [dbo].[LuggageOrders] CHECK CONSTRAINT [FK_LuggageOrders_TicketInvoicingDetails]
GO
ALTER TABLE [dbo].[LuggageOrders]  WITH CHECK ADD  CONSTRAINT [FK_LuggageOrders_TransferPayments] FOREIGN KEY([TransferPaymentsId])
REFERENCES [dbo].[TransferPayments] ([Id])
GO
ALTER TABLE [dbo].[LuggageOrders] CHECK CONSTRAINT [FK_LuggageOrders_TransferPayments]
GO
ALTER TABLE [dbo].[Luggages]  WITH CHECK ADD  CONSTRAINT [FK_Luggages_AirFlightManagements] FOREIGN KEY([AirFlightManagementsId])
REFERENCES [dbo].[AirFlightManagements] ([Id])
GO
ALTER TABLE [dbo].[Luggages] CHECK CONSTRAINT [FK_Luggages_AirFlightManagements]
GO
ALTER TABLE [dbo].[MemberLoginLogs]  WITH CHECK ADD  CONSTRAINT [FK_MemberLoginLogs_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[MemberLoginLogs] CHECK CONSTRAINT [FK_MemberLoginLogs_Members]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Countries]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_MemberClasses] FOREIGN KEY([MemberClassId])
REFERENCES [dbo].[MemberClasses] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_MemberClasses]
GO
ALTER TABLE [dbo].[MessageBoxes]  WITH CHECK ADD  CONSTRAINT [FK_MessageBoxes_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[MessageBoxes] CHECK CONSTRAINT [FK_MessageBoxes_Members]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_CompanyStaffs] FOREIGN KEY([CompanyStaffId])
REFERENCES [dbo].[CompanyStaffs] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_CompanyStaffs]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Members]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_MessageBoxes] FOREIGN KEY([MessageBoxId])
REFERENCES [dbo].[MessageBoxes] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_MessageBoxes]
GO
ALTER TABLE [dbo].[MileageDetails]  WITH CHECK ADD  CONSTRAINT [FK_MileageDetails_Members] FOREIGN KEY([MermberIsd])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[MileageDetails] CHECK CONSTRAINT [FK_MileageDetails_Members]
GO
ALTER TABLE [dbo].[MileApply]  WITH CHECK ADD  CONSTRAINT [FK_MileApply_MileageDetails] FOREIGN KEY([EventId])
REFERENCES [dbo].[MileageDetails] ([Id])
GO
ALTER TABLE [dbo].[MileApply] CHECK CONSTRAINT [FK_MileApply_MileageDetails]
GO
ALTER TABLE [dbo].[MileApply]  WITH CHECK ADD  CONSTRAINT [FK_MileApply_MileageDetails1] FOREIGN KEY([ChoseId])
REFERENCES [dbo].[MileageDetails] ([Id])
GO
ALTER TABLE [dbo].[MileApply] CHECK CONSTRAINT [FK_MileApply_MileageDetails1]
GO
ALTER TABLE [dbo].[MileOrders]  WITH CHECK ADD  CONSTRAINT [FK_MileOrders_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[MileOrders] CHECK CONSTRAINT [FK_MileOrders_Members]
GO
ALTER TABLE [dbo].[MileOrders]  WITH CHECK ADD  CONSTRAINT [FK_MileOrders_TransferPayments] FOREIGN KEY([TransferPaymentId])
REFERENCES [dbo].[TransferPayments] ([Id])
GO
ALTER TABLE [dbo].[MileOrders] CHECK CONSTRAINT [FK_MileOrders_TransferPayments]
GO
ALTER TABLE [dbo].[PermissionGroupsAddStaffs]  WITH CHECK ADD  CONSTRAINT [FK_PermissionGroupsAddStaffs_CompanyStaffs] FOREIGN KEY([CompanyStaffsId])
REFERENCES [dbo].[CompanyStaffs] ([Id])
GO
ALTER TABLE [dbo].[PermissionGroupsAddStaffs] CHECK CONSTRAINT [FK_PermissionGroupsAddStaffs_CompanyStaffs]
GO
ALTER TABLE [dbo].[PermissionGroupsAddStaffs]  WITH CHECK ADD  CONSTRAINT [FK_PermissionGroupsAddStaffs_PermissionGroups] FOREIGN KEY([PermissionGroupsId])
REFERENCES [dbo].[PermissionGroups] ([Id])
GO
ALTER TABLE [dbo].[PermissionGroupsAddStaffs] CHECK CONSTRAINT [FK_PermissionGroupsAddStaffs_PermissionGroups]
GO
ALTER TABLE [dbo].[PermissionSettings]  WITH CHECK ADD  CONSTRAINT [FK_PermissionSettings_PermissionGroups] FOREIGN KEY([PermissionGroupsId])
REFERENCES [dbo].[PermissionGroups] ([Id])
GO
ALTER TABLE [dbo].[PermissionSettings] CHECK CONSTRAINT [FK_PermissionSettings_PermissionGroups]
GO
ALTER TABLE [dbo].[PermissionSettings]  WITH CHECK ADD  CONSTRAINT [FK_PermissionSettings_PermissionPageInfos] FOREIGN KEY([PermissionPageInfoId])
REFERENCES [dbo].[PermissionPageInfos] ([Id])
GO
ALTER TABLE [dbo].[PermissionSettings] CHECK CONSTRAINT [FK_PermissionSettings_PermissionPageInfos]
GO
ALTER TABLE [dbo].[SeatGroups]  WITH CHECK ADD  CONSTRAINT [FK_SeatGroups_AirTypes] FOREIGN KEY([AirTypeId])
REFERENCES [dbo].[AirTypes] ([Id])
GO
ALTER TABLE [dbo].[SeatGroups] CHECK CONSTRAINT [FK_SeatGroups_AirTypes]
GO
ALTER TABLE [dbo].[SeatGroups]  WITH CHECK ADD  CONSTRAINT [FK_SeatGroups_SeatAreas] FOREIGN KEY([SeatAreaId])
REFERENCES [dbo].[SeatAreas] ([Id])
GO
ALTER TABLE [dbo].[SeatGroups] CHECK CONSTRAINT [FK_SeatGroups_SeatAreas]
GO
ALTER TABLE [dbo].[TFItems]  WITH CHECK ADD  CONSTRAINT [FK_TFItems_TFCategories] FOREIGN KEY([TFCategoriesId])
REFERENCES [dbo].[TFCategories] ([Id])
GO
ALTER TABLE [dbo].[TFItems] CHECK CONSTRAINT [FK_TFItems_TFCategories]
GO
ALTER TABLE [dbo].[TFOrderlists]  WITH CHECK ADD  CONSTRAINT [FK_TFOrderlists_TFItems] FOREIGN KEY([TFItemsId])
REFERENCES [dbo].[TFItems] ([Id])
GO
ALTER TABLE [dbo].[TFOrderlists] CHECK CONSTRAINT [FK_TFOrderlists_TFItems]
GO
ALTER TABLE [dbo].[TFOrderlists]  WITH CHECK ADD  CONSTRAINT [FK_TFOrderlists_TFOrderStatuses] FOREIGN KEY([TFOrderStatusID])
REFERENCES [dbo].[TFOrderStatuses] ([Id])
GO
ALTER TABLE [dbo].[TFOrderlists] CHECK CONSTRAINT [FK_TFOrderlists_TFOrderStatuses]
GO
ALTER TABLE [dbo].[TFOrderlists]  WITH CHECK ADD  CONSTRAINT [FK_TFOrderlists_TicketDetails] FOREIGN KEY([TicketDetailsId])
REFERENCES [dbo].[TicketDetails] ([Id])
GO
ALTER TABLE [dbo].[TFOrderlists] CHECK CONSTRAINT [FK_TFOrderlists_TicketDetails]
GO
ALTER TABLE [dbo].[TFOrderStatuses]  WITH CHECK ADD  CONSTRAINT [FK_TForderStatus_TFReserves] FOREIGN KEY([TFReserveId])
REFERENCES [dbo].[TFReserves] ([Id])
GO
ALTER TABLE [dbo].[TFOrderStatuses] CHECK CONSTRAINT [FK_TForderStatus_TFReserves]
GO
ALTER TABLE [dbo].[TFReservelists]  WITH CHECK ADD  CONSTRAINT [FK_TFReservelists_TFItems] FOREIGN KEY([TFItemsId])
REFERENCES [dbo].[TFItems] ([Id])
GO
ALTER TABLE [dbo].[TFReservelists] CHECK CONSTRAINT [FK_TFReservelists_TFItems]
GO
ALTER TABLE [dbo].[TFReservelists]  WITH CHECK ADD  CONSTRAINT [FK_TFReservelists_TFReserves] FOREIGN KEY([TFReserveId])
REFERENCES [dbo].[TFReserves] ([Id])
GO
ALTER TABLE [dbo].[TFReservelists] CHECK CONSTRAINT [FK_TFReservelists_TFReserves]
GO
ALTER TABLE [dbo].[TFReserves]  WITH CHECK ADD  CONSTRAINT [FK_TFReserves_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[TFReserves] CHECK CONSTRAINT [FK_TFReserves_Members]
GO
ALTER TABLE [dbo].[TFReserves]  WITH CHECK ADD  CONSTRAINT [FK_TFReserves_TicketDetails] FOREIGN KEY([TicketDetailsId])
REFERENCES [dbo].[TicketDetails] ([Id])
GO
ALTER TABLE [dbo].[TFReserves] CHECK CONSTRAINT [FK_TFReserves_TicketDetails]
GO
ALTER TABLE [dbo].[TFReserves]  WITH CHECK ADD  CONSTRAINT [FK_TFReserves_TransferPayments] FOREIGN KEY([TransferPaymentId])
REFERENCES [dbo].[TransferPayments] ([Id])
GO
ALTER TABLE [dbo].[TFReserves] CHECK CONSTRAINT [FK_TFReserves_TransferPayments]
GO
ALTER TABLE [dbo].[TFWishlists]  WITH CHECK ADD  CONSTRAINT [FK_TFWishlists_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[TFWishlists] CHECK CONSTRAINT [FK_TFWishlists_Members]
GO
ALTER TABLE [dbo].[TFWishlists]  WITH CHECK ADD  CONSTRAINT [FK_TFWishlists_TFItems] FOREIGN KEY([TFItemsId])
REFERENCES [dbo].[TFItems] ([Id])
GO
ALTER TABLE [dbo].[TFWishlists] CHECK CONSTRAINT [FK_TFWishlists_TFItems]
GO
ALTER TABLE [dbo].[TicketCancels]  WITH CHECK ADD  CONSTRAINT [FK_TicketCancels_TicketCancelStatuses] FOREIGN KEY([TicketCancelStatusId])
REFERENCES [dbo].[TicketCancelStatuses] ([Id])
GO
ALTER TABLE [dbo].[TicketCancels] CHECK CONSTRAINT [FK_TicketCancels_TicketCancelStatuses]
GO
ALTER TABLE [dbo].[TicketCancels]  WITH CHECK ADD  CONSTRAINT [FK_TicketCancels_Tickets] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([Id])
GO
ALTER TABLE [dbo].[TicketCancels] CHECK CONSTRAINT [FK_TicketCancels_Tickets]
GO
ALTER TABLE [dbo].[TicketCancels]  WITH CHECK ADD  CONSTRAINT [FK_TicketCancels_TransferRefunds] FOREIGN KEY([TransferRefundId])
REFERENCES [dbo].[TransferRefunds] ([Id])
GO
ALTER TABLE [dbo].[TicketCancels] CHECK CONSTRAINT [FK_TicketCancels_TransferRefunds]
GO
ALTER TABLE [dbo].[TicketDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketDetails_AirCabinRules] FOREIGN KEY([AirCabinRuleId])
REFERENCES [dbo].[AirCabinRules] ([Id])
GO
ALTER TABLE [dbo].[TicketDetails] CHECK CONSTRAINT [FK_TicketDetails_AirCabinRules]
GO
ALTER TABLE [dbo].[TicketDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketDetails_Tickets] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([Id])
GO
ALTER TABLE [dbo].[TicketDetails] CHECK CONSTRAINT [FK_TicketDetails_Tickets]
GO
ALTER TABLE [dbo].[TicketDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketDetails_TypeofPassengers] FOREIGN KEY([TypeofPassengerId])
REFERENCES [dbo].[TypeofPassengers] ([Id])
GO
ALTER TABLE [dbo].[TicketDetails] CHECK CONSTRAINT [FK_TicketDetails_TypeofPassengers]
GO
ALTER TABLE [dbo].[TicketInvoicingDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketInvoicingDetails_AirFlightSeats] FOREIGN KEY([AirFlightSeatId])
REFERENCES [dbo].[AirFlightSeats] ([Id])
GO
ALTER TABLE [dbo].[TicketInvoicingDetails] CHECK CONSTRAINT [FK_TicketInvoicingDetails_AirFlightSeats]
GO
ALTER TABLE [dbo].[TicketInvoicingDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketInvoicingDetails_AirMeals] FOREIGN KEY([AirMealId])
REFERENCES [dbo].[AirMeals] ([Id])
GO
ALTER TABLE [dbo].[TicketInvoicingDetails] CHECK CONSTRAINT [FK_TicketInvoicingDetails_AirMeals]
GO
ALTER TABLE [dbo].[TicketInvoicingDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketInvoicingDetails_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[TicketInvoicingDetails] CHECK CONSTRAINT [FK_TicketInvoicingDetails_Countries]
GO
ALTER TABLE [dbo].[TicketInvoicingDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketInvoicingDetails_TicketDetails] FOREIGN KEY([TicketDetailId])
REFERENCES [dbo].[TicketDetails] ([Id])
GO
ALTER TABLE [dbo].[TicketInvoicingDetails] CHECK CONSTRAINT [FK_TicketInvoicingDetails_TicketDetails]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_AirFlights] FOREIGN KEY([AirFlightId])
REFERENCES [dbo].[AirFlights] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_AirFlights]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Members]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_TransferPayments] FOREIGN KEY([TransferPaymentId])
REFERENCES [dbo].[TransferPayments] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_TransferPayments]
GO
ALTER TABLE [dbo].[TransferPayments]  WITH CHECK ADD  CONSTRAINT [FK_TransferPayments_TransferMethods] FOREIGN KEY([TransferMethodsId])
REFERENCES [dbo].[TransferMethods] ([Id])
GO
ALTER TABLE [dbo].[TransferPayments] CHECK CONSTRAINT [FK_TransferPayments_TransferMethods]
GO
ALTER TABLE [dbo].[TransferRefunds]  WITH CHECK ADD  CONSTRAINT [FK_TransferRefunds_TransferMethods] FOREIGN KEY([TransferIMethodsId])
REFERENCES [dbo].[TransferMethods] ([Id])
GO
ALTER TABLE [dbo].[TransferRefunds] CHECK CONSTRAINT [FK_TransferRefunds_TransferMethods]
GO
ALTER TABLE [dbo].[TransferRefunds]  WITH CHECK ADD  CONSTRAINT [FK_TransferRefunds_TransferPayments1] FOREIGN KEY([TransferPaymentsId])
REFERENCES [dbo].[TransferPayments] ([Id])
GO
ALTER TABLE [dbo].[TransferRefunds] CHECK CONSTRAINT [FK_TransferRefunds_TransferPayments1]
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
