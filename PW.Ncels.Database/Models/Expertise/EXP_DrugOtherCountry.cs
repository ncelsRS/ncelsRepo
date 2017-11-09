using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugOtherCountry : IDrugDeclarationCollection
    {
        public string IssueDateStr
        {
            get { return DateHelper.GetDate(IssueDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    IssueDate = dateTemp.Value;
                }
            }
        }

        public string ExpireDateStr
        {
            get { return DateHelper.GetDate(ExpireDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    ExpireDate = dateTemp.Value;
                }
            }
        }
    }
}



