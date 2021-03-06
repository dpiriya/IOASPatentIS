update tblTapal set ProjectNumber = null
alter table tblTapal
alter column ProjectNumber int  


GO

/****** Object:  Table [dbo].[tblLoginDetails]    Script Date: 07/27/2018 18:50:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLoginDetails](
	[LoginDetailId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[LoginTime] [datetime] NULL,
 CONSTRAINT [PK_LoginDetails] PRIMARY KEY CLUSTERED 
(
	[LoginDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GO

/****** Object:  Table [dbo].[tblModules]    Script Date: 08/01/2018 18:04:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblModules](
	[ModuleID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_tblModules] PRIMARY KEY CLUSTERED 
(
	[ModuleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblModules] ADD  CONSTRAINT [DF_tblModules_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO



/****** Object:  Table [dbo].[tblNotification]    Script Date: 08/02/2018 16:23:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblNotification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationType] [varchar](250) NULL,
	[FromUserId] [int] NULL,
	[ToUserId] [int] NULL,
	[Subject] [varchar](500) NULL,
	[Description] [varchar](max) NULL,
	[FunctionURL] [varchar](200) NULL,
	[ReferenceId] [int] NOT NULL,
	[Crt_Ts] [datetime] NULL,
	[Crt_By] [int] NULL,
 CONSTRAINT [PK_tblNotification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

alter table tblNotification
add IsDeleted bit DEFAULT 0;

alter table dbo.tblProposalCoPI
alter column Name int not null