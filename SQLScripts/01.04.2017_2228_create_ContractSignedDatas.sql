CREATE TABLE [dbo].[ContractSignedDatas](
	[ContractId] [uniqueidentifier] NOT NULL,
	[ApplicantSig] [ntext] NULL,
	[CeoSign] [ntext] NULL,
 CONSTRAINT [PK_ContractSignedDatas] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ContractSignedDatas]  WITH CHECK ADD  CONSTRAINT [FK_ContractSignedDatas_Contracts] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([Id])
GO

ALTER TABLE [dbo].[ContractSignedDatas] CHECK CONSTRAINT [FK_ContractSignedDatas_Contracts]
GO


