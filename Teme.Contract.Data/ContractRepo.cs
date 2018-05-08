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
using Teme.Shared.Data.Primitives.OrgScopes;

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

        /// <summary>
        /// Автосохранение договора
        /// </summary>
        /// <param name="data">Type</param>
        /// <param name="fieldName">наименование поля</param>
        /// <param name="value">значение</param>
        /// <param name="id">id договора</param>
        /// <returns></returns>
        public async Task UpdateContractSql(Type data, string fieldName, object value, int id)
        {
            await Context.Database.ExecuteSqlCommandAsync(string.Format(@"UPDATE {0} SET {1} = '{2}' WHERE Id = {3}", data.Name + "s", fieldName, value, id));
        }

        /// <summary>
        /// Поулчение договора по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Context.Contract> GetContract(int id) => await Context.Contracts.FirstOrDefaultAsync(e => e.Id == id);

        /// <summary>
        /// Получение договора по workflowId
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<Context.Contract> GetContractByWorkflowId(string workflowId) => await Context.Contracts.FirstOrDefaultAsync(e => e.WorkflowId == workflowId);

        /// <summary>
        /// Создание договора
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task CreateContract(Context.Contract contract)
        {
            Context.Contracts.Add(contract);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Получение Заявиетоля по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Context.Declarant> GetDeclarant(int id) => await Context.Declarants.Include(x => x.DeclarantDetails).FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

        /// <summary>
        /// Создание заявителя
        /// </summary>
        /// <param name="declarant"></param>
        /// <returns></returns>
        public async Task CreateDeclarant(Context.Declarant declarant)
        {
            Context.Declarants.Add(declarant);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Создание описания заявителя
        /// </summary>
        /// <param name="declarantdetail"></param>
        /// <returns></returns>
        public async Task CreateDeclarantDetail(Context.DeclarantDetail declarantdetail)
        {
            Context.DeclarantDetails.Add(declarantdetail);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Поиск заявителя по ИИН
        /// </summary>
        /// <param name="iin"></param>
        /// <returns></returns>
        public async Task<Context.Declarant> SearchDeclarantResident(string iin) => await Context.Declarants.Include(x => x.DeclarantDetails).FirstOrDefaultAsync(e => e.IdNumber == iin);

        /// <summary>
        /// Поиск заявителя по странам
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Context.Declarant>> SearchDeclarantNonResident(int countryId) => await Context.Declarants.Where(e => e.CountryId == countryId).ToListAsync();

        /// <summary>
        /// Обновление договора
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task UpdateContract(Context.Contract contract)
        {
            Context.Contracts.Update(contract);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавление прайса к договору
        /// </summary>
        /// <param name="costWork"></param>
        /// <returns></returns>
        public async Task SaveCostWork(Context.CostWork costWork)
        {
            Context.CostWorks.Add(costWork);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteCostWork(int contractId)
        {
            var cws = await Context.CostWorks.Where(e => e.ContractId == contractId).ToListAsync();
            Context.CostWorks.RemoveRange(cws);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Получение списка договор
        /// </summary>
        /// <param name="contractScope">scope ЕАЭС или Нац</param>
        /// <returns></returns>
        public async Task<IQueryable<Context.Contract>> GetListContracts(string contractScope) => // string organizationScope
            await Task.Run(() => Context.Contracts.Where(e => !e.isDeleted && e.ContractScope.Contains(contractScope) && e.StatePolicies.Any(x => x.Scope == OrganizationScopeEnum.Ext)));
        
    }
}
