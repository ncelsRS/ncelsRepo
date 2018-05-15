using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Data.DTO;
using Teme.Shared.Data.Context;

namespace Teme.Template.Logic
{
    public class FillTemplateObjects : IFillTemplateObjects
    {
        public string checkNull(object temp)
        {
            return temp == null ? "" : temp.ToString();
        }

        public void FillContract(Dictionary<string, string> dictionary, Shared.Data.Context.Contract contract)
        {
            dictionary.Add(TemplateAttributes.tttContractMedicalDeviceNameRuttt.ToString(), checkNull(contract.MedicalDeviceNameRu));
        }

        public void FillDeclarant(Dictionary<string, string> dictionary, Shared.Data.Context.Declarant declarant)
        {
            dictionary.Add(TemplateAttributes.tttDeclarantsOrgformIDttt.ToString(), checkNull(declarant.OrganizationFormId));
            dictionary.Add(TemplateAttributes.tttDeclarantNameRuttt.ToString(), checkNull(declarant.NameRu));
            dictionary.Add(TemplateAttributes.tttDeclarantNameEnttt.ToString(), checkNull(declarant.NameEn));
            var declarantDetails = declarant.DeclarantDetails.OrderByDescending(e => e.DateCreate).FirstOrDefault();
            dictionary.Add(TemplateAttributes.tttDeclarantBossLastNamettt.ToString(), checkNull(declarantDetails.BossLastName));
            dictionary.Add(TemplateAttributes.tttDeclarantBossFirstNamettt.ToString(), checkNull(declarantDetails.BossFirstName));
            dictionary.Add(TemplateAttributes.tttDeclarantBossMiddleNamettt.ToString(), checkNull(declarantDetails.BossMiddleName));
            dictionary.Add(TemplateAttributes.tttDeclarantBossPositionRuttt.ToString(), checkNull(declarantDetails.BossPositionRu));
        }

        public void FillPayer(Dictionary<string, string> dictionary, Declarant payer)
        {
            dictionary.Add(TemplateAttributes.tttDeclarantBossPositionRuttt.ToString(), checkNull(payer.cur));
        }
    }
}
