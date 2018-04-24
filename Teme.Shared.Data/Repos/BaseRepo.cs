using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos
{
    public class BaseRepo<TEntity> where TEntity : BaseEntity
    {
        protected TemeContext Context { get; }
        protected DbSet<TEntity> Repo { get; }

        public BaseRepo(TemeContext context)
        {
            Context = context;
            Repo = Context.Set<TEntity>();
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