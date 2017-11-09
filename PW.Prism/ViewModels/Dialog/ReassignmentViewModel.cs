using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PW.Ncels.Database.Models;

namespace PW.Prism.ViewModels.Dialog
{
    public class ReassignmentViewModel
    {
        public Guid Id { get; set; }

        public Guid? DocumentId { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public Item ExecutorId { get; set; }
        
        public string Text { get; set; }
        

        //public System.Guid ActivityId { get; set; }

        //public System.Guid TaskTypeId { get; set; }

        public string ActivityTypeCode { get; set; }

        public string DocumentTypeCode { get; set; }

        public string TaskTypeCode { get; set; }

    }
}