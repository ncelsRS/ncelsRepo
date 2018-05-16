delete from Dictionaries
where Type='ContractStatus'

INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('0', 'Черновик', 'Черновик', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('1', 'На корректировке', 'На корректировке', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('2', 'На согласовании', 'На согласовании', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('3', 'На подписании у руководителя НЦЭЛС', 'На подписании у руководителя НЦЭЛС', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('4', 'На подписание у заявителя', 'На подписание у заявителя', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('5', 'Активный', 'Активный', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('6', 'Истекший', 'Истекший', 'ContractStatus',0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('7', 'В обработке', 'В обработке', 'ContractStatus',0)
GO





