using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos
{
    public class BaseRepo<TEntity> : IDisposable where TEntity : BaseEntity
    {
        protected TemeContext Context { get; }
        protected DbSet<TEntity> Repo { get; }

        public BaseRepo(TemeContext context)
        {
            Context = context;
            Repo = Context.Set<TEntity>();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}