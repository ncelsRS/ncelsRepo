USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Applicant]    Script Date: 30.11.2017 11:03:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Applicant](
	[Id] [uniqueidentifier] NOT NULL,
	[NameRu] [nvarchar](1000) NULL,
	[NameKz] [nvarchar](1000) NULL,
	[CreateDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
 CONSTRAINT [PK_OBK_Applicant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [ncels].[dbo].[OBK_StageExpDocumentResult] ADD SelectionDate DATETIME NULL
ALTER TABLE [ncels].[dbo].[OBK_StageExpDocumentResult] ADD SelectionTime DATETIME NULL
ALTER TABLE [ncels].[dbo].[OBK_StageExpDocumentResult] ADD SelectionPlace nvarchar(4000) NULL

ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration] ADD ExpertRequest bit NULL
ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration] ADD ApplicantAgreement bit NULL

ALTER TABLE [ncels].[dbo].[OBK_ActReception] ADD ApplicantId UNIQUEIDENTIFIER NULL
ALTER TABLE [ncels].[dbo].[OBK_ActReception] ADD AttachPath NVARCHAR(300) NULL
ALTER TABLE [ncels].[dbo].[OBK_ActReception] ADD Declarer NVARCHAR(1000) NULL
ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration] ADD OBKApplicantParty bit NULL

