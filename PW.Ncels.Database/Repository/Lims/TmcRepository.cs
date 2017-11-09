using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Lims
{
    public class TmcRepository : ARepositoryGeneric<Tmc>
    {
        public IQueryable<I1c_lims_ContractProducts> GetI1cLimsContractProducts(
          Expression<Func<I1c_lims_ContractProducts, bool>> filter = null)
        {
            if (filter != null)
                return AppContext.I1c_lims_ContractProducts.Where(filter);
            return AppContext.I1c_lims_ContractProducts;
        }


        public void RemoveTmcByPoaId(Guid poaId)
        {
            var tmcs = AppContext.Tmcs.Where(t => t.TmcInId == poaId);
            foreach (var tmc in tmcs)
            {
                AppContext.Tmcs.Remove(tmc);
            }
            //AppContext.SaveChanges();
        }
    }
}
