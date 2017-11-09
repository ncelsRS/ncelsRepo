using System;

namespace PW.Ncels.Database.Models {
	public class TaskPathModel 
	{
		public Guid? ActivityId { get; set; }
		public string ExecutorId { get; set; }
		public string AuthorId { get; set; }
		public bool IsMainLine { get; set; }

		public Guid? ParentId { get; set; }
		public string Executor { get; set; }
		public int? Rank { get; set; }
	}
}