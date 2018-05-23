using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.ContractCoz.Data
{
    public interface IContractCozRepo : IContractBaseRepo
    {
        Task<IEnumerable<object>> GetListContract(string statusCode, List<string> permissions);
    }
}
