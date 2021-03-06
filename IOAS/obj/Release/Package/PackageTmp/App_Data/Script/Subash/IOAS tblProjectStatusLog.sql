USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblProjectStatusLog]    Script Date: 08/08/2018 11:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectStatusLog](
	[ProjectStatusLogId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[FromStatus] [int] NULL,
	[ToStatus] [int] NULL,
	[UpdtdUserId] [int] NULL,
	[UpdtdTS] [datetime] NULL,
	[IsCurrentStatus] [bit] NULL,
 CONSTRAINT [PK_tblProjectStatusLog] PRIMARY KEY CLUSTERED 
(
	[ProjectStatusLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE tblProject
ADD AgencyRegisteredName nvarchar(200); 

ALTER TABLE tblProject
ADD ConsProjectSubType int; 


ALTER TABLE tblProjectEnhancement
ADD IsExtensionOnlyCurrentversion bit; 