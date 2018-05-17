using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.ContractCoz.Data
{
    public class ContractCozRepo : ContractBaseRepo, IContractCozRepo
    {
        public ContractCozRepo(TemeContext context) : base(context)
        {
        }

        /// <summary>
        /// Получение списка договров
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<object>> GetListContract() => await Task.Run(() => Context.Contracts.Where(e => !e.isDeleted).Select(x=> new {
            x.Id, //Id
            x.Number, //номер
            x.ContractType,
            x.WorkflowId,
            x.ContractForm, // Тип договора
            x.ContractScope,// Тип услуги
            ServiceTypes = x.CostWorks.Where(q=> q.Ref_PriceList.Ref_ServiceType.ParentId == null).Select(e=> new {
                e.Ref_PriceList.Ref_ServiceType.NameRu, // тип услуги
                e.Ref_PriceList.Ref_ServiceType.NameKz, // тип услуги
            }),
            StartDate = x.DateCreate, // дата отправки
            DeclarantNameRu = x.Declarant.NameRu, // Наименование заявителя
            DeclarantNameKz = x.Declarant.NameKz, // Наименование заявителя
        }));
    }
}
