using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos.ContractRepo;
using Teme.Shared.Logic.ContractLogic;
using WorkflowCore.Interface;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic
{
    public class ContractLogic : BaseContractLogic<IContractRepo>, IContractLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        private readonly IContractRepo _repo;

        protected ContractLogic(IContractRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
            _repo = repo;
        }

        public async Task<string> Create()
        {
            return await _wflogic.Create();
        }

        public async Task<object> SaveModel(int contractId, string code, string fieldName, string fieldValue)
        {
            //return await _wflogic.SaveModel(contractId, code, fieldName, fieldValue);

            var model = await _repo.GetContract(contractId);
            if(model == null)
            {

            }
        }
    }
}