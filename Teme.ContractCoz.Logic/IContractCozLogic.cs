using System.Threading.Tasks;
using Teme.Shared.Logic;

namespace Teme.ContractCoz.Logic
{
    public interface IContractCozLogic : IBaseLogic
    {
        Task<object> DistributionByExecutors(string userPromt, string userOption, string workflowId, int userId);
    }
}
