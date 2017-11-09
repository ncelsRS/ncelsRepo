using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class OBK_Procunts_Series
    {
        #region 
        public Guid? ExpId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductSeriesId { get; set; }
        public string ExpResult { get; set; }
        public string ExpResultTitle { get; set; }
        public string ExpStartDate { get; set; }
        public string ExpEndDate { get; set; }
        public string ExpReasonNameRu { get; set; }
        public string ExpReasonNameKz { get; set; }
        public string ExpProductNameRu { get; set; }
        public string ExpProductNameKz { get; set; }
        public string ExpNomenclatureRu { get; set; }
        public string ExpNomenclatureKz { get; set; }
        public string ExpAddInfoRu { get; set; }
        public string ExpAddInfoKz { get; set; }
        public string ExpConclusionNumber { get; set; }
        public string ExpBlankNumber { get; set; }
        public bool ExpApplication { get; set; }
        public string ExpApplicationNumber { get; set; }
        public string ExpExecutorSign { get; set; }
        #endregion

        public string SeriesNameRu { get; set; }
        public string SeriesNameKz { get; set; }
        public string SeriesShortNameRu { get; set; }
        public string SeriesShortNameKz { get; set; }
    }
}
