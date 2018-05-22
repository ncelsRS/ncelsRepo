using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.RefBaseRepo;

namespace Teme.Shared.Logic.RefBaseLogic
{
    public class RefBaseLogic<TIRepo, TEntity> : BaseLogic<TIRepo, TEntity>, IRefBaseLogic<TEntity> where TIRepo : IRefBaseRepo<TEntity> where TEntity : BaseEntity
    {
        public RefBaseLogic(TIRepo repo) : base(repo)
        {
        }

        public async Task<int> Add(TEntity entity)
        {
            return await Repo.Add(entity);
        }

        public async Task Delete(int id)
        {
            await Repo.Delete(id);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Repo.GetById(id);
        }

        public async Task Save()
        {
            await Repo.Save();
        }

        public async Task Update(TEntity entity)
        {
            await Repo.Update(entity);
        }
    }
}
