CREATE TABLE [dbo].[EMP_Ref_DegreeRisk](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_EMP_Ref_DegreeRisk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

alter table EMP_Ref_ServiceType add IsDeleted bit default 0 not null

alter table EMP_Ref_ServiceType add DegreeRiskId uniqueidentifier null
CONSTRAINT FK_EMP_Ref_ServiceType_DegreeRiskId_EMP_Ref_DegreeRisk_Id
		 FOREIGN KEY ([DegreeRiskId])
		 REFERENCES [EMP_Ref_DegreeRisk]([Id])
