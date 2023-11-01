USE [TestAppDB]
GO

/****** Object:  Table [dbo].[SampleCabData]    Script Date: 10/31/2023 19:57:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SampleCabData](
	[tpep_pickup_datetime] [datetime] NULL,
	[tpep_dropoff_datetime] [datetime] NULL,
	[passenger_count] [int] NULL,
	[trip_distance] [real] NULL,
	[store_and_fwd_flag] [nvarchar](max) NULL,
	[PULocationID] [int] NULL,
	[DOLocationID] [int] NULL,
	[fare_amount] [real] NULL,
	[tip_amount] [real] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

