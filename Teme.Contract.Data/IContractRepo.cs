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
        Task<Shared.Data.Context.Contract> CreateContract(Shared.Data.Context.Contract contract);
        Task<Shared.Data.Context.Declarant> CreateDeclarant(Shared.Data.Context.Declarant declarant);
        Task<Shared.Data.Context.DeclarantDetail> CreateDeclarantDetail(Shared.Data.Context.DeclarantDetail declarantdetail);
        Task<Shared.Data.Context.Contract> GetContract(int id);
        Task<ContractTypeEnum> GetContractType(int contractId);
        Task<Shared.Data.Context.Declarant> GetDeclarant(int id);
        Task<Shared.Data.Context.DeclarantDetail> GetDeclarantDetail(int id);
        Task SaveContract(Shared.Data.Context.Contract contract);
        Task UpdateContractSql(Type data, string fieldName, object value, int id);

        Task UpdateContract(Shared.Data.Context.Contract contract);

        /// <summary>
        /// Поиск заявителя по ИИН
        /// </summary>
        /// <param name="iin"></param>
        /// <returns></returns>
        Task<Shared.Data.Context.Declarant> SearchDeclarantResident(string iin);

        /// <summary>
        /// Поиск заявителя по странам
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<IEnumerable<Shared.Data.Context.Declarant>> SearchDeclarantNonResident(int countryId);
    }
}