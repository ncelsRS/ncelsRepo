using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Repos.DeclarationRepo;

namespace Teme.Declaration.Data
{
    public class DeclarationRepo : DeclarationBaseRepo, IDeclarationRepo
    {
        public DeclarationRepo(TemeContext context) : base(context)
        {
        }

        /// <summary>
        /// Автосохранение заявления
        /// </summary>
        /// <param name="data">Type</param>
        /// <param name="fieldName">наименование поля</param>
        /// <param name="value">значение</param>
        /// <param name="id">id договора</param>
        /// <returns></returns>
        public async Task UpdateDeclarationSql(Type data, string fieldName, object value, int id)
        {
            await Context.Database.ExecuteSqlCommandAsync(string.Format(@"UPDATE {0} SET {1} = '{2}' WHERE Id = {3}", data.Name + "s", fieldName, value, id));
        }
    }
}
