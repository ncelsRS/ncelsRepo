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
    /// Заявки на доверенность
    /// </summary>
    public class ApplicationPoaRepository: ARepositoryGeneric<TmcIn>
    {
        public ApplicationPoaRepository(bool isProxy=true) : base(isProxy) { }

        public IQueryable<I1c_lims_Contracts> GetI1cLimsContracts(
            Expression<Func<I1c_lims_Contracts, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.I1c_lims_Contracts.Where(filter);
            return AppContext.I1c_lims_Contracts;
        }

        public IQueryable<TmcInView> GetTmcInViews(
            Expression<Func<TmcInView, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.TmcInViews.Where(filter);
            return AppContext.TmcInViews;
        }

        public IQueryable<LimsTmcInView> GetLimsTmcInViews(
            Expression<Func<LimsTmcInView, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsTmcInViews.Where(filter);
            return AppContext.LimsTmcInViews;
        }
        
        public IQueryable<LimsTmcTempView> GetLimsTmcTempViews(
            Expression<Func<LimsTmcTempView, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsTmcTempViews.Where(filter);
            return AppContext.LimsTmcTempViews;
        }

        public IQueryable<LimsTmcTemp> GetLimsTmcTemps(
            Expression<Func<LimsTmcTemp, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsTmcTemps.Where(filter);
            return AppContext.LimsTmcTemps;
        }

        public void AddLimsTmcTemp(LimsTmcTemp model)
        {
            var entityToAdd = AppContext.LimsTmcTemps.FirstOrDefault(lt => lt.TmcId == model.TmcId && lt.TmcInId == model.TmcInId);
            if (entityToAdd == null)
                AppContext.LimsTmcTemps.Add(model);
        }

        public void UpdateLimsTmcTemp(LimsTmcTemp model)
        {
            AppContext.LimsTmcTemps.Attach(model);
            AppContext.Entry(model).State = EntityState.Modified;
        }

        public void DeleteLimsTmcTemp(LimsTmcTemp model)
        {
            var entityToDelete =
                AppContext.LimsTmcTemps.FirstOrDefault(lt => lt.TmcId == model.TmcId && lt.TmcInId == model.TmcInId);

            if (entityToDelete != null)
            {
                if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    AppContext.LimsTmcTemps.Attach(entityToDelete);
                }

                AppContext.LimsTmcTemps.Remove(entityToDelete);
            }
        }
    }
}