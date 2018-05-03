using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.DeclarantActions
{
    public class DeclarantActionsLogic : IDeclarantActionsLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        private readonly IContractRepo _repo;
        private readonly IContractStatePolicyLogic _contractSp;

        public DeclarantActionsLogic(IContractRepo repo, IContractWorkflowLogic wflogic, IContractStatePolicyLogic contractSp)
        {
            _wflogic = wflogic;
            _contractSp = contractSp;
            _repo = repo;
        }

        public async Task<object> PublishUserAction(string userPromt, string userOption, ContractTypeEnum contractType, string workflowId, int contractId)
        {
            var actions = await _wflogic.GetUserActions(workflowId, "declarant");
            var action = actions
                .FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            if (action == null) throw new ArgumentException("Action not found");
            var executorsIds = new Dictionary<string, IEnumerable<string>> {{ScopeEnum.Coz, new[] {"BossCoz"}}};
            var r = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds, contractType); //ContractTypeEnum.OneToMore

            switch (userOption)
            {
                case UserOptions.Delete:
                    break;
                case UserOptions.SendWithSign:
                case UserOptions.SendWithoutSign:
                    switch (contractType)
                    {
                        case ContractTypeEnum.OneToMore:
                            await _repo.SaveStatePolice(_contractSp.GetStatePolicy("DeclarantSendContractOneToMore", contractId), contractId);
                            break;
                        case ContractTypeEnum.OneToOne:
                            throw new ArgumentException("This OnToOne type");
                            break;
                    }
                    break;
            }
            return new {r};
        }
    }
}