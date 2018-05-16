using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Models
{
	public class TaskAction
	{
		public Guid Id { get; set; }
		public Guid? ActionId { get; set; }
		public int? Type { get; set; }
		public int State { get; set; }

		public string DocumenetNumber { get; set; }
		public string DocumenetDate { get; set; }
		public DateTime? Date { get; set; }
		public List<Item> ExecutorId { get; set; }

		public DateTime? ExecutionDate { get; set; }
		public List<Item> ResponsibleId { get; set; }

		public string Text { get; set; }
		public Guid? DocumentId { get; set; }
	}
}