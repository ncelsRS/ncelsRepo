using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugPrice : IEXP_DrugDosageCollection
    {
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string IntermediateText { get; set; }
    }
}
