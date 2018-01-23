USE [ncels]
GO

/****** Object:  Trigger [dbo].[TR_UpdateCertificateOfCompletion]    Script Date: 22.01.2018 19:27:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ayan Akhmetov
-- Create date: 2017-11-06
-- Description:	ActNumber and ActDate From 1c
-- =============================================
ALTER TRIGGER [dbo].[TR_UpdateCertificateOfCompletion]
   ON  [dbo].[I1c_act_ObkApplication]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF((SELECT ZBKCopyId FROM inserted) IS NOT NULL) 
		BEGIN
			UPDATE [dbo].[OBK_CertificateOfCompletion]
					SET	ActNumber1C = Tra.ActNumber1C,
					ActDate1C = Tra.ActDate1C,
					ActReturnedBack = Tra.ActReturnedBack
					FROM inserted AS Tra
			WHERE [OBK_CertificateOfCompletion].[ZBKCopyId] = Tra.ZBKCopyId
		END
	ELSE
		BEGIN
			UPDATE [dbo].[OBK_CertificateOfCompletion]
					SET	ActNumber1C = Tra.ActNumber1C,
					ActDate1C = Tra.ActDate1C,
					ActReturnedBack = Tra.ActReturnedBack
					FROM inserted AS Tra
			WHERE [OBK_CertificateOfCompletion].[Number] = Tra.Number
		END
END
GO


USE [ncels]
GO

/****** Object:  Trigger [dbo].[Tr_insert_ObkPrimaryNotFullPaidStatus]    Script Date: 22.01.2018 19:28:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Ayan Akhmetov
-- Create date: 2017-10-19
-- Description: Not Fully Paid From 1c
-- =============================================
ALTER TRIGGER [dbo].[Tr_insert_ObkPrimaryNotFullPaidStatus]
   ON  [dbo].[I1c_primary_ObkApplicationNotFullPaymentState]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @notFullPaidStatusId uniqueidentifier;
		
	SELECT TOP 1 @notFullPaidStatusId = [Id]
	FROM [dbo].[OBK_Ref_PaymentStatus]
	WHERE Code = 'notFullPaid' and IsDeleted = 'False';

	If ((SELECT IsPaid from inserted) = 1)
	BEGIN
		IF((SELECT ZBKCopyId from inserted) IS NOT NULL)
			BEGIN
				UPDATE [dbo].[OBK_DirectionToPayments]
					SET StatusId = @notFullPaidStatusId,
					IsPaid = 'True',
					IsNotFullPaid = 'True',
					PaymentDatetime = Tra.PaymentDatetime,
					PaymentValue = Tra.PaymentValue,
					PaymentBill = Tra.PaymentBill
					FROM inserted AS Tra
				WHERE ZBKCopy_id = Tra.ZBKCopyId
			END
		ELSE
			BEGIN
				UPDATE [dbo].[OBK_DirectionToPayments]
					SET StatusId = @notFullPaidStatusId,
					IsPaid = 'True',
					IsNotFullPaid = 'True',
					PaymentDatetime = Tra.PaymentDatetime,
					PaymentValue = Tra.PaymentValue,
					PaymentBill = Tra.PaymentBill
					FROM inserted AS Tra
				WHERE ContractId = Tra.refContractId
			END
		
	END
END
GO


USE [ncels]
GO
/****** Object:  Trigger [dbo].[Tr_insert_ObkPrimaryPaidStatus]    Script Date: 22.01.2018 19:28:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ayan Akhmetov
-- Create date: 2017-10-19
-- Description:	Paid From 1c
-- =============================================
ALTER TRIGGER [dbo].[Tr_insert_ObkPrimaryPaidStatus]
   ON  [dbo].[I1c_primary_ObkApplicationPaymentState]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @paidStatusId  uniqueidentifier;
		
	SELECT TOP 1 @paidStatusId = [Id]
	FROM [dbo].[OBK_Ref_PaymentStatus]
	WHERE Code = 'paid' and IsDeleted = 'False';

	IF ((SELECT IsPaid from inserted) = 1)
		BEGIN
			IF((SELECT ZBKCopyId from inserted) IS NOT NULL)
				BEGIN
					UPDATE [dbo].[OBK_DirectionToPayments]
						SET StatusId = @paidStatusId,
						IsPaid = 'True',
						IsNotFullPaid = 'False',
						PaymentDatetime = Tra.PaymentDatetime
					FROM inserted AS Tra
				WHERE ZBKCopy_id = Tra.ZBKCopyId
			END
			ELSE
				BEGIN
					UPDATE [dbo].[OBK_DirectionToPayments]
						SET StatusId = @paidStatusId,
						IsPaid = 'True',
						IsNotFullPaid = 'False',
						PaymentDatetime = Tra.PaymentDatetime
						FROM inserted AS Tra
					WHERE ContractId = Tra.refContractId
				END
			END
END


USE [ncels]
GO
/****** Object:  Trigger [dbo].[TR_UpdateDirectionToPayment]    Script Date: 22.01.2018 19:28:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ayan Akhmetov
-- Create date: 2017-10-16
-- Description:	InvoiceNumber and InvoiceDate From 1c
-- =============================================
ALTER TRIGGER [dbo].[TR_UpdateDirectionToPayment]
   ON  [dbo].[I1c_primary_ObkApplications]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusId  uniqueidentifier;
		
	SELECT TOP 1 @StatusId = [Id]
	FROM [dbo].[OBK_Ref_PaymentStatus]
	WHERE Code = 'reqSign' and IsDeleted = 'False';

	If ((SELECT InvoiceNumber1C from inserted) is not null)
	BEGIN	
		IF((SELECT ZBKCopyId from inserted) is not null)
			BEGIN
				UPDATE [dbo].[OBK_DirectionToPayments]
					SET StatusId = @StatusId,
					InvoiceNumber1C = Tra.InvoiceNumber1C,
					InvoiceDatetime1C = Tra.InvoiceDatetime1C
					FROM inserted AS Tra
				WHERE [OBK_DirectionToPayments].[ZBKCopy_id] = Tra.ZBKCopyId
			END
		ELSE
			BEGIN
				UPDATE [dbo].[OBK_DirectionToPayments]
					SET StatusId = @StatusId,
					InvoiceNumber1C = Tra.InvoiceNumber1C,
					InvoiceDatetime1C = Tra.InvoiceDatetime1C
					FROM inserted AS Tra
				WHERE [OBK_DirectionToPayments].[ContractId] = Tra.ContractId
			END
	END
END
