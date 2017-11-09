
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.Repository.Equipment
{
    public class EquipmentPlanRepository : ARepositoryGeneric<LimsEquipmentPlan>
    {
        public EquipmentPlanRepository(bool isProxy=true):base(isProxy) { }
        
        public IQueryable<LimsPlanEquipmentLink> GetLimsEquipmentLink(Expression<Func<LimsPlanEquipmentLink, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsPlanEquipmentLinks.Where(filter);
            return AppContext.LimsPlanEquipmentLinks;
        }

        public void InsertLimsEquipmentLink(LimsPlanEquipmentLink entity)
        {
            AppContext.LimsPlanEquipmentLinks.Add(entity);
        }

        public void DeleteLimsEquipmentLink(object id)
        {
            LimsPlanEquipmentLink entityToDelete = AppContext.LimsPlanEquipmentLinks.Find(id);
            DeleteLimsEquipmentLink(entityToDelete);
        }

        public void DeleteLimsEquipmentLink(LimsPlanEquipmentLink entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                AppContext.LimsPlanEquipmentLinks.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                UpdateLimsEquipmentLink(entityToDelete);
            }
            else
            {
                AppContext.LimsPlanEquipmentLinks.Remove(entityToDelete);
            }
        }

        public virtual void UpdateLimsEquipmentLink(LimsPlanEquipmentLink entityToUpdate)
        {
            AppContext.LimsPlanEquipmentLinks.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
