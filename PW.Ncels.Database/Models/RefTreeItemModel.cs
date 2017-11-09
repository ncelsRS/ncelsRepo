using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Models{

	public class RefTreeItemModel{

		public RefTreeItemModel() {
			items = new List<RefTreeItemModel>();
		}
		public int id { get; set; }
        public string Code { get; set; }
        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public bool hasChildren { get; set; }
		
		public bool expanded { get; set; }
		public int? ParentId { get; set; }

		public List<RefTreeItemModel> items { get; set; }

	}
}