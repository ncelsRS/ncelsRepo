using System;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKContractMtPartViewModel
    {
        public Guid? Id { get; set; }
        public int? PartNumber {get;set;}
        public string Model { get; set; }
        public string Specification { get; set; }
        public string SpecificationKz { get; set; }
        public string Name { get; set; }
        public string NameKz { get; set; }
        public string ProducerName { get; set; }
        public string CountryName { get; set; }
        public string ProducerNameKz { get; set; }
        public string CountryNameKz { get; set; }
    }
}
