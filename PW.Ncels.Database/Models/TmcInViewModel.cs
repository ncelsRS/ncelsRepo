using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Recources;

namespace PW.Ncels.Database.Models
{
    public class TmcInViewModel
    {
        public System.Guid? Id { get; set; }
        public System.Guid? TmcInId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid? CreatedEmployeeId { get; set; }
        public int StateType { get; set; }
        public string StateTypeValue { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public Nullable<System.Guid> OwnerEmployeeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public Nullable<System.Guid> ExecutorEmployeeId { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public Nullable<System.Guid> AccountantEmployeeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public Nullable<System.Guid> AgreementEmployeeId { get; set; }

        public string OwnerEmployeeValue { get; set; }
        public string ExecutorEmployeeValue { get; set; }
        public string AgreementEmployeeValue { get; set; }
        public string AccountantEmployeeValue { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public string ProviderBin { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public string ContractNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public Nullable<System.DateTime> ContractDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Msg_Необходимо_заполнить")]
        public Nullable<System.DateTime> LastDeliveryDate { get; set; }

        public bool IsFullDelivery { get; set; }
        public string IsFullDeliveryValue { get; set; }
        public string PowerOfAttorney { get; set; }
        public int? Func1 { get; set; }

         public List<UploadInitialFile> AttachFiles { get; set; }
         public string Comment { get; set; }
    }
    
}