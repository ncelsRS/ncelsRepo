using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Models
{
	public class UnitTreeItemModel
	{
		public UnitTreeItemModel() {
			items = new List<UnitTreeItemModel>();
		}
		public Guid id { get; set; }
		public Guid DataId { get; set; }
		public int Type { get; set; }
		public string Name { get; set; }
		public bool hasChildren { get; set; }
		public int Sex { get; set; }
		
		public bool expanded { get; set; }
		public Guid? ParentId { get; set; }

		public List<UnitTreeItemModel> items { get; set; }

	}
}