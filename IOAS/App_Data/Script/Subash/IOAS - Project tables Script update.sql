USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblSupportDocuments]    Script Date: 07/26/2018 12:23:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupportDocuments](
	[DocId] [int] IDENTITY(1,1) NOT NULL,
	[ProposalId] [int] NULL,
	[ProjectId] [int] NULL,
	[DocName] [nvarchar](200) NULL,
	[AttachmentPath] [nvarchar](200) NULL,
	[AttachmentName] [nvarchar](max) NULL,
	[DocType] [int] NULL,
	[DocUploadUserid] [int] NULL,
	[DocUpload_TS] [datetime] NULL,
	[IsCurrentVersion] [bit] NULL,
 CONSTRAINT [PK_tblSupportDocuments] PRIMARY KEY CLUSTERED 
(
	[DocId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblSupportDocuments] ON
INSERT [dbo].[tblSupportDocuments] ([DocId], [ProposalId], [ProjectId], [DocName], [AttachmentPath], [AttachmentName], [DocType], [DocUploadUserid], [DocUpload_TS], [IsCurrentVersion]) VALUES (1, 5, 5, N'Test 1.pdf', N'ba13d1ea-4477-4053-a2d6-b69009060c1d_Test 1.pdf', N'Test Document', 2, 1, CAST(0x0000A928013F6778 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblSupportDocuments] OFF
/****** Object:  Table [dbo].[tblProjectCoPI]    Script Date: 07/26/2018 12:23:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectCoPI](
	[CoPIId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[Name] [int] NULL,
	[Department] [int] NULL,
	[Designation] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Crtd_TS] [datetime] NULL,
	[CrtdUserId] [int] NULL,
	[Updt_TS] [datetime] NULL,
	[UpdtUserId] [int] NULL,
	[isdeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedUserid] [int] NULL,
 CONSTRAINT [PK_tblProjectCoPI] PRIMARY KEY CLUSTERED 
(
	[CoPIId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMinistryMaster]    Script Date: 07/26/2018 12:23:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMinistryMaster](
	[MinistryId] [int] IDENTITY(1,1) NOT NULL,
	[MinistryName] [nvarchar](200) NULL,
	[Logo] [nvarchar](200) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_tblMinistryMaster] PRIMARY KEY CLUSTERED 
(
	[MinistryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblMinistryMaster] ON
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (1, N'Ministry of Health and Family Welfare', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (2, N'Ministry of New and Renewable Energy', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (3, N'SERB-IRRD, DST', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (4, N'Ministry of Defence', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (5, N'Ministry of Heavy Industries and Public Enterprises', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (6, N'Ministry of Road Transport and Highways', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (7, N'Ministry of Steel', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (8, N'Ministry of Urban Development', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (9, N'Ministry of Textiles', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (10, N'Ministry of Coal', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (11, N'Department of Space', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (12, N'Ministry of Railways', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (13, N'Ministry of Mines', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (14, N'Ministry of Power', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (15, N'Ministry of Environment, Forest and Climate Change', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (16, N'Ministry of Housing and Urban Poverty Alleviation', NULL, NULL)
INSERT [dbo].[tblMinistryMaster] ([MinistryId], [MinistryName], [Logo], [IsDeleted]) VALUES (17, N'Ministry of Petroleum and Natural Gas', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblMinistryMaster] OFF



/** create tblProjectAllocation **/

USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblProjectAllocation]    Script Date: 07/27/2018 14:22:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectAllocation](
	[AllocationId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[AllocationHead] [int] NULL,
	[AllocationValue] [decimal](18, 2) NULL,
	[CrtdUserId] [int] NULL,
	[CrtdTS] [datetime] NULL,
	[UpdtUserId] [int] NULL,
	[UpdtTS] [datetime] NULL,
 CONSTRAINT [PK_tblProjectAllocation] PRIMARY KEY CLUSTERED 
(
	[AllocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




/* tblPIDepartmentMaster add Column and DepartmentCode and update data for it*/
Alter table tblPIDepartmentMaster Add DepartmentCode nvarchar(200)

UPDATE tblPIDepartmentMaster
SET DepartmentCode = 'AE '
WHERE DepartmentId = 1 

UPDATE tblPIDepartmentMaster
SET DepartmentCode='BT'
WHERE DepartmentId = 2

UPDATE tblPIDepartmentMaster
SET DepartmentCode='CE'
WHERE DepartmentId = 3

UPDATE tblPIDepartmentMaster
SET DepartmentCode='CHE'
WHERE DepartmentId = 4

UPDATE tblPIDepartmentMaster
SET DepartmentCode='CVLE'
WHERE DepartmentId = 5

UPDATE tblPIDepartmentMaster
SET DepartmentCode='CSE' 
WHERE DepartmentId = 6

UPDATE tblPIDepartmentMaster
SET DepartmentCode='EE'
WHERE DepartmentId = 7

UPDATE tblPIDepartmentMaster
SET DepartmentCode='ED' 
WHERE DepartmentId = 8

UPDATE tblPIDepartmentMaster
SET DepartmentCode='HSS'
WHERE DepartmentId = 9

UPDATE tblPIDepartmentMaster
SET DepartmentCode='MS'
WHERE DepartmentId = 10

UPDATE tblPIDepartmentMaster
SET DepartmentCode='MAT' 
WHERE DepartmentId = 11

UPDATE tblPIDepartmentMaster
SET DepartmentCode='MECH'
WHERE DepartmentId = 12

UPDATE tblPIDepartmentMaster
SET DepartmentCode='MME'
WHERE DepartmentId = 13

UPDATE tblPIDepartmentMaster
SET DepartmentCode='OE'
WHERE DepartmentId = 14

UPDATE tblPIDepartmentMaster
SET DepartmentCode='PHY' 
WHERE DepartmentId = 15



/* tables added on 01.08.2018 */
USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblProjectEnhancementAllocation]    Script Date: 08/01/2018 18:40:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectEnhancementAllocation](
	[ProjectEnhancementAllocationId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectEnhancementId] [int] NULL,
	[ProjectId] [int] NULL,
	[IsActive] [bit] NULL,
	[AllocationHead] [int] NULL,
	[OldValue] [decimal](18, 2) NULL,
	[EnhancedValue] [decimal](18, 2) NULL,
	[TotalValue] [decimal](18, 2) NULL,
	[CrtdUserId] [int] NULL,
	[CrtdTS] [datetime] NULL,
	[IsCurrentVersion] [bit] NULL,
 CONSTRAINT [PK_tblProjectEnhancementAllocation] PRIMARY KEY CLUSTERED 
(
	[ProjectEnhancementAllocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblProjectEnhancementAllocation] ON
INSERT [dbo].[tblProjectEnhancementAllocation] ([ProjectEnhancementAllocationId], [ProjectEnhancementId], [ProjectId], [IsActive], [AllocationHead], [OldValue], [EnhancedValue], [TotalValue], [CrtdUserId], [CrtdTS], [IsCurrentVersion]) VALUES (1, 1, 1, 1, 1, CAST(10000.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(15000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1)
INSERT [dbo].[tblProjectEnhancementAllocation] ([ProjectEnhancementAllocationId], [ProjectEnhancementId], [ProjectId], [IsActive], [AllocationHead], [OldValue], [EnhancedValue], [TotalValue], [CrtdUserId], [CrtdTS], [IsCurrentVersion]) VALUES (2, 1, 1, 1, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(15000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1)
INSERT [dbo].[tblProjectEnhancementAllocation] ([ProjectEnhancementAllocationId], [ProjectEnhancementId], [ProjectId], [IsActive], [AllocationHead], [OldValue], [EnhancedValue], [TotalValue], [CrtdUserId], [CrtdTS], [IsCurrentVersion]) VALUES (3, 1, 1, 1, 3, CAST(50000.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(55000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1)
INSERT [dbo].[tblProjectEnhancementAllocation] ([ProjectEnhancementAllocationId], [ProjectEnhancementId], [ProjectId], [IsActive], [AllocationHead], [OldValue], [EnhancedValue], [TotalValue], [CrtdUserId], [CrtdTS], [IsCurrentVersion]) VALUES (4, 1, 1, 1, 5, CAST(50000.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(55000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblProjectEnhancementAllocation] OFF
/****** Object:  Table [dbo].[tblProjectEnhancement]    Script Date: 08/01/2018 18:40:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectEnhancement](
	[ProjectEnhancementId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[DocumentReferenceNumber] [nvarchar](200) NULL,
	[IsExtensiononly] [bit] NULL,
	[IsEnhancementonly] [bit] NULL,
	[IsEnhancementWithExtension] [bit] NULL,
	[PresentDueDate] [datetime] NULL,
	[ExtendedDueDate] [datetime] NULL,
	[OldSanctionValue] [decimal](18, 2) NULL,
	[EnhancedSanctionValue] [decimal](18, 2) NULL,
	[TotalEnhancedValue] [decimal](18, 2) NULL,
	[TotalAllocatedValue] [decimal](18, 2) NULL,
	[Status] [int] NULL,
	[CrtdUserId] [int] NULL,
	[CrtsTS] [datetime] NULL,
	[LastUpdtUserId] [int] NULL,
	[LastUpdtTS] [datetime] NULL,
	[AttachmentName] [nvarchar](200) NULL,
	[AttachmentPath] [nvarchar](200) NULL,
	[IsCurrentVersion] [bit] NULL,
 CONSTRAINT [PK_tblProjectEnhancement] PRIMARY KEY CLUSTERED 
(
	[ProjectEnhancementId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblProjectEnhancement] ON
INSERT [dbo].[tblProjectEnhancement] ([ProjectEnhancementId], [ProjectId], [DocumentReferenceNumber], [IsExtensiononly], [IsEnhancementonly], [IsEnhancementWithExtension], [PresentDueDate], [ExtendedDueDate], [OldSanctionValue], [EnhancedSanctionValue], [TotalEnhancedValue], [TotalAllocatedValue], [Status], [CrtdUserId], [CrtsTS], [LastUpdtUserId], [LastUpdtTS], [AttachmentName], [AttachmentPath], [IsCurrentVersion]) VALUES (1, 1, N'Abcd101', NULL, 1, 0, NULL, NULL, CAST(1200000.00 AS Decimal(18, 2)), CAST(20000.00 AS Decimal(18, 2)), CAST(20000.00 AS Decimal(18, 2)), CAST(1400000.00 AS Decimal(18, 2)), 1, 1, NULL, 1, CAST(0x0000A92F0126441B AS DateTime), N'Test Document', N'34c391b2-539e-4b69-810c-1044ef962617_Test Doc.docx', 1)
INSERT [dbo].[tblProjectEnhancement] ([ProjectEnhancementId], [ProjectId], [DocumentReferenceNumber], [IsExtensiononly], [IsEnhancementonly], [IsEnhancementWithExtension], [PresentDueDate], [ExtendedDueDate], [OldSanctionValue], [EnhancedSanctionValue], [TotalEnhancedValue], [TotalAllocatedValue], [Status], [CrtdUserId], [CrtsTS], [LastUpdtUserId], [LastUpdtTS], [AttachmentName], [AttachmentPath], [IsCurrentVersion]) VALUES (2, 1, N'BDIF101', NULL, 0, 1, CAST(0x0000AED700000000 AS DateTime), CAST(0x0000B06100000000 AS DateTime), CAST(1400000.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, 1, CAST(0x0000A92F010C7FC0 AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblProjectEnhancement] ([ProjectEnhancementId], [ProjectId], [DocumentReferenceNumber], [IsExtensiononly], [IsEnhancementonly], [IsEnhancementWithExtension], [PresentDueDate], [ExtendedDueDate], [OldSanctionValue], [EnhancedSanctionValue], [TotalEnhancedValue], [TotalAllocatedValue], [Status], [CrtdUserId], [CrtsTS], [LastUpdtUserId], [LastUpdtTS], [AttachmentName], [AttachmentPath], [IsCurrentVersion]) VALUES (3, 1, N'BDIF101', 1, 0, 0, CAST(0x0000AED700000000 AS DateTime), CAST(0x0000B06500000000 AS DateTime), CAST(1400000.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, 1, CAST(0x0000A92F010DEB3A AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblProjectEnhancement] ([ProjectEnhancementId], [ProjectId], [DocumentReferenceNumber], [IsExtensiononly], [IsEnhancementonly], [IsEnhancementWithExtension], [PresentDueDate], [ExtendedDueDate], [OldSanctionValue], [EnhancedSanctionValue], [TotalEnhancedValue], [TotalAllocatedValue], [Status], [CrtdUserId], [CrtsTS], [LastUpdtUserId], [LastUpdtTS], [AttachmentName], [AttachmentPath], [IsCurrentVersion]) VALUES (4, 1, N'BDIF101', 1, 0, 0, CAST(0x0000AED700000000 AS DateTime), CAST(0x0000B06900000000 AS DateTime), CAST(1400000.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, 1, CAST(0x0000A92F0122689F AS DateTime), NULL, NULL, N'Test Document', N'a107bc5b-a756-440c-9b14-4c40a9a51e5e_Test 1.pdf', 1)
SET IDENTITY_INSERT [dbo].[tblProjectEnhancement] OFF
/****** Object:  Table [dbo].[tblProjectAllocation]    Script Date: 08/01/2018 18:40:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectAllocation](
	[AllocationId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[AllocationHead] [int] NULL,
	[AllocationValue] [decimal](18, 2) NULL,
	[CrtdUserId] [int] NULL,
	[CrtdTS] [datetime] NULL,
	[UpdtUserId] [int] NULL,
	[UpdtTS] [datetime] NULL,
 CONSTRAINT [PK_tblProjectAllocation] PRIMARY KEY CLUSTERED 
(
	[AllocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblProjectAllocation] ON
INSERT [dbo].[tblProjectAllocation] ([AllocationId], [ProjectId], [AllocationHead], [AllocationValue], [CrtdUserId], [CrtdTS], [UpdtUserId], [UpdtTS]) VALUES (1, 1, 1, CAST(10000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1, CAST(0x0000A92F00A7BD10 AS DateTime))
INSERT [dbo].[tblProjectAllocation] ([AllocationId], [ProjectId], [AllocationHead], [AllocationValue], [CrtdUserId], [CrtdTS], [UpdtUserId], [UpdtTS]) VALUES (2, 1, 2, CAST(10000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1, CAST(0x0000A92F00A7BD13 AS DateTime))
INSERT [dbo].[tblProjectAllocation] ([AllocationId], [ProjectId], [AllocationHead], [AllocationValue], [CrtdUserId], [CrtdTS], [UpdtUserId], [UpdtTS]) VALUES (3, 1, 3, CAST(50000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1, CAST(0x0000A92F00A7BD15 AS DateTime))
INSERT [dbo].[tblProjectAllocation] ([AllocationId], [ProjectId], [AllocationHead], [AllocationValue], [CrtdUserId], [CrtdTS], [UpdtUserId], [UpdtTS]) VALUES (4, 1, 5, CAST(50000.00 AS Decimal(18, 2)), 1, CAST(0x0000A92901101F41 AS DateTime), 1, CAST(0x0000A92F00A7BD17 AS DateTime))
SET IDENTITY_INSERT [dbo].[tblProjectAllocation] OFF
