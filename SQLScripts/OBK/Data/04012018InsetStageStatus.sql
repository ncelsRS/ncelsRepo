USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (15
           ,'requiresIssuingZBKCopy'
           ,N'������� ������ ����� ���'
           ,N'������� ������ ����� ���'
           ,GETDATE()
           ,0)
GO


