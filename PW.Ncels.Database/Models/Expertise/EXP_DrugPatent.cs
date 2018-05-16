//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugPatent : IDrugDeclarationCollection
    {
        public string PatentDateStr
        {
            get { return DateHelper.GetDate(PatentDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    PatentDate = dateTemp.Value;
                }
            }
        }

        public string PatentExpiryDateStr
        {
            get { return DateHelper.GetDate(PatentExpiryDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    PatentExpiryDate = dateTemp.Value;
                }
            }
        }
    }
}
