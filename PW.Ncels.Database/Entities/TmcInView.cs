using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    [MetadataType(typeof(TmcInViewMetadata))]
    public partial class TmcInView : ITmcRequest
    {
        public Guid TmcInId { get; set; }

        protected class TmcInViewMetadata
        {
            public System.Guid Id { get; set; }
            public System.DateTime CreatedDate { get; set; }
            public System.Guid CreatedEmployeeId { get; set; }
            public int StateType { get; set; }
            public string StateTypeValue { get; set; }

            [Required]
            public Nullable<System.Guid> OwnerEmployeeId { get; set; }
            public string OwnerEmployeeValue { get; set; }
            public string Provider { get; set; }
            public string ContractNumber { get; set; }
            public Nullable<System.DateTime> ContractDate { get; set; }
            public Nullable<System.DateTime> LastDeliveryDate { get; set; }
            public bool IsFullDelivery { get; set; }
            public string IsFullDeliveryValue { get; set; }
            public string PowerOfAttorney { get; set; }
            public string ProviderBin { get; set; }
            public string ExecutorEmployeeValue { get; set; }
            public string AgreementEmployeeValue { get; set; }
            public Nullable<System.Guid> ExecutorEmployeeId { get; set; }
            public Nullable<System.Guid> AgreementEmployeeId { get; set; }
            public bool IsScan { get; set; }
            public string IsScanValue { get; set; }
            public Nullable<int> Func1 { get; set; }
        }
    }
}
