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
        private TemplateRepo _templateRepo;

        public FillTemplateLogic(TemplateRepo templateRepo)
        {
            _templateRepo = templateRepo;
        }

        public string checkNull(object temp)
        {
            return temp == null ? "" : temp.ToString();
        }

        public void FillContract(Dictionary<string, string> dictionary, Shared.Data.Context.Contract contract)
        {
            //Контракт
            dictionary.Add(TemplateAttributes.tttContractMedicalDeviceNameRuttt.ToString(), checkNull(contract.MedicalDeviceNameRu));

            if (contract.Declarant != null)
            {
                //Заявитель
                dictionary.Add(TemplateAttributes.tttDeclarantOrgformIdttt.ToString(), checkNull(contract.Declarant.OrganizationFormId));
                dictionary.Add(TemplateAttributes.tttDeclarantNameRuttt.ToString(), checkNull(contract.Declarant.NameRu));
                dictionary.Add(TemplateAttributes.tttDeclarantNameEnttt.ToString(), checkNull(contract.Declarant.NameEn));
             }

            if (contract.DeclarantDetail != null)
            {
                //Детали заявителя
                dictionary.Add(TemplateAttributes.tttDeclarantBossLastNamettt.ToString(), checkNull(contract.DeclarantDetail.BossLastName));
                dictionary.Add(TemplateAttributes.tttDeclarantBossFirstNamettt.ToString(), checkNull(contract.DeclarantDetail.BossFirstName));
                dictionary.Add(TemplateAttributes.tttDeclarantBossMiddleNamettt.ToString(), checkNull(contract.DeclarantDetail.BossMiddleName));
                dictionary.Add(TemplateAttributes.tttDeclarantBossPositionRuttt.ToString(), checkNull(contract.DeclarantDetail.BossPositionRu));
            }

            if (contract.PayerDetail != null)
            {
                //Детали плательщика
                if (contract.PayerDetail.Ref_Currency != null)
                {
                    dictionary.Add(TemplateAttributes.tttPayerCurrencyttt.ToString(), checkNull(contract.PayerDetail.Ref_Currency.NameRu));
                }
            }

            //Стоимость контракта
            if (contract.CostWorks != null)
            {
                var costWork = contract.CostWorks.SingleOrDefault();
                dictionary.Add(TemplateAttributes.tttContractCostTotalPricettt.ToString(), checkNull(costWork.TotalPriceWithValueAddedTax));
            }
        }

    }
}
