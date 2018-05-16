using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Models
{
	public class ProjectRequestsTreeItemModel
    {
		public ProjectRequestsTreeItemModel() {
			items = new List<ProjectRequestsTreeItemModel>();
		}
		public string id { get; set; }
		//public Guid DataId { get; set; }
		public int Type { get; set; }
        public int ListType { get; set; }
        public string Name { get; set; }
		public bool hasChildren { get; set; }
		public int Year { get; set; }
		
		public bool expanded { get; set; }
		public Guid? ParentId { get; set; }

		public List<ProjectRequestsTreeItemModel> items { get; set; }

	}
}