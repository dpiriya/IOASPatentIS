USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblProjectStaffCategorywiseBreakup]    Script Date: 09/07/2018 14:49:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProjectStaffCategorywiseBreakup](
	[ProjectStaffCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[ProjectStaffCategory] [int] NULL,
	[NoofStaffs] [int] NULL,
	[SalaryofStaffs] [decimal](18, 2) NULL,
	[CrtdUserId] [int] NULL,
	[CrtdTS] [datetime] NULL,
	[UpdtUserID] [int] NULL,
	[UpdtTS] [datetime] NULL,
 CONSTRAINT [PK_tblProjectStaffCategorywiseBreakup] PRIMARY KEY CLUSTERED 
(
	[ProjectStaffCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblOtherInstituteCoPI]    Script Date: 09/07/2018 15:16:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOtherInstituteCoPI](
	[CoPIId] [int] IDENTITY(1,1) NOT NULL,
	[ProposalId] [int] NULL,
	[Name] [int] NOT NULL,
	[Institution] [int] NULL,
	[Department] [int] NULL,
	[Remarks] [nvarchar](max) NULL,
	[Crtd_TS] [datetime] NULL,
	[CrtdUserId] [int] NULL,
	[Updt_TS] [datetime] NULL,
	[UpdtUserId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedUserid] [int] NULL,
 CONSTRAINT [PK_tblOtherInstituteCoPI] PRIMARY KEY CLUSTERED 
(
	[CoPIId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [IOASDB]
GO
/****** Object:  Table [dbo].[tblCodeControl]    Script Date: 09/11/2018 21:10:30 ******/
SET IDENTITY_INSERT [dbo].[tblCodeControl] ON

INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (66, N'ProjectSubtype', 1, N'Internal', N'SponsoredProject', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (67, N'ProjectSubtype', 2, N'External', N'SponsoredProject', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (68, N'ProjectSource', 1, N'Workflow', N'ProjectSource', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (69, N'ProjectSource', 2, N'Email', N'ProjectSource', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (70, N'ProjectSource', 3, N'HardCopy', N'ProjectSource', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (71, N'ProjectFundingType', 1, N'Indian', N'ProjectFundingType', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (72, N'ProjectFundingType', 2, N'Foreign', N'ProjectFundingType', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (73, N'ProjectFundingType', 3, N'Both', N'ProjectFundingType', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (74, N'ProjectFundedBy', 1, N'Government', N'ProjectFundedBy', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (75, N'ProjectFundedBy', 2, N'Non Government', N'ProjectFundedBy', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (76, N'ProjectFundedBy', 3, N'Both', N'ProjectFundedBy', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (77, N'Indfundgovtbody', 1, N'MHRD', N'IndianProjectFundedByGovt', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (78, N'Indfundgovtbody', 2, N'Ministry', N'IndianProjectFundedByGovt', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (79, N'Indfundgovtbody', 3, N'University', N'IndianProjectFundedByGovt', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (80, N'Indfundnongovtbody', 1, N'Industry', N'IndianProjectFundedbyNonGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (81, N'Indfundnongovtbody', 2, N'Universities', N'IndianProjectFundedbyNonGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (82, N'Indfundnongovtbody', 3, N'Others', N'IndianProjectFundedbyNonGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (83, N'Fornfundgovtbody', 1, N'Govt. Department', N'ForeignProjectFundedbyGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (84, N'Fornfundgovtbody', 2, N'Govt. Universities', N'ForeignProjectFundedbyGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (85, N'Fornfundgovtbody', 3, N'Govt. Others', N'ForeignProjectFundedbyGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (86, N'Fornfundnongovtbody', 1, N'Non Govt. Department', N'ForeignProjectFundedbynonGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (87, N'Fornfundnongovtbody', 2, N'Non Govt. University', N'ForeignProjectFundedbynonGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (88, N'Fornfundnongovtbody', 3, N'Non Govt. Others', N'ForeignProjectFundedbynonGovtBody', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (89, N'TypeofProject', 1, N'IITM Project', N'TypeofProject', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (90, N'TypeofProject', 2, N'Collaborative Project', N'TypeofProject', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (91, N'SponProjectCategory', 1, N'PFMS', N'Sponsored Project Category', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (92, N'SponProjectCategory', 2, N'Non PFMS', N'Sponsored Project Category', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (93, N'ConsProjecttaxtype', 1, N'Taxable', N'ConsProjecttaxtype', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (94, N'ConsProjecttaxtype', 2, N'Non Taxable', N'ConsProjecttaxtype', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (95, N'ConsProjecttaxtype', 3, N'Tax Exempted', N'ConsProjecttaxtype', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (96, N'TaxServiceRegStatus', 1, N'Registered', N'Tax Service Registration Status', NULL, NULL, NULL)
INSERT [dbo].[tblCodeControl] ([CodeID], [CodeName], [CodeValAbbr], [CodeValDetail], [CodeDescription], [UPDT_UserID], [CRTE_TS], [UPDT_TS]) VALUES (97, N'TaxServiceRegStatus', 2, N'Un Registered', N'Tax Service Registration Status', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblCodeControl] OFF

USE [IOASDB]
GO

/****** Object:  Table [dbo].[tblFunctionDocument]    Script Date: 09/12/2018 08:30:56 ******/
SET IDENTITY_INSERT [dbo].[tblFunctionDocument] ON

INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (10, 9, 6)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (11, 9, 7)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (12, 9, 8)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (13, 9, 9)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (14, 10, 6)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (15, 10, 7)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (16, 10, 8)
INSERT [dbo].[tblFunctionDocument] ([FunctionDocumentId], [FunctionId], [DocumentId]) VALUES (17, 10, 9)
SET IDENTITY_INSERT [dbo].[tblFunctionDocument] OFF

/****** Object:  Table [dbo].[tblDocument]    Script Date: 09/12/2018 08:30:56 ******/
SET IDENTITY_INSERT [dbo].[tblDocument] ON

INSERT [dbo].[tblDocument] ([DocumentId], [DocumentName]) VALUES (6, N'Proposal Form')
INSERT [dbo].[tblDocument] ([DocumentId], [DocumentName]) VALUES (7, N'Supporting Document')
INSERT [dbo].[tblDocument] ([DocumentId], [DocumentName]) VALUES (8, N'Budget Document')
INSERT [dbo].[tblDocument] ([DocumentId], [DocumentName]) VALUES (9, N'Covering Letter')
SET IDENTITY_INSERT [dbo].[tblDocument] OFF


/**Date: 17.09.18 **/

ALTER TABLE dbo.tblProject 
ADD JointdevelopmentQuestion nvarchar(50);