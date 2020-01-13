
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'quickpay')
BEGIN
	EXEC('CREATE SCHEMA [quickpay]')
END;
IF NOT EXISTS (select * from dbo.sysobjects where xtype='U' and Name = 'Payments')
BEGIN
  CREATE TABLE [quickpay].[Payments] (
  [UniqueId] NVARCHAR(40) PRIMARY KEY NOT NULL,
  [PayPlatId] INT NOT NULL,
  [AppId] NVARCHAR(50) NOT NULL,
  [OutTradeNo] NVARCHAR(50) NOT NULL,
  [TradeType] NVARCHAR(20) NOT NULL,
  [BusinessCode] NVARCHAR(20) NOT NULL,
  [TransactionId] NVARCHAR(50) NULL,
  [Amount] DECIMAL(18,4) NOT NULL,
  [PayStatusId] INT NOT NULL,
  [PayObject] NVARCHAR(1024) NULL,
  [Describe] NVARCHAR(256) NULL);
END
GO

IF NOT EXISTS (select * from dbo.sysobjects where xtype='U' and Name = 'Refunds')
BEGIN
  CREATE TABLE [quickpay].[Refunds] (
  [UniqueId] nvarchar(40) PRIMARY KEY NOT NULL,
  [PayPlatId] int NOT NULL,
  [AppId] nvarchar(50) NOT NULL,
  [OutTradeNo] nvarchar(50) NOT NULL,
  [TransactionId] nvarchar(50) NULL,
  [OutRefundNo] nvarchar(50) NOT NULL,
  [RefundAmount] decimal(18,4) NOT NULL,
  [RefundId] nvarchar(50) NULL,
  [PayObject] nvarchar(1024) NULL,
  [Describe] nvarchar(256) NULL);
END
GO