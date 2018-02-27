using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Notifications;

namespace PW.Ncels.Database.Repository.OBK
{
    public class OBKCertificateRepository : ARepository
    {
        public OBKCertificateRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public OBKCertificateRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public OBKCertificateRepository(ncelsEntities context) : base(context) { }



        public void UpdateCertificates()
        {
            var list = AppContext.OBK_CertificateReference.Include(o => o.OBK_CertificateValidityType).ToList();
            foreach (var obj in list)
            {
                if (obj.EndDate < DateTime.Now && !"Recalled".Equals(obj.OBK_CertificateValidityType.Code))
                {
                    OBK_CertificateReference d = AppContext.OBK_CertificateReference.First(o => o.Id == obj.Id);
                    d.OBK_CertificateValidityType = AppContext.OBK_CertificateValidityType.First(o => o.Code == "Passive");
                    AppContext.SaveChanges();
                }

            }
        }

    }
}
