using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository
{
    public abstract class ARepository
    {
        protected ARepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public ARepository(ncelsEntities context)
        {
            AppContext = context;
        }


        protected ncelsEntities AppContext { get; set; }
        // создание контекста базы данных. необходимо использовать using
        public virtual ncelsEntities CreateDatabaseContext()
        {
            return new ncelsEntities();
        }
        public virtual ncelsEntities CreateDatabaseContext(bool isProxy)
        {
            return new ncelsEntities(isProxy);
        }

        public DbSet<T> Set<T>() where T : class
        {
            return AppContext.Set<T>();
        }
    }
}
