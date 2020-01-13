CREATE SCHEMA IF NOT EXISTS "quickpay";

CREATE TABLE IF NOT EXISTS "quickpay"."payments" (
"uniqueid" VARCHAR(40) PRIMARY KEY NOT NULL,
"pay_platid" INT4 NOT NULL,
"appid" VARCHAR(50) NOT NULL,
"out_tradeno" VARCHAR(50) NOT NULL,
"trade_type" VARCHAR(20) NOT NULL,
"business_code" VARCHAR(20) NOT NULL,
"transactionid" VARCHAR(50),
"amount" NUMERIC(18,4) NOT NULL,
"pay_statusid" int4 NOT NULL,
"pay_object" VARCHAR(1024),
"describe" VARCHAR(256)
);

CREATE TABLE IF NOT EXISTS "quickpay"."refunds" (
"uniqueid" VARCHAR(40) PRIMARY KEY NOT NULL,
"pay_platid" INT4 NOT NULL,
"appid" VARCHAR(50) NOT NULL,
"out_tradeno" VARCHAR(50) NOT NULL,
"transactionid" VARCHAR(50),
"out_refundno" VARCHAR(50) NOT NULL,
"refund_amount" NUMERIC(18,4) NOT NULL,
"refundid" VARCHAR(50),
"pay_object" VARCHAR(1024),
"describe" VARCHAR(256)
);

