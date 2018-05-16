USE [ncels]
GO
/****** Object:  Trigger [dbo].[update_Tmcs]    Script Date: 12.06.2017 10:40:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-02-15
-- Description:	Accept TMC
-- =============================================
ALTER TRIGGER [dbo].[update_Tmcs] 
   ON  [dbo].[Tmcs]
   AFTER UPDATE
AS 
BEGIN
	if (update(ReceivingDate))
	begin		
		INSERT INTO [dbo].[I1c_lims_ApplicationEAdmissions]
				([Id]
				  ,[TmcCode]
				  ,[TmcNumber]
				  ,[QuntityVolume]
				  ,[QuntityVolumeStr]
				  ,[Unit]
				  ,[UnitCode]
				  ,[Producer]
				  ,[ProducerExpirationDate]
				  ,[PartyNumber]
				  ,[ShelfNumber]
				  ,[CupboardNumber]
				  ,[WarehouseNumber]
				  ,[DateOfReceiving]
				  ,[Note]
				  ,[ExportDatetime]
				  ,PowerOfAttorneyNumber_1C
				  ,PowerOfAttorneyDatetime_1C)
		SELECT 
			NEWID()
			,Tt.Id
			,Tt.Number
			,Tt.CountFact
			,Tt.CountFact
			,(select Top 1 Name from Dictionaries where Dictionaries.Id = [MeasureTypeDicId])
			,[MeasureTypeDicId]
			, NULL
			, NULL
			, NULL
			, NULL
			, NULL
			, NULL
			, ReceivingDate
			, Tt.[Name]
			, GETDATE()
			, Tiapp.PowerOfAttorneyNumber_1C
			, Tiapp.PowerOfAttorneyDatetime_1C
		FROM inserted AS Tt
		INNER jOIN [dbo].[TmcIns] AS Tin ON Tin.Id = Tt.TmcInId
		INNER JOIN [dbo].[I1c_lims_Applications] AS Tiapp ON Tiapp.Number = Tin.Id
	end
END

