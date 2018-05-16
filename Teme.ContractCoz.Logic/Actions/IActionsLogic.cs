using System.Threading.Tasks;
using Teme.Contract.Logic.Clients;
using Teme.Shared.Logic;

namespace Teme.ContractCoz.Logic.Actions
{
    public interface IActionsLogic: IBaseLogic
    {
        Task<object> DistributionByExecutors(string workflowId, int userId, ContractTypeEnum contractType);
    }
}
