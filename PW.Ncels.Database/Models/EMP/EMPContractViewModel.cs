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
        //public Guid? HolderType { get; set; }
        public string MedicalDeviceName { get; set; }
        public Declarations Manufactur { get; set; }
        public Declarations Declarant { get; set; }
        public Declarations Payer { get; set; }
        public List<ServicePrices> ServicePrice { get; set; }
    }
}
