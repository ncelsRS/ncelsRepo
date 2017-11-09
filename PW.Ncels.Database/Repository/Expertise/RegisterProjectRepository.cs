using System;
using PW.Ncels.Database.DataModel;
using System.Data.Entity;
using System.Linq;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class RegisterProjectRepository : ARepository
    {
        public RegisterProject GetById(Guid id)
        {
            return AppContext.Set<RegisterProject>().FirstOrDefault(e => e.Id == id);
        }

        public RegisterProject GetPreamble(Guid id)
        {
            var context = CreateDatabaseContext(false);
            var preamble = context.RegisterProjects.FirstOrDefault(e => e.Id == id);
            return preamble;
        }
    }
}
