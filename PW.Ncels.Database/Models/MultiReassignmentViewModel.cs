using System;
using System.Collections.Generic;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models{

	public class MultiReassignmentViewModel{
		public Guid Id { get; set; }

	    public string TaskIds { get; set; }

        public List<Task> Tasks { get; set; }

        public List<Item> ExecutorId { get; set; }

		public string Text { get; set; }
	}
}