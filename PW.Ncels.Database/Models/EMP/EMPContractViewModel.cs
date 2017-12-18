using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.EMP
{
    public class EMPContractViewModel
    {
        public Guid Id { get; set; }
        public Guid? HolderType { get; set; }
        public Guid? ContractType { get; set; }
        public string MedicalDeviceName { get; set; }
        public string MedicalDeviceNameKz { get; set; }
        public bool DeclarantIsManufactur { get; set; }
        public string ChoosePayer { get; set; }
        public Declarants Manufactur { get; set; }
        public Declarants Declarant { get; set; }
        public Declarants Payer { get; set; }
        public List<ServicePrice> ServicePrices { get; set; }
        public string ContractScope { get; set; }
        public bool HasProxy { get; set; }
        public int DocumentType { get; set; }
    }
}
