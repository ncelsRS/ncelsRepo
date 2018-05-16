using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.Material
{
    public class CreateMaterialViewModel
    {
        public Guid? Id { get; set; }
        public Guid? DrugDeclarationId { get; set; }

        public Guid TypeIdDefaultValue { get; set; }

        public string DrugFormName { get; set; }
        public int? DrugFormId{ get; set; }

        public bool IsNpp{ get; set; }
    }
}
