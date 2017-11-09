
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
    public class EquipmentJournalRepository : ARepositoryGeneric<LimsApplicationJournal>
    {
        public EquipmentJournalRepository(bool isProxy=true):base(isProxy) { }
        
        public IQueryable<LimsApplicationJournalView> GetAsQueryableApplicationJournalViews(
            Expression<Func<LimsApplicationJournalView, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsApplicationJournalViews.Where(filter);
            return AppContext.LimsApplicationJournalViews;

        }


        public IQueryable<LimsEquipmentJournal> GetAsQueryableEquipmentJournals(
            Expression<Func<LimsEquipmentJournal, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsEquipmentJournals.Where(filter);
            return AppContext.LimsEquipmentJournals;
        }

        public void InsertEquipmentJournal(LimsEquipmentJournal entity)
        {
            AppContext.LimsEquipmentJournals.Add(entity);
        }

        public void DeleteEquipmentJournal(object id)
        {
            LimsPlanEquipmentLink entityToDelete = AppContext.LimsPlanEquipmentLinks.Find(id);
            DeleteEquipmentJournal(entityToDelete);
        }

        public void DeleteEquipmentJournal(LimsEquipmentJournal entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                AppContext.LimsEquipmentJournals.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                UpdateEquipmentJournal(entityToDelete);
            }
            else
            {
                AppContext.LimsEquipmentJournals.Remove(entityToDelete);
            }
        }

        public virtual void UpdateEquipmentJournal(LimsEquipmentJournal entityToUpdate)
        {
            AppContext.LimsEquipmentJournals.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }







        public IQueryable<LimsEquipmentJournalRecord> GetAsQueryableEquipmentJournalRecords(
         Expression<Func<LimsEquipmentJournalRecord, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsEquipmentJournalRecords.Where(filter);
            return AppContext.LimsEquipmentJournalRecords;
        }

        public IQueryable<LimsEquipmentJournalRecordView> GetAsQueryableEquipmentJournalRecordViews(
         Expression<Func<LimsEquipmentJournalRecordView, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.LimsEquipmentJournalRecordViews.Where(filter);
            return AppContext.LimsEquipmentJournalRecordViews;
        }

        public void InsertEquipmentJournalRecord(LimsEquipmentJournalRecord entity)
        {
            AppContext.LimsEquipmentJournalRecords.Add(entity);
        }

        public void DeleteEquipmentJournalRecord(object id)
        {
            LimsEquipmentJournalRecord entityToDelete = AppContext.LimsEquipmentJournalRecords.Find(id);
            DeleteEquipmentJournal(entityToDelete);
        }

        public void DeleteEquipmentJournal(LimsEquipmentJournalRecord entityToDelete)
        {
            if (AppContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                AppContext.LimsEquipmentJournalRecords.Attach(entityToDelete);
            }

            var entToDelete = entityToDelete as ISoftDeleteEntity;
            if (entToDelete != null)
            {
                entToDelete.DeleteDate = DateTime.Now;
                UpdateEquipmentJournalRecords(entityToDelete);
            }
            else
            {
                AppContext.LimsEquipmentJournalRecords.Remove(entityToDelete);
            }
        }

        public virtual void UpdateEquipmentJournalRecords(LimsEquipmentJournalRecord entityToUpdate)
        {
            AppContext.LimsEquipmentJournalRecords.Attach(entityToUpdate);
            AppContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}
