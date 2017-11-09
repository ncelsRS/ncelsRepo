using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBK_ProductInfo
    {
        public int ProductId;
        public string RegNumber;
        public string RegNumberKz;
        public string Name;
        public string NameKz;
        public int RegTypeId;
        public string RegTypeName;
        public DateTime RegDate;
        public DateTime? ExpireDate;
        public string ProducerName;
        public string ProducerNameKz;
        public string CountryName;
        public string CountryNameKz;
        public string TnvedCode;
        public string KpvedCode;
        public decimal? Price;
        public string Currency;
        public int? DegreeRiskId;
        public string NdName;
        public string NdNumber;
        public int RegisterId;
    }
}
