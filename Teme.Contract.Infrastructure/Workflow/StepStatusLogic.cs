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

        /// <summary>
        /// Отправка договора в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task SendToNcels(string workflowId)
        {
            var contract = await _repo.GetContractByWorkflowId(workflowId);
            await _repo.RemoveStatePolice(contract.Id);
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InProcessing, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief }
                });
        }

        /// <summary>
        /// Удаление договора
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task DeleteContract(string workflowId)
        {
            var contract = await _repo.GetContractByWorkflowId(workflowId);
            contract.isDeleted = true;
            await _repo.UpdateContract(contract);
        }

        /// <summary>
        /// Распеределение договора по исполнителям
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task SelectExecutorsFirst(string workflowId)
        {
            var contract = await _repo.GetContractByWorkflowId(workflowId);
            await _repo.RemoveStatePolice(contract.Id);
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.InWork, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.InWork, Permission = IntPortal.IsExecutor }
                });

        }
    }
}
