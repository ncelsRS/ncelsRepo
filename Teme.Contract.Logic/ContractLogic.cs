using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Interface;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic
{
    public class ContractLogic : IContractLogic
    {
        private readonly ContractWorkflowLogic _wflogic;

        public ContractLogic(ContractWorkflowLogic wflogic)
        {
            _wflogic = wflogic;
        }

        public async Task<string> Create()
        {
            return await _wflogic.Create();
        }

    }
}
