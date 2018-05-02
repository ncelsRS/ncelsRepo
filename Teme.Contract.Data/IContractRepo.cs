using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.Contract.Data
{
    public interface IContractRepo : IContractBaseRepo
    {
        Task CreateContract(Shared.Data.Context.Contract contract);
        Task CreateDeclarant(Shared.Data.Context.Declarant declarant);
        Task CreateDeclarantDetail(Shared.Data.Context.DeclarantDetail declarantdetail);
        Task<Shared.Data.Context.Contract> GetContract(int id);
        Task<ContractTypeEnum> GetContractType(int contractId);
        Task<Shared.Data.Context.Declarant> GetDeclarant(int id);
        Task UpdateContractSql(Type data, string fieldName, object value, int id);
        Task UpdateContract(Shared.Data.Context.Contract contract);
        Task<Shared.Data.Context.Declarant> SearchDeclarantResident(string iin);
        Task<IEnumerable<Shared.Data.Context.Declarant>> SearchDeclarantNonResident(int countryId);
        Task SaveCostWork(Shared.Data.Context.CostWork costWork);
        Task DeleteCostWork(int contractId);
        Task SaveStatePolice(List<Shared.Data.Context.StatePolicy> statePolicies, int contractId);
        Task<IQueryable<Shared.Data.Context.Contract>> GetListContracts(string contractScope);
    }
}