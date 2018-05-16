using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.Repository.Lims
{
    /// <summary>
    /// Заявки на получение 
    /// </summary>
    public class OrderTmcRepository: ARepositoryGeneric<TmcOut>
    {
        internal DbSet<TmcOutCount> TmcOutCountSet;
        internal DbSet<LimsTmcOutView> LimsTmcOutViewSet;

        public OrderTmcRepository(bool isProxy = true):base(isProxy)
        {
            TmcOutCountSet = AppContext.TmcOutCounts;
            LimsTmcOutViewSet = AppContext.LimsTmcOutViews;
        }

        public IQueryable<TmcOutCount> TmcOutCountGetAsQuarable(
            Expression<Func<TmcOutCount, bool>> filter = null,
            Func<IQueryable<TmcOutCount>, IOrderedQueryable<TmcOutCount>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TmcOutCount> query = TmcOutCountSet;

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

        public void TocInsert(TmcOutCount entity)
        {
            TmcOutCountSet.Add(entity);
        }

        public void TocDelete(object id)
        {
            TmcOutCount entityToDelete = TmcOutCountSet.Find(id);
            TocUpdate(entityToDelete);
        }

        public void TocDelete(TmcOutCount entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                TmcOutCountSet.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                TocUpdate(entityToDelete);
            }
            else
            {
                TmcOutCountSet.Remove(entityToDelete);
            }
        }

        public void TocUpdate(TmcOutCount entityToUpdate)
        {
            TmcOutCountSet.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IQueryable<LimsTmcOutView> LimsTmcOutViewGetAsQuarable(
             Expression<Func<LimsTmcOutView, bool>> filter = null,
             Func<IQueryable<LimsTmcOutView>, IOrderedQueryable<LimsTmcOutView>> orderBy = null,
             string includeProperties = "")
        {
            IQueryable<LimsTmcOutView> query = LimsTmcOutViewSet;

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

    }
}
