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

        protected ARepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public ARepository(NcelsEntities context)
        {
            AppContext = context;
        }


        protected NcelsEntities AppContext { get; set; }
        // создание контекста базы данных. необходимо использовать using
        public virtual NcelsEntities CreateDatabaseContext()
        {
            return new NcelsEntities();
        }
        public virtual NcelsEntities CreateDatabaseContext(bool isProxy)
        {
            return new NcelsEntities(isProxy);
        }

        public DbSet<T> Set<T>() where T : class
        {
            return AppContext.Set<T>();
        }
    }
}
