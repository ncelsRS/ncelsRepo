using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKContractProductViewModel
    {
        public int? ProductId { get; set; }
        public int? Id { get; set; }
        public int RegTypeId { get; set; }
        public int? DegreeRiskId { get; set; }
        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public string ProducerNameRu { get; set; }
        public string ProducerNameKz { get; set; }
        public string CountryNameRu { get; set; }
        public string CountryNameKz { get; set; }
        public string TnvedCode { get; set; }
        public string KpvedCode { get; set; }
        public string Price { get; set; }
        public Guid? Currency { get; set; }
        public string DrugFormBoxCount { get; set; }
        public string DrugFormFullName { get; set; }
        public string DrugFormFullNameKz { get; set; }

        public int RegisterId { get; set; }
        public string RegNumber { get; set; }
        public string RegNumberKz { get; set; }
        public DateTime RegDate { get; set; }
        public string NdName { get; set; }
        public string NdNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public List<OBKContractSeriesViewModel> Series { get; set; }
        public List<OBKContractMtPartViewModel> MtParts { get; set; }

        public Guid ServiceName { get; set; }
    }
}