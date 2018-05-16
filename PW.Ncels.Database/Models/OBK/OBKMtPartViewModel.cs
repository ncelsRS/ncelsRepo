using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKMtPartViewModel
    {
        public int Id { get; set; }
        public int RegisterId { get; set; }
        public string Name { get; set; }
        public string NameKz { get; set; }

        public int? PartNumber { get; set; }
        public string Model { get; set; }
        public string Specification { get; set; }
        public string SpecificationKz { get; set; }

        public string ProducerName { get; set; }
        public string CountryName { get; set; }
        public string ProducerNameKz { get; set; }
        public string CountryNameKz { get; set; }
    }
}
