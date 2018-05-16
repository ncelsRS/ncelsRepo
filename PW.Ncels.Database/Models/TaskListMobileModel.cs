using System;

namespace PW.Ncels.Database.Models {
	public class TaskListMobileModel {
		public Guid TaskId { get; set; }

		public string Note { get; set; }
		public int TypeReport { get; set;}

		public int? PageCount { get; set; }
		public int? CopiesCount { get; set;}

	}
}