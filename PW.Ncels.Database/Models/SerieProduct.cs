using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class SerieProduct
    {
        public int ProductId { get; set; }
        public int MeasureId { get; set; }
        public int serie { get; set; }
        public int serieParty { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int quantity { get; set; }
    }
}