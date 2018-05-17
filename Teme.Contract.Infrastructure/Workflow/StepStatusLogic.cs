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

        private async Task<Shared.Data.Context.Contract> GetContract(string workflowId, List<string> scope)
        {
            var contract = await _repo.GetContractByWorkflowId(workflowId);
            await _repo.RemoveStatePolice(contract.Id, scope);
            return contract;
        }

        /// <summary>
        /// Отправка договора в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task SendToNcels(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InProcessing, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.Viewer }
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
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.InWork, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.InWork, Permission = IntPortal.IsExecutor },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.InWork, Permission = IntPortal.Viewer }
                });

        }

        /// <summary>
        /// Согласование договора исполнителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CozExecutorMeetReq(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Отказ Согласовании договора исполнителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CozExecutorNotMeetReq(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredNotAgreement, Permission = IntPortal.IsExecutor },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredNotAgreement, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Согласование договора руководителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CozBossMeetReq(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredSign, Permission = IntPortal.IsCeo },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnAgreement, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        ///  Отказ Согласовании договора руководителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CozBossNotMeetReq(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredNotAgreement, Permission = IntPortal.IsExecutor },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredNotAgreement, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Согласование договора Замгендиром
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CeoMeetReq(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredRegistration, Permission = IntPortal.IsExecutor },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredRegistration, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Отказ Согласовании договора Замгендиром
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CeoNotMeetReq(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredRegistration, Permission = IntPortal.IsExecutor },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredRegistration, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Возврат заявителю
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task CozReturnToDeclarant(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.OnAdjustment, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Регистрация договора
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task RegisterContract(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.Active, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.Active, Permission = IntPortal.Viewer }
                });
        }
    }
}
