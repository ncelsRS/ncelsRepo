/****** Object:  Table [dbo].[EXP_DIC_CorespondenceType]    Script Date: 12.05.2017 15:12:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXP_DIC_CorespondenceSubject](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_CorespondenceSubject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


insert into EXP_DIC_CorespondenceSubject (Id, Code, NameRu, NameKz, DateCreate, IsDeleted)
values (1, '1', N'Замечания', N'Замечания', DATEFROMPARTS(2017,1,1), 0)
go

insert into EXP_DIC_CorespondenceSubject (Id, Code, NameRu, NameKz, DateCreate, IsDeleted)
values (2, '2', N'Отказ в проведение экспертизы за неуплату', N'Отказ в проведение экспертизы за неуплату', DATEFROMPARTS(2017,1,1), 0)
go