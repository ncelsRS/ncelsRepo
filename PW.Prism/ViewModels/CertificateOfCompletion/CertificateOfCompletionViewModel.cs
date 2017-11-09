using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.CertificateOfCompletion
{
    public class CertificateOfCompletionViewModel
    {
        public Guid Id { get; set; }

        public int StageId { get; set; }

        public Guid DrugDeclarationId { get; set; }
    }
}