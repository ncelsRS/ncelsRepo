using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.Contract.Data
{
    public class ContractRepo : ContractBaseRepo, IContractRepo
    {
        public ContractRepo(TemeContext context) : base(context)
        {
        }

        public async Task<ContractTypeEnum> GetContractType(int contractId)
        {
            return await Repo
                .Where(x => x.Id == contractId)
                .Select(x => x.ContractType)
                .FirstOrDefaultAsync();
        }
    }
}