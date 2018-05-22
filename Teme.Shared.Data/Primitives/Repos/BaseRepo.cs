using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        public TemeContext Context { get; }
        public DbSet<TEntity> Repo { get; }

        public BaseRepo(TemeContext context)
        {
            Context = context;
            Repo = Context.Set<TEntity>();
        }

        public IQueryable<AuthUser> GetCurrentUser(int userId)
        {
            return Context.AuthUsers.Where(x => x.Id == userId);
        }

        public async Task Add(TEntity entity)
        {
            await Repo.AddAsync(entity);
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Repo.FindAsync(new { Id = id });
        }

        //        Нужно добавить этот метод в OnRelease в автофаке, однако di dotnet core видимо уже решил эти проблемы
        //        private bool _disposed = false;
        //
        //        protected virtual void Dispose(bool disposing)
        //        {
        //            if (!this._disposed)
        //            {
        //                if (disposing)
        //                {
        //                    Context.Dispose();
        //                }
        //            }
        //
        //            this._disposed = true;
        //        }
        //
        //        public void Dispose()
        //        {
        //            Dispose(true);
        //            GC.SuppressFinalize(this);
        //        }
    }
}
