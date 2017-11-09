using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Lims
{
    public class ApplicationPoaRepository: ARepositoryGeneric<TmcIn>
    {
        public IQueryable<I1c_lims_Contracts> GetI1cLimsContracts(
            Expression<Func<I1c_lims_Contracts, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.I1c_lims_Contracts.Where(filter);
            return AppContext.I1c_lims_Contracts;
        }
    }
}
