using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.OBK.ExpertCouncil
{
    public class ECDeclarationListMV
    {
        public Guid? Id { get; set; }
        public Guid? StageId { get; set; }
        public int? ExpertCouncilId { get; set; }
        public string Number { get; set; }
        public DateTime? FirstSendDate { get; set; }
        public string DeclarantName { get; set; }
        public string CountryName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? Result { get; set; }
        public string ResultName { get; set; }
        public string Comment { get; set; }
    }
}