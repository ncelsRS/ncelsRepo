using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.Repository
{
    public class ARepositoryGeneric<TEntity> : ARepository where TEntity : class
    {
        internal DbSet<TEntity> dbSet;

        public ARepositoryGeneric(bool isProxy = true)
        {
            AppContext = CreateDatabaseContext(isProxy);
            this.dbSet = AppContext.Set<TEntity>();
        }

        public ARepositoryGeneric(ncelsEntities context) : base(context)
        {
            this.dbSet = AppContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = GetAsQuarable(filter, orderBy, includeProperties);
            return query.ToList();
        }

        public virtual IQueryable<TEntity> GetAsQuarable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                Update(entityToDelete);
            }
            else
            {
                dbSet.Remove(entityToDelete);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Save()
        {
            AppContext.SaveChanges();
        }
    }
}
