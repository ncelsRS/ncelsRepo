using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<object>> GetListContract(string statusCode, List<string> permissions)
        {
            return await Task.Run(() => Context.Contracts
                .Include(e => e.StatePolicies)
                .Include(e => e.CostWorks)
                    .Where(e => !e.isDeleted && e.StatePolicies.Any(c => permissions.Contains(c.Permission))).Select(x => new
                    {
                        x.Id, //Id
                        x.Number, //номер
                        x.ContractType,
                        x.WorkflowId,
                        x.ContractForm, // Тип договора
                        x.ContractScope,// Тип услуги
                        ServiceTypes = x.CostWorks.Where(q => q.Ref_PriceList.Ref_ServiceType.ParentId == null).Select(e => new
                        {
                            e.Ref_PriceList.Ref_ServiceType.NameRu, // тип услуги
                            e.Ref_PriceList.Ref_ServiceType.NameKz, // тип услуги
                        }),
                        StartDate = x.DateCreate, // дата отправки
                        DeclarantNameRu = x.Declarant.NameRu, // Наименование заявителя
                        DeclarantNameKz = x.Declarant.NameKz, // Наименование заявителя
                    }));
        }


    }
}
