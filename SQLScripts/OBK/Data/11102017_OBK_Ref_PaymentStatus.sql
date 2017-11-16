USE [ncelsProd]
GO

INSERT INTO [dbo].[OBK_Ref_PaymentStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('1ea197e7-0746-45a4-9d91-ac73487e4db2'
           ,'onFormation'
           ,'На формировании'
           ,'На формировании'
           ,'2017-10-10 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_PaymentStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('87b61fc7-c637-4421-9498-9a5bc5b926a2'
           ,'reqSign'
           ,'Требует подписания'
           ,'Требует подписания'
           ,'2017-10-10 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_PaymentStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('aef94fc6-68af-4b44-bbd0-e8751250487f'
           ,'sendToPayment'
           ,'Отправлен на оплату'
           ,'Отправлен на оплату'
           ,'2017-10-10 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_PaymentStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('e7323de9-75b3-4446-8731-ed17412cd11b'
           ,'paid'
           ,'Оплачен'
           ,'Оплачен'
           ,'2017-10-10 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_PaymentStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('f91cf6ba-48db-489a-9605-65a9b5076569'
           ,'notFullPaid'
           ,'Оплачен не полностью'
           ,'Оплачен не полностью'
           ,'2017-10-10 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_PaymentStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('207a8ee0-68e9-4017-aee0-443b127ff2b1'
           ,'paymentExpired'
           ,'Срок оплаты истек'
           ,'Срок оплаты истек'
           ,'2017-10-10 00:00:00.000'
           ,'False')
GO