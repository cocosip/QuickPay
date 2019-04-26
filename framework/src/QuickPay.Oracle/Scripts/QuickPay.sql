USE [QuickPay]
GO
/****** Object:  Table [dbo].[QP_Refunds]    Script Date: 11/14/2018 22:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QP_Refunds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [nvarchar](50) NOT NULL,
	[PayPlatId] [int] NOT NULL,
	[AppId] [nvarchar](50) NOT NULL,
	[OutTradeNo] [nvarchar](50) NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[OutRefundNo] [nvarchar](50) NOT NULL,
	[RefundAmount] [decimal](18, 4) NOT NULL,
	[RefundId] [nvarchar](50) NULL,
	[PayObject] [nvarchar](max) NULL,
	[Describe] [nvarchar](1024) NULL,
 CONSTRAINT [PK_QP_Refunds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QP_Payments]    Script Date: 11/14/2018 22:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QP_Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [nvarchar](50) NOT NULL,
	[PayPlatId] [int] NOT NULL,
	[AppId] [nvarchar](50) NOT NULL,
	[OutTradeNo] [nvarchar](50) NOT NULL,
	[TradeType] [nvarchar](20) NOT NULL,
	[BusinessCode] [nvarchar](20) NOT NULL,
	[TransactionId] [nvarchar](50) NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[PayStatusId] [int] NOT NULL,
	[PayObject] [nvarchar](max) NULL,
	[Describe] [nvarchar](1024) NULL,
 CONSTRAINT [PK_QP_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO