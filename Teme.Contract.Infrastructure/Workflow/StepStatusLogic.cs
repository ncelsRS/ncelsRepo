using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.OrgScopes;
using Teme.Shared.Data.Primitives.Statuses;
using static Teme.Shared.Data.Primitives.Permissions.Permissions;

namespace Teme.Contract.Infrastructure.Workflow
{
    public class StepStatusLogic : IStepStatusLogic
    {
        private readonly IContractRepo _repo;
        public StepStatusLogic(IContractRepo repo)
        {
            _repo = repo;
        }

        public async Task SendToNcels(bool isSignedByDeclarant, string workflowId)
        {
            var contract = await _repo.GetContractByWorkflowId(workflowId);

            var sts = new List<StatePolicy>();
            if (isSignedByDeclarant)
            {
                sts.Add(new StatePolicy { ContractId = 1, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InProcessing, Permission = ExtPortal.IsDeclarant });
                sts.Add(new StatePolicy { ContractId = 1, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief });
            }

            Log.Information($"IsSignedByDeclarant = {isSignedByDeclarant}");
        }
    }
}
