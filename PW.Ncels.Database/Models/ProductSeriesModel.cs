using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class ProductSeriesModel
    {
        public int SerieId { get; set; }
        public string Comment { get; set; }
        public bool Available { get; set; }
        public int? Quantity { get; set; }
        public long? SeriesMeasureId { get; set; }
    }
}
