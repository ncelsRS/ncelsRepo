using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Helpers
{
    //НДС
    public class TaxHelper
    {
        private static ncelsEntities db = UserHelper.GetCn();

        public static double GetNdsRef()
        {
            var result = db.OBK_Ref_ValueAddedTax.FirstOrDefault(e => e.Year == DateTime.Now.Year);
            if (result == null)
                return 0;
            return result.Value/100;
        }
        
        public static double GetCalculationTax(double price)
        {
            return price * GetNdsRef() + price;
        }

        public static decimal GetTaxWithPrice(decimal price)
        {
            return price * (decimal)GetNdsRef();
        }
    }
}
