using System.Collections.Generic;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.OrgScopes;
using Teme.Shared.Data.Primitives.Statuses;
using static Teme.Shared.Data.Primitives.Permissions.Permissions;

namespace Teme.Shared.Logic.ContractLogic
{
    /// <summary>
    /// Все статусы по договорам
    /// </summary>
    public class ContractStatePolicyLogic
    {
        public List<StatePolicy> GetStatePolicy(string stage, int contractId)
        {
            var sts = new List<StatePolicy>();
            switch (stage)
            {
                // создание черновика договора на внешнем портале
                case "DeclarantCreateContract":
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.Draft, Permission = ExtPortal.IsDeclarant });
                    break;
                // отправка договора в ЦОЗ один ко многим
                case "DeclarantSendContractOneToMore": 
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InProcessing, Permission = ExtPortal.IsDeclarant });
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief });
                    break;
                // отправка договора в ЦОЗ один к одному
                case "DeclarantSendContractOneToOne":
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InProcessing, Permission = ExtPortal.IsDeclarant });
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Coz, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief });
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Gv, Status = IntContractStatus.OnDistribution, Permission = IntPortal.IsChief });
                    break;
                case "":
                    sts.Add(new StatePolicy { ContractId = contractId, Scope = OrganizationScopeEnum.Ext, Status = ExtContractStatus.InWork, Permission = ExtPortal.IsDeclarant });
                    break;
                default:
                    break;
            }
            return sts;
        }
    }
}
