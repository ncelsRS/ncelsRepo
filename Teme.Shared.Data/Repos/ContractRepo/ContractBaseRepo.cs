﻿using Microsoft.EntityFrameworkCore;
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
        public async Task SaveStatePolice(List<StatePolicy> statePolicies, int contractId)
        {
            Context.StatePolicies.RemoveRange(await Context.StatePolicies.Where(e => e.ContractId == contractId).ToListAsync());
            Context.StatePolicies.AddRange(statePolicies);
            await Context.SaveChangesAsync();
        }
    }
}