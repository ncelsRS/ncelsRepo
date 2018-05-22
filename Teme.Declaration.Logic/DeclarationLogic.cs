using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Declaration.Data;
using Teme.Declaration.Data.Model;
using Teme.Shared.Logic.DeclarationLogic;

namespace Teme.Declaration.Logic
{
    public class DeclarationLogic : BaseDeclarationLogic<IDeclarationRepo>, IDeclarationLogic
    {
        private readonly IDeclarationRepo _repo;
        public DeclarationLogic(IDeclarationRepo repo) : base(repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Автосохранение заявления
        /// </summary>
        /// <param name="contract">модель</param>
        /// <returns></returns>
        public async Task<object> ChangeModel(DeclarationUpdateModel contract)
        {
            var data = Type.GetType(contract.ClassName + ", Teme.Shared.Data");
            List<Task> tasks = new List<Task>();
            Dictionary<string, string> objectList = JsonConvert.DeserializeObject<Dictionary<string, string>>(contract.Fields.ToString());
            foreach (var o in objectList)
            {
                tasks.Add(_repo.UpdateDeclarationSql(data, o.Key, o.Value, contract.Id));
            }
            Task.WaitAll(tasks.ToArray());
            return new { contractId = contract.Id };
        }
    }
}
