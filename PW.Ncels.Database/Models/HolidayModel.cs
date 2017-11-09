using System;
using System.Collections.Generic;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models {
	public class HolidayModel {
		public long Id { get; set; }
		public List<Item> EmployeeId { get; set; }
		public string EmployeeValue {
			get { return DictionaryHelper.GetItemsName(EmployeeId); }
		}
		public List<Item> HolidayTypeDictionaryId { get; set; }
		public string HolidayTypeDictionaryValue {
			get { return DictionaryHelper.GetItemsName(HolidayTypeDictionaryId); }
		}
		public DateTime? PeriodStart { get; set; }
		public DateTime? PeriodEnd { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }
		public List<Item> DocumentId { get; set; }
		public string Note { get; set; }
		public string DocumentValue {
			get { return DictionaryHelper.GetItemsName(DocumentId); }
		}
		public int Count { get; set; }
	}
}