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

        /// <summary>
        /// ОТправка заявки на платеж в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task SendPaymentToNcels(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InProcessing, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// Распределение договора рук ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task SelectPaymentExecutors(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.InWork, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.InWork, Permission = IntPortal.IsExecutor },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.InWork, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// СОгласование заявки на плажет исполнителем ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task GvExecutorMeet(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// НЕ СОгласование заявки на плажет исполнителем ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task GvExecutorNotMeet(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.IsChief },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.RequiredAgreement, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// СОгласование заявки на плажет рук ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task GvBossMeet(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.OnAgreement, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnAgreement, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Def, Status = IntContractStatus.InWork, Permission = IntPortal.IsExecutor }
                });
        }

        /// <summary>
        /// НЕ СОгласование заявки на плажет рук ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task GvBossNotMeet(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.OnAdjustment, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Def, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer }
                });
        }

        /// <summary>
        /// СОгласование заявки на плажет исполнитель дэф
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task DefExecutorMeet(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.FormationInvoice, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.FormationInvoice, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.FormationInvoice, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Def, Status = IntContractStatus.FormationInvoice, Permission = IntPortal.IsExecutor }
                });
        }

        /// <summary>
        /// НЕ СОгласование заявки на плажет исполнитель дэф
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task DefExecutorNotMeet(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.OnAdjustment, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Def, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnAdjustment, Permission = IntPortal.Viewer }
                });
        }


        public async Task RegisterPayment(string workflowId)
        {
            var contract = await GetContract(workflowId, new List<string> { OrganizationScopeEnum.Ext, OrganizationScopeEnum.Coz, OrganizationScopeEnum.Gv, OrganizationScopeEnum.Def });
            await _repo.SaveStatePolice(
                new List<StatePolicy>{
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.Active, Permission = ExtPortal.IsDeclarant },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.Active, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Def, Status = IntContractStatus.Active, Permission = IntPortal.Viewer },
                    new StatePolicy { ContractId = contract.Id, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.Active, Permission = IntPortal.Viewer }
                });
        }
    }
}
