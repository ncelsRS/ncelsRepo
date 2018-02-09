using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class SerieProduct
    {
        public int ProductId { get; set; }
        public Int64 MeasureId { get; set; }
        public string serie { get; set; }
        public string serieParty { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int quantity { get; set; }
    }
}