USE [ncelsProd]
GO
/****** Object:  Trigger [dbo].[TR_SendActToI1c]    Script Date: 03.11.2017 15:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ayan Akhmetov
-- Create date: 2017-10-16
-- Description:	Send data Act to Db 1C
-- =============================================
CREATE TRIGGER [dbo].[TR_SendActToI1c]
   ON  [dbo].[OBK_CertificateOfCompletion]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    BEGIN
		INSERT INTO [dbo].[I1c_act_ObkApplication]
           ([Id]
           ,[Number]
           ,[CreateDatetime]
           ,[TotalPrice]
           ,[InvoiceNumber1C]
           ,[InvoiceDatetime1C]
           ,[ExportDatetime]
           ,[StageId]
           ,[StageCode]
           ,[StageValue]
           ,[ActNumber1C]
           ,[ActDate1C]
           ,[ActReturnedBack])
		   SELECT 
				NEWID() AS [Id]
				,Td.Number AS Number
				,GETDATE() AS CreateDatetime
				,Td.TotalPrice AS TotalPrice
				,Td.InvoiceNumber1C AS InvoiceNumber1C
				,Td.InvoiceDatetime1C AS InvoiceDatetime1C
				,GETDATE() AS ExportDatetime
				,'2' AS StageId
				,'2' AS StageCode
				,'Ёкспертиза документов' AS StageValue
				, NULL AS ActNumber1C
				, NULL AS ActDate1C
				, 'False' AS ActReturnedBack
		   FROM [inserted] AS Td
	END
END
GO
