using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Data.DTO;
using Teme.Shared.Data.Context;
using Teme.Template.Data;
using Teme.Template.Data.TemplateRepo;

namespace Teme.Template.Logic
{
    public class FillTemplateLogic : IFillTemplateLogic
    {
        private ITemplateRepo _templateRepo;

        public FillTemplateLogic(TemplateRepo templateRepo)
        {
            _templateRepo = templateRepo;
        }

        public string checkNull(object temp)
        {
            return temp == null ? "" : temp.ToString();
        }

        public async Task FillContract(Dictionary<string, string> dictionary, Shared.Data.Context.Contract contract)
        {
            //Контракт
            dictionary.Add(TemplateAttributes.tttContractMedicalDeviceNameRuttt.ToString(), checkNull(contract.MedicalDeviceNameRu));
            dictionary.Add(TemplateAttributes.tttContractMedicalDeviceNameKzttt.ToString(), checkNull(contract.MedicalDeviceNameKz));

            if (contract.Declarant != null)
            {
                //Заявитель
                dictionary.Add(TemplateAttributes.tttDeclarantOrgFormttt.ToString(), checkNull(contract.Declarant.Ref_OrganizationForm.NameRu));
                dictionary.Add(TemplateAttributes.tttDeclarantOrgFormKzttt.ToString(), checkNull(contract.Declarant.Ref_OrganizationForm.NameKz));

                dictionary.Add(TemplateAttributes.tttDeclarantNameRuttt.ToString(), checkNull(contract.Declarant.NameRu));
                dictionary.Add(TemplateAttributes.tttDeclarantNameKzttt.ToString(), checkNull(contract.Declarant.NameKz));
                dictionary.Add(TemplateAttributes.tttDeclarantNameEnttt.ToString(), checkNull(contract.Declarant.NameEn));
            }

            if (contract.DeclarantDetail != null)
            {
                //Детали заявителя
                dictionary.Add(TemplateAttributes.tttDeclarantBossLastNamettt.ToString(), checkNull(contract.DeclarantDetail.BossLastName));
                dictionary.Add(TemplateAttributes.tttDeclarantBossFirstNamettt.ToString(), checkNull(contract.DeclarantDetail.BossFirstName));
                dictionary.Add(TemplateAttributes.tttDeclarantBossMiddleNamettt.ToString(), checkNull(contract.DeclarantDetail.BossMiddleName));
                dictionary.Add(TemplateAttributes.tttDeclarantBossPositionRuttt.ToString(), checkNull(contract.DeclarantDetail.BossPositionRu));
                dictionary.Add(TemplateAttributes.tttDeclarantBossPositionKzttt.ToString(), checkNull(contract.DeclarantDetail.BossPositionKz));

                dictionary.Add(TemplateAttributes.tttDeclarantAddressLegalttt.ToString(), checkNull(contract.DeclarantDetail.LegalAddress));
                var bank = contract.DeclarantDetail.Ref_Bank != null ? contract.DeclarantDetail.Ref_Bank.NameRu : "";
                dictionary.Add(TemplateAttributes.tttDeclarantBankttt.ToString(), checkNull(bank));
                dictionary.Add(TemplateAttributes.tttDeclarantBankSwiftttt.ToString(), checkNull(contract.DeclarantDetail.BankSwift));
                dictionary.Add(TemplateAttributes.tttDeclarantPhonettt.ToString(), checkNull(contract.DeclarantDetail.Phone));
            }

            if (contract.PayerDetail != null)
            {
                //Детали плательщика
                var currency = contract.PayerDetail.Ref_Currency;
                var currencyRu = currency == null ? "" : currency.NameRu;
                var currencyKz = currency == null ? "" : currency.NameKz;
                dictionary.Add(TemplateAttributes.tttPayerCurrencyttt.ToString(), checkNull(currencyRu));
                dictionary.Add(TemplateAttributes.tttPayerCurrencyKzttt.ToString(), checkNull(currencyKz));
            }

            if (contract.Manufactur != null)
            {
                var orgForm = contract.Manufactur.Ref_OrganizationForm;
                var orgFormNameRu = orgForm == null ? "" : orgForm.NameRu;
                var orgFormNameKz = orgForm == null ? "" : orgForm.NameKz;

                dictionary.Add(TemplateAttributes.tttManufacturerOrgFormttt.ToString(), checkNull(orgFormNameRu));
                dictionary.Add(TemplateAttributes.tttManufacturerOrgFormKzttt.ToString(), checkNull(orgFormNameKz));
                dictionary.Add(TemplateAttributes.tttManufacturerNameRuttt.ToString(), checkNull(contract.Manufactur.NameRu));
                dictionary.Add(TemplateAttributes.tttManufacturerNameKzttt.ToString(), checkNull(contract.Manufactur.NameKz));
                dictionary.Add(TemplateAttributes.tttManufacturerNameEnttt.ToString(), checkNull(contract.Manufactur.NameEn));
            }

            if (contract.ManufacturDetail != null)
            {
                dictionary.Add(TemplateAttributes.tttManufacturerAddressLegalttt.ToString(), checkNull(contract.ManufacturDetail.LegalAddress));
                var bank = contract.ManufacturDetail.Ref_Bank != null ? contract.ManufacturDetail.Ref_Bank.NameRu : "";
                dictionary.Add(TemplateAttributes.tttManufacturerBankttt.ToString(), checkNull(bank));
                dictionary.Add(TemplateAttributes.tttManufacturerBankSwiftttt.ToString(), checkNull(contract.ManufacturDetail.BankSwift));
                dictionary.Add(TemplateAttributes.tttManufacturerPhonettt.ToString(), checkNull(contract.ManufacturDetail.Phone));
            }

            var costWorks = await _templateRepo.GetCostWorks(contract.Id);

            //Стоимость контракта
            if (costWorks != null)
            {
                dictionary.Add(TemplateAttributes.tttContractCostTotalPricettt.ToString(), checkNull(costWorks.TotalPriceWithValueAddedTax));
            }

            if (contract.Payer != null)
            {
                var orgForm = contract.Payer.Ref_OrganizationForm;
                var orgFormNameRu = orgForm == null ? "" : orgForm.NameRu;
                var orgFormNameKz = orgForm == null ? "" : orgForm.NameKz;

                dictionary.Add(TemplateAttributes.tttPayerOrgFormttt.ToString(), checkNull(orgFormNameRu));
                dictionary.Add(TemplateAttributes.tttPayerOrgFormKzttt.ToString(), checkNull(orgFormNameKz));
                dictionary.Add(TemplateAttributes.tttPayerNameRuttt.ToString(), checkNull(contract.Payer.NameRu));
                dictionary.Add(TemplateAttributes.tttPayerNameKzttt.ToString(), checkNull(contract.Payer.NameKz));
                dictionary.Add(TemplateAttributes.tttPayerNameEnttt.ToString(), checkNull(contract.Payer.NameEn));
            }

            if (contract.PayerDetail != null)
            {
                dictionary.Add(TemplateAttributes.tttPayerAddressLegalttt.ToString(), checkNull(contract.PayerDetail.LegalAddress));
                var bank = contract.PayerDetail.Ref_Bank != null ? contract.PayerDetail.Ref_Bank.NameRu : "";
                dictionary.Add(TemplateAttributes.tttPayerBankttt.ToString(), checkNull(bank));
                dictionary.Add(TemplateAttributes.tttPayerBankSwiftttt.ToString(), checkNull(contract.PayerDetail.BankSwift));
                dictionary.Add(TemplateAttributes.tttPayerPhonettt.ToString(), checkNull(contract.PayerDetail.Phone));
            }
        }

    }
}
