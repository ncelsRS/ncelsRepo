using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models.Expertise
{
    public class PrimaryEntity : AStageEntity
    {
        public string ExpeditedType { get; set; }
        public EXP_DrugPrimaryNTD EXP_DrugPrimaryNTD { get; set; }
        public List<EXP_DrugPrimaryKind> ExpDrugPrimaryKinds { get; set; }
    }
}
