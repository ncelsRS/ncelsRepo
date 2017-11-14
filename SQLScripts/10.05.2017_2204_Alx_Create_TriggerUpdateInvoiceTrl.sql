SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-05-10
-- Description:	Update information about Invoice
-- =============================================
CREATE TRIGGER [dbo].[TR_update_InvoiceTrl]
   ON  [dbo].[I1c_trl_Application]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

   IF (UPDATE(InvoiceNumber_1C))
   BEGIN
		UPDATE [dbo].[EXP_DirectionToPays]
		   SET [InvoiceCode1C] = Tra.[InvoiceCode_1C]
			  ,[InvoiceNumber1C] = Tra.[InvoiceNumber_1C]
			  ,[InvoiceDatetime1C] = Tra.[InvoiceDatetime_1C]
		   FROM inserted AS Tra
   END
END
GO
