using System.Threading.Tasks;
using Teme.Contract.Data.Model;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Contract.Logic
{
    public interface IContractLogic : IBaseContractLogic
    {
        Task<object> Create();
        Task<object> ChangeModel(ContractUpdateModel value);
        Task<object> SearchDeclarantResident(string iin);
        Task<object> SearchDeclarantNonResident(int countryId);
        Task<object> AddDeclarant(int contractId, string code);
        Task<object> GetDeclarantById(int id);
        Task<object> SaveCostWork(CostWorkModel[] costWorkModel);
        Task DeleteCostWork(int contractId);
    }
}