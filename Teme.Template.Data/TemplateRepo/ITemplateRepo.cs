using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Context.References;

namespace Teme.Template.Data.TemplateRepo
{
    public interface ITemplateRepo
    {
        Task<Ref_Currency> GetCurrency(int id);

        Task<CostWork> GetCostWorks(int contractId);

        Task<Shared.Data.Context.Contract> GetContract(int id);
    }
}
