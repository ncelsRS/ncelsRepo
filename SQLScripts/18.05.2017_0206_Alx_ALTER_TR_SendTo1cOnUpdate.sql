SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-05-10
-- Description:	Send data to Db 1C
-- =============================================
ALTER TRIGGER [dbo].[TR_SendToI1cOnUpdate]
   ON  [dbo].[EXP_DirectionToPays]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @sended1CStatusId  uniqueidentifier, @expireStatusId  uniqueidentifier;--, @agreedStatusId uniqueidentifier;
	
	SELECT TOP 1 @sended1CStatusId = [Id]   
	FROM [dbo].[Dictionaries]
	WHERE [Type] = 'ExpDirectionToPayStatus' AND Code = '5' AND [ExpireDate] IS NULL;

	--SELECT TOP 1 @agreedStatusId = [Id]   
	--FROM [dbo].[Dictionaries]
	--WHERE [Type] = 'ExpDirectionToPayStatus' AND Code = '2' AND [ExpireDate] IS NULL;

	SELECT TOP 1 @expireStatusId = [Id]   
	FROM [dbo].[Dictionaries]
	WHERE [Type] = 'ExpDirectionToPayStatus' AND Code = '7' AND [ExpireDate] IS NULL;

	-- Primary direction to pay
	IF ((select [Type] from inserted) = 1)
	BEGIN
		-- Insert primary direction to pay
		IF (UPDATE(StatusId) AND (select StatusId from inserted) = @sended1CStatusId)
		BEGIN
			INSERT INTO [dbo].[I1c_primary_Applications]
			   ([Id]
			   ,[ExportDatetime]
			   ,ImportDatetime
			   ,[ApplicationId]
			   ,[ApplicationNumber]
			   ,[ApplicationType]
			   ,[ApplicationDatetime]
			   ,[Producer]
			   ,[ProducerId]
			   ,[Applicant]
			   ,[ApplicantId]
			   ,[ContractNumber]
			   ,[ContractStartDate]
			   ,[ContractEndDate]
			   ,[ContractId]
			   ,[DoverennostNumber]
			   ,[DoverennostCreatedDate]
			   ,[DoverennostExpiryDate]
			   ,[Address]
			   ,[Phone]
			   ,[Payer]
			   ,[PayerId]
			   ,[PayerAddress]
			   ,[PayerBank]
			   ,[PayerAccount]
			   ,[PayerBIK]
			   ,[PayerSWIFT]
			   ,[PayerBIN]
			   ,[PayerIIN]
			   ,[PayerCurrency]
			   ,[PayerCurrencyCode]
			   ,[PayerCurrencyId]
			   ,[Country]
			   ,[CountryId]
			   ,[IsResident]
			   ,[IsLegal]
			   ,[InvoiceNumber1C]
			   ,[InvoiceDatetime1C]
			   ,[StatementNumber]
			   ,[DrugTradeName]
			   ,[DrugTradeNameKz]
			   ,[DrugPackage]
			   ,[DrugPackageKz]
			   ,[Type]
			   ,[TypeId]
			   ,[TotalPrice])
			SELECT	NEWID() AS [Id]
					,GETDATE() AS [ExportDatetime]
					,NULL AS [ImportDatetime]
					,Td.[Id] AS [ApplicationId]
					,Td.[Number] AS [ApplicationNumber]
					,'ЛС' AS [ApplicationType]
					,Td.[DirectionDate] [ApplicationDatetime]
					,Tmanuf.[NameRu] AS [Producer]
					,Tmanuf.[Id] AS [ProducerId]
					,TApplicant.[NameRu] AS [Applicant]
					,TApplicant.Id AS [ApplicantId]
					,TContract.Number AS [ContractNumber]
					,TContract.StartDate AS [ContractStartDate]
					,TContract.EndDate AS [ContractStartDate]
					,TContract.Id AS [ContractId]
					,TContract.DoverennostNumber AS [DoverennostNumber]
					,TContract.DoverennostCreatedDate AS [DoverennostCreatedDate]
					,TContract.DoverennostExpiryDate AS [DoverennostExpiryDate]
					,TPayer.[AddressFact] AS [Address]
					,TPayer.[Phone] AS [Phone]
					,TPayer.[NameRu] AS [Payer]
					,TPayer.[Id] AS [PayerId]
					,TPayer.[AddressFact] AS [PayerAddress]
					,TPayer.[BankName] AS [PayerBank]
					,TPayer.[PaymentBill] AS [PayerAccount]
					,TPayer.[BankBik] AS [PayerBIK]
					,TPayer.[BankSwift] AS [PayerSWIFT]
					,TPayer.[Bin] AS [PayerBIN]
					,TPayer.[Iin] AS [PayerIIN]
					,TPayer.[BankCurencyDicName] AS [PayerCurrency]
					,Tcurrency.[Code] AS [PayerCurrencyCode]
					,TPayer.[BankCurencyDicId] AS [PayerCurrencyId]
					,TPayer.[CountryName] AS [Country]
					,TPayer.[CountryDicId] AS [CountryId]
					,TPayer.[IsResident] AS [IsResident]
					,(CASE WHEN TOrf.Code = 'fl' THEN 0 ELSE 1 END) AS [IsLegal]
					,NULL AS [InvoiceNumber1C]
					,NULL AS [InvoiceDatetime1C]
					,Tdd.Number AS [StatementNumber]
					,Tdd.NameRu AS [DrugTradeName]
					,Tdd.NameKz AS [DrugTradeNameKz]
					,NULL AS [DrugPackage]
					,NULL AS [DrugPackageKz]
					,Tddtype.NameRu AS [Type]
					,Tdd.TypeId AS [TypeId]
					,Td.TotalPrice AS [TotalPrice]
				FROM [inserted] AS Td
				INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Tdpdd ON Tdpdd.DirectionToPayId = Td.Id
				INNER JOIN [dbo].[EXP_DrugDeclaration] AS Tdd ON Tdd.Id = Tdpdd.DrugDeclarationId
				INNER JOIN [dbo].[EXP_DIC_Type] AS Tddtype ON Tddtype.Id = Tdd.TypeId
				INNER JOIN [dbo].[Contracts] AS TContract ON TContract.Id = Tdd.ContractId
				INNER JOIN [dbo].[OrganizationsView] AS TApplicant ON TApplicant.Id = TContract.ApplicantOrganizationId
				INNER JOIN [dbo].[OrganizationsView] AS Tmanuf ON Tmanuf.Id = TContract.ManufacturerOrganizationId
				INNER JOIN [dbo].[OrganizationsView] AS TPayer ON TPayer.Id = TContract.PayerOrganizationId  
				LEFT OUTER JOIN [dbo].[Dictionaries] AS TOrf ON TOrf.Id = TPayer.OriginalOrgId
				INNER JOIN [dbo].[Dictionaries] AS Tcurrency ON Tcurrency.Id = TPayer.[BankCurencyDicId]

				-- insert dosage as elements
				INSERT INTO [dbo].[I1c_primary_ApplicationElements]
						([Id]
						,[Dosage]
						,[MeasureName]
						,[RegNumber]
						,[ConcentrationRu]
						,[ConcentrationKz]
						,[refApplication])
				SELECT NEWID() AS [Id]
					,[Dosage] AS [Dosage]
					,Tmeas.[Name] AS [MeasureName]
					,[RegNumber] AS [RegNumber]		
					,[ConcentrationRu]
					,[ConcentrationKz]
					,Tdp.Id AS [refApplication]
				FROM [dbo].[EXP_DrugDosage] AS Tdd
				INNER JOIN [dbo].[sr_measures] AS Tmeas ON Tmeas.Id = Tdd.DosageMeasureTypeId
				INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Tdpdd ON Tdpdd.DrugDeclarationId = Tdd.DrugDeclarationId
				INNER JOIN [inserted] AS Tdp ON Tdp.Id = Tdpdd.DirectionToPayId


		END

		-- Add primary refuse direction to pay
		IF (UPDATE(StatusId) AND (select StatusId from inserted) = @expireStatusId)
		BEGIN
			INSERT INTO [dbo].[I1c_primary_ApplicationRefuseState]
			   ([Id]
			   ,[ApplicationNumber]
			   ,[ApplicationDatetime]
			   ,[IsRefuse]
			   ,[RefuseDatetime]
			   ,[refApplication])
			 SELECT
				   NEWID() AS [Id]
				   ,Td.Number AS [ApplicationNumber]
				   ,Td.DirectionDate AS [ApplicationDatetime]
				   ,1 AS [IsRefuse]
				   ,GETDATE()
				   ,Td.Id AS [refApplication]
			 FROM [inserted] AS Td
		END
	END
    ELSE
	BEGIN
		-- Insert translate direction to pay
		IF (UPDATE(StatusId) AND (select StatusId from inserted) = @sended1CStatusId )
		BEGIN
			INSERT INTO [dbo].[I1c_trl_Application]
			   ([Id]
			   ,[ApplicationId]
			   ,[ApplicationNumber]
			   ,[ApplicationDatetime]
			   ,[TranslatorFIO]
			   ,[TranslatorId]
			   ,[PageQuantity]
			   ,[PagePrice]
			   ,[TotalPrice]
			   ,[InvoiceCode_1C]
			   ,[InvoiceNumber_1C]
			   ,[InvoiceDatetime_1C]
			   ,[ExportDatetime]
			   ,[ImportDatetime]
			   ,[refPrimaryApplication])
			 SELECT 
				   NEWID() AS [Id]
				   ,Td.[Id] AS [ApplicationId]
				   ,Td.[Number] AS [ApplicationNumber]
				   ,Td.[DirectionDate] AS ApplicationDatetime
				   ,Te.[FullName] AS [TranslatorFIO]
				   ,Te.[Id] AS [TranslatorId]
				   ,Td.[PageCount] AS [PageQuantity]
				   ,Td.[PriceForPage] AS [PagePrice]
				   ,Td.TotalPrice AS [TotalPrice]
				   ,NULL AS [InvoiceCode_1C]
				   ,NULL AS [InvoiceNumber_1C]
				   ,NULL AS [InvoiceDatetime_1C]
				   ,GETDATE() AS [ExportDatetime]
				   ,NULL AS [ImportDatetime]
				   ,(SELECT TOP 1 DirectionToPayId FROM [dbo].[EXP_DirectionToPays_DrugDeclaration] 
						WHERE DrugDeclarationId = Tdtp.DrugDeclarationId AND DirectionToPayId <> Td.Id) AS [refPrimaryApplication]
			FROM [inserted] AS Td
			INNER JOIN [dbo].[Employees] AS Te ON Te.Id = Td.CreateEmployeeId
			INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Tdtp ON Tdtp.DirectionToPayId = Td.Id
			WHERE Td.[Type] = 2
		END

		-- Add translate refuse direction to pay 
		IF (UPDATE(StatusId) AND (select StatusId from inserted) = @expireStatusId)
		BEGIN
			INSERT INTO [dbo].[I1c_trl_ApplicationRefuseState]
				   ([Id]
				   ,[ApplicationNumber]
				   ,[ApplicationDatetime]
				   ,[IsRefuse]
				   ,[RefuseDatetime]
				   ,[refApplication])
			 SELECT
				   NEWID() AS [Id]
				   ,Td.Number AS [ApplicationNumber]
				   ,Td.DirectionDate AS [ApplicationDatetime]
				   ,1 AS [IsRefuse]
				   ,GETDATE() AS [RefuseDatetime]
				   ,Td.Id AS [refApplication]
			 FROM [inserted] AS Td
		END
	END
END
