using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Lims
{
    public class TmcOffRepository : ARepositoryGeneric<TmcOff>
    {
        internal DbSet<TmcUseOffView> TuovdbSet;

        public TmcOffRepository(bool isProxy= true):base(isProxy)
        {
            TuovdbSet = AppContext.TmcUseOffViews;
        }

        public IQueryable<TmcUseOffView> TuoGetAsQuarable(
            Expression<Func<TmcUseOffView, bool>> filter = null,
            Func<IQueryable<TmcUseOffView>, IOrderedQueryable<TmcUseOffView>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TmcUseOffView> query = TuovdbSet;

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
