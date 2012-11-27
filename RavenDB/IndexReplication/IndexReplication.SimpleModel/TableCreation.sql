USE [RavenReplication]
GO

/****** Object:  Table [dbo].[Customers]    Script Date: 11/27/2012 20:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Customers](
	[Id] [nvarchar](50) NULL,
	[FullName] [varchar](max) NULL,
	[JoinedAt] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


