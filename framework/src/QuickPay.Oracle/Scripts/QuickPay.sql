--CREATE SCHEMA IF NOT EXISTS "wechat";

CREATE TABLE "PAYMENTS" IF NOT EXISTS (
"UNIQUEID" NVARCHAR2(40) PRIMARY KEY NOT NULL,
"PAY_PLATID" NUMBER(11,0) NOT NULL,
"APPID" NVARCHAR2(50) NOT NULL,
"OUT_TRADENO" NVARCHAR2(50) NOT NULL,
"TRADE_TYPE" NVARCHAR2(20) NOT NULL,
"BUSINESS_CODE" NVARCHAR2(20) NOT NULL,
"TRANSACTIONID" NVARCHAR2(50) NULL,
"AMOUNT" NUMBER NOT NULL,
"PAY_STATUSID" NUMBER(11,0) NOT NULL,
"PAY_OBJECT" NCLOB NULL,
"DESCRIBE" NVARCHAR2(256) NULL
);

CREATE TABLE "REFUNDS" IF NOT EXISTS (
"UNIQUEID" NVARCHAR2(40) PRIMARY KEY NOT NULL,
"PAY_PLATID" NUMBER(11,0) NOT NULL,
"APPID" NVARCHAR2(50) NOT NULL,
"OUT_TRADENO" NVARCHAR2(50) NOT NULL,
"TRANSACTIONID" NVARCHAR2(50) NULL,
"OUT_REFUNDNO" NVARCHAR2(50) NOT NULL,
"REFUND_AMOUNT" NUMBER NOT NULL,
"REFUNDID" NVARCHAR2(50) NULL,
"PAY_OBJECT" NCLOB NULL,
"DESCRIBE" NVARCHAR2(256) NULL
);