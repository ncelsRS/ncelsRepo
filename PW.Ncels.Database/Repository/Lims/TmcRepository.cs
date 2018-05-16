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
    /// <summary>
    /// ТМЦ
    /// </summary>
    public class TmcRepository : ARepositoryGeneric<Tmc>
    {
        private DbSet<LimsTmcActualView> LimsTmcActualViewSet { get; set; }
        private DbSet<TmcView> TmcViewSet { get; set; }
        public TmcRepository(bool isProxy = true) : base(isProxy)
        {
            LimsTmcActualViewSet = AppContext.LimsTmcActualViews;
            TmcViewSet = AppContext.TmcViews;
        }

        public IQueryable<I1c_lims_ContractProducts> GetI1cLimsContractProducts(
          Expression<Func<I1c_lims_ContractProducts, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.I1c_lims_ContractProducts.Where(filter);
            return AppContext.I1c_lims_ContractProducts;
        }
        
        public void RemoveTmcByPoaId(Guid poaId)
        {
            var tmcs = AppContext.Tmcs.Where(t => t.TmcInId == poaId);
            foreach (var tmc in tmcs)
            {
                if (tmc.StateType == Tmc.TmcStatuses.New)
                    AppContext.Tmcs.Remove(tmc);
            }
            var limsTmcTemps = AppContext.LimsTmcTemps.Where(t => t.TmcInId == poaId);
            foreach (var ltt in limsTmcTemps)
            {
                AppContext.LimsTmcTemps.Remove(ltt);
            }
        }
        
        public virtual IQueryable<LimsTmcActualView> LtaGetAsQuarable(
            Expression<Func<LimsTmcActualView, bool>> filter = null,
            Func<IQueryable<LimsTmcActualView>, IOrderedQueryable<LimsTmcActualView>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<LimsTmcActualView> query = LimsTmcActualViewSet;

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

        public virtual IQueryable<TmcView> TvGetAsQuarable(
            Expression<Func<TmcView, bool>> filter = null,
            Func<IQueryable<TmcView>, IOrderedQueryable<TmcView>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TmcView> query = TmcViewSet;

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
