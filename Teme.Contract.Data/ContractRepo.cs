using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Context = Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos.ContractRepo;
using Teme.Contract.Data.Model;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Teme.Contract.Data
{
    public class ContractRepo : ContractBaseRepo, IContractRepo
    {
        public ContractRepo(Context.TemeContext context) : base(context)
        {
        }

        public async Task<ContractTypeEnum> GetContractType(int contractId)
        {
            return await Repo
                .Where(x => x.Id == contractId)
                .Select(x => x.ContractType)
                .FirstOrDefaultAsync();
        }

        public async Task SaveContract(Context.Contract contract)
        {
            Context.Contracts.Add(contract);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateContractSql(Type data, string fieldName, object value, int id)
        {
            await Context.Database.ExecuteSqlCommandAsync(string.Format(@"UPDATE {0} SET {1} = '{2}' WHERE Id = {3}", data.Name + "s", fieldName, value, id));
        }



        public async Task<Context.Contract> GetContract(int id)
        {
            return await Context.Contracts.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Context.Contract> CreateContract(Context.Contract contract)
        {
            Context.Contracts.Add(contract);
            await Context.SaveChangesAsync();
            return contract;
        }

        public async Task<Context.Declarant> GetDeclarant(int id)
        {
            return await Context.Declarants.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<Context.Declarant> CreateDeclarant(Context.Declarant declarant)
        {
            Context.Declarants.Add(declarant);
            await Context.SaveChangesAsync();
            return declarant;
        }

        public async Task<Context.DeclarantDetail> GetDeclarantDetail(int id)
        {
            return await Context.DeclarantDetails.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Context.DeclarantDetail> CreateDeclarantDetail(Context.DeclarantDetail declarantdetail)
        {
            Context.DeclarantDetails.Add(declarantdetail);
            await Context.SaveChangesAsync();
            return declarantdetail;
        }

        public async Task<Context.Declarant> SearchDeclarantResident(string iin) => await Context.Declarants.FirstOrDefaultAsync(e => e.IdNumber == iin);

        public async Task<IEnumerable<Context.Declarant>> SearchDeclarantNonResident(int countryId) => await Context.Declarants.Where(e => e.CountryId == countryId).ToListAsync();

        public async Task UpdateContract(Context.Contract contract)
        {
            Context.Contracts.Update(contract);
            await Context.SaveChangesAsync();
        }
    }
}