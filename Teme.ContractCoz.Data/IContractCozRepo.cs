using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.ContractCoz.Data
{
    public interface IContractCozRepo : IContractBaseRepo
    {
        Task<IQueryable<object>> GetListContract(string statusCode, string permission);
    }
}
