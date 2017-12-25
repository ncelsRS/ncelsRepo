using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class ZBKViewModel
    {
        public Guid Id { get; set; }
        public Guid StageId { get; set; }
        public string Declarer { get; set; }
        public string OrganizationName { get; set; }
        public string ConclusionNumber { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public string DeclarationNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string DeclarationType { get; set; }
        public string ConclusionType { get; set; }
        public int? CopyQuantity { get; set; }
        public Guid? ContractId { get; set; }
        public string AttachPath { get; set; }
        public bool? ExpApplication { get; set; }
        public string StageStatusCode { get; set; }
        public string Notes { get; set; }
        public bool? PaymentInvoice { get; set; }
        public int? refStatus { get; set; }
        public bool? InOBK { get; set; }
        public bool? OBK_Completed { get; set; }
        public bool? OriginalsGiven { get; set; }
        public DateTime? ExtraditeDate { get; set; }
        public bool? zbkCopiesReady { get; set; }

        
    }
}
