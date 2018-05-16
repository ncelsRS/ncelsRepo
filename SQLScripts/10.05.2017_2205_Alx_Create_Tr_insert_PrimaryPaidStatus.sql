SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-05-10
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[Tr_insert_PrimaryPaidStatus]
   ON  [dbo].[I1c_primary_ApplicationPaymentState]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @paidStatusId  uniqueidentifier, @notPaidStatusId  uniqueidentifier;
	
	SELECT TOP 1 @paidStatusId = [Id]   
	FROM [dbo].[Dictionaries]
	WHERE [Type] = 'ExpDirectionToPayStatus' AND Code = '6' AND [ExpireDate] IS NULL;

	SELECT TOP 1 @notPaidStatusId = [Id]   
	FROM [dbo].[Dictionaries]
	WHERE [Type] = 'ExpDirectionToPayStatus' AND Code = '8' AND [ExpireDate] IS NULL;
	

	If ((SELECT IsPaid from inserted) = 1)
	BEGIN
		UPDATE [dbo].[EXP_DirectionToPays]
			SET StatusId = @paidStatusId,
			PaymentComment1C = Tra.PaymentComment,
			PaymentDatetime1C = Tra.PaymentDatetime
			FROM inserted AS Tra
		WHERE ApplicationNumber = Tra.ApplicationNumber
		AND ApplicationDatetime = Tra.ApplicationDatetime

	END
	else
	BEGIN
		UPDATE [dbo].[EXP_DirectionToPays]
			SET StatusId = @notPaidStatusId,
			PaymentComment1C = Tra.PaymentComment,
			PaymentDatetime1C = Tra.PaymentDatetime
			FROM inserted AS Tra
		WHERE ApplicationNumber = Tra.ApplicationNumber
		AND ApplicationDatetime = Tra.ApplicationDatetime
	END
	
	
END
GO
