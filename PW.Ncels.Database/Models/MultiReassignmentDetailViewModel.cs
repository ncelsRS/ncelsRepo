using System;

namespace PW.Ncels.Database.Models{

	public class MultiReassignmentDetailViewModel{
		public Guid Id { get; set; }

        public Nullable<Guid> DocumentId { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string OutgoingNumber { get; set; }

        public DateTime? OutgoingDate { get; set; }

        public string CorrespondentsValue { get; set; }
        
        public string ProxyOrganizationName { get; set; }

    }
}