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
    public class EquipmentRepository : ARepositoryGeneric<LimsEquipment>
    {
        public EquipmentRepository(bool isProxy=true):base(isProxy) { }



        public IQueryable<LimsEquipmentAct> GetLimsEquipmentActs(Expression<Func<LimsEquipmentAct, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsEquipmentActs.Where(filter);
            return AppContext.LimsEquipmentActs;
        }

        public void InsertLimsEquipmentAct(LimsEquipmentAct entity)
        {
            AppContext.LimsEquipmentActs.Add(entity);
        }

        public void DeleteLimsEquipmentAct(object id)
        {
            LimsEquipmentAct entityToDelete = AppContext.LimsEquipmentActs.Find(id);
            DeleteLimsEquipmentAct(entityToDelete);
        }

        public void DeleteLimsEquipmentAct(LimsEquipmentAct entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                AppContext.LimsEquipmentActs.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                UpdateLimsEquipmentAct(entityToDelete);
            }
            else
            {
                AppContext.LimsEquipmentActs.Remove(entityToDelete);
            }
        }

        public virtual void UpdateLimsEquipmentAct(LimsEquipmentAct entityToUpdate)
        {
            AppContext.LimsEquipmentActs.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }




        public IQueryable<LimsEquipmentActSparePart> GetLimsEquipmentActSpareParts(Expression<Func<LimsEquipmentActSparePart, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsEquipmentActSpareParts.Where(filter);
            return AppContext.LimsEquipmentActSpareParts;
        }

        public void InsertLimsEquipmentActSparePart(LimsEquipmentActSparePart entity)
        {
            AppContext.LimsEquipmentActSpareParts.Add(entity);
        }

        public void DeleteLimsEquipmentActSparePart(object id)
        {
            LimsEquipmentAct entityToDelete = AppContext.LimsEquipmentActs.Find(id);
            DeleteLimsEquipmentActSparePart(entityToDelete);
        }

        public void DeleteLimsEquipmentActSparePart(LimsEquipmentActSparePart entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                AppContext.LimsEquipmentActSpareParts.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                UpdateLimsEquipmentActSparePart(entityToDelete);
            }
            else
            {
                AppContext.LimsEquipmentActSpareParts.Remove(entityToDelete);
            }
        }

        public virtual void UpdateLimsEquipmentActSparePart(LimsEquipmentActSparePart entityToUpdate)
        {
            AppContext.LimsEquipmentActSpareParts.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public FileLink GetActualActFile(Guid actId)
        {
            //var isContract = AppContext.LimsEquipmentActs.Any(e => e.Id == actId &&  e.DeleteDate == null);
            var attachTypeCode = Dictionary.DicEquipmentAct.DicCode;
            var attachItemTypeCode = Dictionary.DicEquipmentAct.ProtocolOfQualification;
            return
                AppContext.FileLinks.FirstOrDefault(e => e.ParentId == null && e.DocumentId == actId 
                            && e.FileCategory.Type == attachTypeCode
                            && e.FileCategory.Code == attachItemTypeCode);
        }

        public FileLink GetEquipmentFile(Guid equipmentId)
        {
            //var isContract = AppContext.LimsEquipmentActs.Any(e => e.Id == actId &&  e.DeleteDate == null);
            var attachTypeCode = Dictionary.DicEquipmentType.DicCode;
            var attachItemTypeCode = Dictionary.DicEquipmentType.DicCode;
            return AppContext.FileLinks.FirstOrDefault(e => e.ParentId == null && e.DocumentId == equipmentId
                            && e.FileCategory.Type == attachTypeCode
                            && e.FileCategory.Code == attachItemTypeCode);
        }

    }
}
