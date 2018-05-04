using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.ContractRepo
{
    public abstract class ContractBaseRepo : BaseRepo<Contract>, IContractBaseRepo
    {
        protected ContractBaseRepo(TemeContext context) : base(context)
        {
        }

        /// <summary>
        /// Сохранение статуса договора
        /// </summary>
        /// <param name="statePolicies">Список статусов, scope-ов</param>
        /// <param name="contractId">Id договора</param>
        /// <returns></returns>
        public async Task SaveStatePolice(List<StatePolicy> statePolicies)
        {
            Context.StatePolicies.AddRange(statePolicies);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Получение списка статусов
        /// </summary>
        /// <param name="contractId">Id договора</param>
        /// <returns></returns>
        public async Task<List<StatePolicy>> GetListStatePolice(int contractId) => await Context.StatePolicies.Where(e => e.ContractId == contractId).ToListAsync();

        /// <summary>
        /// Удаление списка статусов
        /// </summary>
        /// <param name="list">Список статусов</param>
        /// <returns></returns>
        public async Task RemoveStatePolice(List<StatePolicy> list)
        {
            Context.StatePolicies.RemoveRange(list);
            await Context.SaveChangesAsync();
        }
    }
}