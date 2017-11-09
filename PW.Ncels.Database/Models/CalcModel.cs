using System;
using PW.Ncels.Database.Enums;

namespace PW.Ncels.Database.Models {
	public class CalcModel {
		public Guid Id { get; set; }
		public decimal ManufacturerPrice { get; set; }
		public decimal CipPrice { get; set; }
		public decimal RefPrice { get; set; }
		public decimal OwnerPrice { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal BritishPrice { get; set; }
		public decimal BritishCost { get; set; }
		public decimal AvgObkCost { get; set; }
		public decimal AvgRznCost { get; set; }
		public decimal AvgOptCost { get; set; }
		public decimal ZakupCost { get; set; }
		public decimal LimitCost { get; set; }
		public decimal MinimalCost { get; set; }
		public decimal OriginalCost { get; set; }
		public decimal MarkupCost { get; set; }
		public decimal MarkupCostOpt { get; set; }
		public DateTime CreatedDate { get; set; }
        public DateTime CalcDateStart { get; set; }
        public DateTime CalcDateEnd { get; set; }
        public int PriceProjectType { get; set; }
    }
}