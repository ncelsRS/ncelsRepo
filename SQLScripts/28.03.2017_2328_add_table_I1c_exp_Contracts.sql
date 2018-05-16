CREATE TABLE [dbo].[I1c_exp_Contracts](
	[Id] [uniqueidentifier] NOT NULL,
	[ContractNumber] [nvarchar](500) NOT NULL,
	[ContractDate] [datetime] NOT NULL,
	[ContractUrl] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_I1c_exp_Contracts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_exp_Contracts] ADD  CONSTRAINT [DF_I1c_exp_Contracts_Id]  DEFAULT (newid()) FOR [Id]
GO


