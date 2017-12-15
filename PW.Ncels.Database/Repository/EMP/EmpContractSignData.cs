using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Repository.EMP
{
    public class EmpContractSignData
    {
        public EmpContractDeclarantSignData Manufacturer { get; set; }
        public EmpContractDeclarantSignData Declarant { get; set; }
        public EmpContractDeclarantSignData Payer { get; set; }
        public string ChoosPayer { get; set; }
        public string MedicalDeviceName { get; set; }
        public IEnumerable<EmpContractWorkCostsSignData> WorkCosts { get; set; }
    }
}