SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-05-10
-- Description:	Update information about Invoice
-- =============================================
CREATE TRIGGER [dbo].[TR_update_InvoicePrm]
   ON  [dbo].[I1c_primary_Applications]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

   IF (UPDATE([InvoiceNumber1C]))
   BEGIN
		UPDATE [dbo].[EXP_DirectionToPays]
		   SET [InvoiceCode1C] = NULL
			  ,[InvoiceNumber1C] = Tra.[InvoiceNumber1C]
			  ,[InvoiceDatetime1C] = Tra.[InvoiceDatetime1C]
		   FROM inserted AS Tra
   END
END
GO