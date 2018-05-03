using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.ContractCoz.Data
{
    public class ContractCozRepo : ContractBaseRepo, IContractCozRepo
    {
        protected ContractCozRepo(TemeContext context) : base(context)
        {
        }
    }
}
