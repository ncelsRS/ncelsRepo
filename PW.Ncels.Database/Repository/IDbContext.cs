using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Repository
{
    public interface IDbContext : IDisposable
    {
        IQueryable<T> Find<T>() where T : class;

        void MarkAsAdded<T>(T entity) where T : class;

        void MarkAsDeleted<T>(T entity) where T : class;

        void MarkAsModified<T>(T entity) where T : class;

        void Commit(bool withLogging);

        //откатывает изменения во всех модифицированных объектах
        void Rollback();

        // включает или отключает отслеживание изменений объектов
        void EnableTracking(bool isEnable);

        EntityState GetEntityState<T>(T entity) where T : class;

        void SetEntityState<T>(T entity, EntityState state) where T : class;

        // возвращает объект содержащий список объектов с их состоянием
        DbChangeTracker GetChangeTracker();

        DbEntityEntry GetDbEntry<T>(T entity) where T : class;
    }

}
