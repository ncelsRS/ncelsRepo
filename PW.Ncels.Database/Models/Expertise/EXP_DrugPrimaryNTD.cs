using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugPrimaryNTD : IDrugDeclarationCollection
    {
        public string DateRegStr
        {
            get { return DateHelper.GetDate(DateReg); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    DateReg = dateTemp.Value;
                }
            }
        }

        public string DateConfirmStr
        {
            get { return DateHelper.GetDate(DateConfirm); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    DateConfirm = dateTemp.Value;
                }
            }
        }
    }
}




