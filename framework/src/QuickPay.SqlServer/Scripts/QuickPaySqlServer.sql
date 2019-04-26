CREATE TABLE [QP_Payments] (
[Id] int NOT NULL,
[UniqueId] nvarchar(50) NOT NULL,
[PayPlatId] int NOT NULL,
[AppId] nvarchar(50) NOT NULL,
[OutTradeNo] nvarchar(50) NOT NULL,
[TradeType] nvarchar(20) NOT NULL,
[BusinessCode] nvarchar(20) NOT NULL,
[TransactionId] nvarchar(50) NULL,
[Amount] decimal(18,4) NULL,
[PayStatusId] int NOT NULL,
[PayObject] nvarchar(MAX) NULL,
[Describe] nvarchar(1000) NULL,
PRIMARY KEY ([Id]) 
)
GO
CREATE INDEX [PK_QP_Payments] ON [QP_Payments] ([Id] ASC)
GO
CREATE INDEX [Payments_UniqueId] ON [QP_Payments] ([UniqueId] ASC)
GO

CREATE TABLE [QP_Refunds] (
[Id] int NOT NULL,
[UniqueId] nvarchar(50) NOT NULL,
[PayPlatId] int NOT NULL,
[AppId] nvarchar(50) NOT NULL,
[OutTradeNo] nvarchar(50) NOT NULL,
[TransactionId] nvarchar(50) NOT NULL,
[OutRefundNo] nvarchar(50) NOT NULL,
[RefundAmount] decimal(18,4) NOT NULL,
[RefundId] nvarchar(50) NULL,
[PayObject] varchar(MAX) NULL,
[Describe] nvarchar(1024) NULL,
PRIMARY KEY ([Id]) 
)
GO
CREATE INDEX [PK_QP_Refunds] ON [QP_Refunds] ([Id] ASC)
GO
CREATE INDEX [Refunds_UniqueId] ON [QP_Refunds] ([UniqueId] ASC)
GO

