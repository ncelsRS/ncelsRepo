using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class SendAgreementViewModel
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string DocumentType { get; set; }

        public string TaskType { get; set; }

    }

    public class AgreementListViewModel
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string ActivityTypeCode { get; set; }

        public string DocumentTypeCode { get; set; }

        public string TaskTypeCode { get; set; }
    }

}
