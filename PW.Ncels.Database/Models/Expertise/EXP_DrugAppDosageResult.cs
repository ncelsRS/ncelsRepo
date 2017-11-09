using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugAppDosageResult : IEXP_DrugDosageCollection
    {
        public string RemarkDateStr
        {
            get { return DateHelper.GetDate(RemarkDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    RemarkDate = dateTemp.Value;
                }
            }
        }

        public string FixedDateStr
        {
            get { return DateHelper.GetDate(FixedDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    FixedDate = dateTemp.Value;
                }
            }
        }
    }
}


