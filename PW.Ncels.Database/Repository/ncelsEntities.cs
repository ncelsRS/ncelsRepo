using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Repository;

namespace PW.Ncels.Database.DataModel
{
    public partial class ncelsEntities : IDbContext
    {
        public ncelsEntities(bool isProxy)
            : this()
        {
            this.Configuration.ProxyCreationEnabled = isProxy;
        }

        //        private ILogDbAction _logger;


        //        public SEC_USER CurrentUser { get; set; }
        //        public DbSet<EntityChange> EntityChanges { get; set; }

        public void MarkAsAdded<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Added;
            Set<T>().Add(entity);
        }

        public void MarkAsDeleted<T>(T entity) where T : class
        {
            Attach(entity);
            Entry(entity).State = EntityState.Deleted;
            Set<T>().Remove(entity);
        }

        public void MarkAsModified<T>(T entity) where T : class
        {
            Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

        public void Commit(bool withLogging)
        {
            BeforeCommit();
          /*  ILogDbAction logger = null;
            if (withLogging)
            {
                logger = new LogDbAction(this);
                logger.Run();
            }*/
            SaveChanges();
            if (withLogging)
            {
//                logger.SaveEvents();
                SaveChanges();
            }
        }

        // откат всех изменений в объектах
        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void EnableTracking(bool isEnable)
        {
            Configuration.AutoDetectChangesEnabled = isEnable;
        }

        public void SetEntityState<T>(T entity, EntityState state) where T : class
        {
            Entry(entity).State = state;
        }

        public DbChangeTracker GetChangeTracker()
        {
            return ChangeTracker;
        }

        public EntityState GetEntityState<T>(T entity) where T : class
        {
            return Entry(entity).State;
        }

        public IQueryable<T> Find<T>() where T : class
        {
            return Set<T>();
        }

        public DbEntityEntry GetDbEntry<T>(T entity) where T : class
        {
            return Entry(entity);
        }

        public void Attach<T>(T entity) where T : class
        {
            if (Entry(entity).State == EntityState.Detached)
            {
                Set<T>().Attach(entity);
            }
        }

        private void BeforeCommit()
        {
            UndoExistAddedEntitys();
        }

        //исправление ситуации, когда есть объекты помеченные как  новые, но при этом существующие в базе данных
        private void UndoExistAddedEntitys()
        {
            var dbEntityEntries =
                GetChangeTracker().Entries().Where(x => x.State == EntityState.Added);
            foreach (DbEntityEntry dbEntityEntry in dbEntityEntries)
            {
                if (GetKeyValue(dbEntityEntry.Entity) !=null)
                {
                    SetEntityState(dbEntityEntry.Entity, EntityState.Unchanged);
                }
            }
        }

        public static Guid? GetKeyValue<T>(T entity) where T : class
        {
            var dbEntity = entity as IEntity;
            if (dbEntity == null)
                throw new ArgumentException("Entity should be IEntity type - " + entity.GetType().Name);

            return dbEntity.Id;
        }
    }
}
