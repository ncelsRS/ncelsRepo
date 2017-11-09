using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.CertificateOfCompletion
{
    public class CertificateOfCompletionRepository: ARepositoryGeneric<EXP_CertificateOfCompletion>
    {
        public CertificateOfCompletionRepository(bool isProxy = true):base(isProxy)
        { }

        public void CreateCertificateOfCompletion(EXP_CertificateOfCompletion model)
        {
            AppContext.EXP_CertificateOfCompletion.Add(model);
            AppContext.SaveChanges();
        }

        public IQueryable<EXP_CertificateOfCompletionView> GetCocViewAsQuarable(Expression<Func<EXP_CertificateOfCompletionView, bool>> filter = null)
        {
            IQueryable<EXP_CertificateOfCompletionView> query = AppContext.EXP_CertificateOfCompletionView;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }
    }
}
