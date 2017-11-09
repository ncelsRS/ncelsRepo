using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugSubstance : IEXP_DrugDosageCollection
    {
        public int Position { get; set; }

        public string CategoryName { get; set; }
        public string CategoryPos { get; set; }

        public List<EXP_DrugSubstanceManufacture> ExpDrugSubstanceManufactures { get; set; }
    }
}
