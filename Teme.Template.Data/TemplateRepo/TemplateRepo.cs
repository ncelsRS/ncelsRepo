using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Context.References;
using Teme.Shared.Data.Repos;

namespace Teme.Template.Data.TemplateRepo
{
    public class TemplateRepo : EntityRepo, ITemplateRepo
    {
        public TemplateRepo(TemeContext context) : base(context)
        {
        }

        public async Task<Contract> GetContract(int id)
        {
            return await Context.Contracts.Where(x => x.Id == id)
                .Select(x => new Contract
                {
                    Id = x.Id,
                    MedicalDeviceNameRu = x.MedicalDeviceNameRu,
                    Declarant = x.Declarant,
                    DeclarantDetail = x.DeclarantDetail,
                    Manufactur = x.Manufactur,
                    ManufacturDetail = x.ManufacturDetail,
                    Payer = x.Payer,
                    PayerDetail = x.PayerDetail,
                    CostWorks = x.CostWorks.Select(y => new CostWork
                    {
                        TotalPriceWithValueAddedTax = y.TotalPriceWithValueAddedTax,
                        PriceWithValueAddedTax = y.PriceWithValueAddedTax
                    }
                    ).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<CostWork> GetCostWorks(int contractId) => await Context.CostWorks.FirstOrDefaultAsync(x => x.ContractId == contractId);

        public async Task<Ref_Currency> GetCurrency(int id) => await Context.Ref_Currencies.FirstOrDefaultAsync(x => x.Id == id);

    }
}
