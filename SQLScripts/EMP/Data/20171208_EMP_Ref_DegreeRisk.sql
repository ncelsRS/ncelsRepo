USE [ncels]
GO

INSERT INTO [dbo].[EMP_Ref_DegreeRisk]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[Code])
     VALUES
           (newid()
           ,N'����� 1'
           ,N'����� 1'
           ,N'1'),
		   (newid()
           ,N'����� 2�'
           ,N'����� 2�'
           ,N'1'),
		   (newid()
           ,N'����� 2�'
           ,N'����� 2�'
           ,N'2'),
		   (newid()
           ,N'����� 3'
           ,N'����� 3'
           ,N'2'),
		   (newid()
           ,N'���� (�������)'
           ,N'���� (�������)'
           ,N'2')
GO


