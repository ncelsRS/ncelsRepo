using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugOrganizations : IDrugDeclarationCollection
    {
        public int Position { get; set; }

        public string ManufactureName { get; set; }

        public string DocDateStr
        {
            get { return DateHelper.GetDate(DocDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    DocDate = dateTemp.Value;
                }
            }
        }

        public string DocExpiryDateStr
        {
            get { return DateHelper.GetDate(DocExpiryDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    DocExpiryDate = dateTemp.Value;
                }
            }
        }
    }
}
