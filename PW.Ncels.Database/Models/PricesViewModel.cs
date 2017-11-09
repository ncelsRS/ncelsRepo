using System;

namespace PW.Ncels.Database.Models {
	public class PricesViewModel {
		public Nullable<System.Guid> Id { get; set; }
		public int Type { get; set; }
		public string Name { get; set; }
		public System.Guid PriceProjectId { get; set; }
		public Nullable<System.Guid> CountryId { get; set; }
		public decimal ManufacturerPrice { get; set; }
		public Nullable<System.Guid> ManufacturerPriceCurrencyDicId { get; set; }
		public string ManufacturerPriceNote { get; set; }
		public decimal LimitPrice { get; set; }
		public Nullable<System.Guid> LimitPriceCurrencyDicId { get; set; }
		public string LimitPriceNote { get; set; }
		public decimal AvgOptPrice { get; set; }
		public Nullable<System.Guid> AvgOptPriceCurrencyDicId { get; set; }
		public string AvgOptPriceNote { get; set; }
		public decimal AvgRozPrice { get; set; }
		public Nullable<System.Guid> AvgRozPriceCurrencyDicId { get; set; }
		public string AvgRozPriceNote { get; set; }
		public decimal CipPrice { get; set; }
		public Nullable<System.Guid> CipPriceCurrencyDicId { get; set; }
		public Nullable<System.Guid> RefPriceTypeDicId { get; set; }
		public decimal RefPrice { get; set; }
		public Nullable<System.Guid> RefPriceCurrencyDicId { get; set; }
		public decimal OwnerPrice { get; set; }
		public Nullable<System.Guid> OwnerPriceCurrencyDicId { get; set; }
		public decimal BritishPrice { get; set; }
		public string CountryName { get; set; }
		public string ManufacturerPriceCurrencyName { get; set; }
		public string LimitPriceCurrencyName { get; set; }
		public string AvgOptPriceCurrencyName { get; set; }
		public string AvgRozPriceCurrencyName { get; set; }
		public string CipPriceCurrencyName { get; set; }
		public string RefPriceCurrencyName { get; set; }
		public string RefPriceTypeName { get; set; }
		public string OwnerPriceCurrencyName { get; set; }
		public decimal UnitPrice { get; set; }
		public Nullable<System.Guid> UnitPriceCurrencyDicId { get; set; }
		public string UnitPriceCurrencyName { get; set; }
		public Nullable<System.DateTime> CreatedDate { get; set; }
		public decimal BritishCost { get; set; }
		public Nullable<int> MtPartsId { get; set; }
		public string PartsName { get; set; }
	}
}