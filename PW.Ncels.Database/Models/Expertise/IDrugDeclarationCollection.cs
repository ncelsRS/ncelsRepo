using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models.Expertise
{
    public interface IDrugDeclarationCollection
    {
        long Id { get; set; }
        System.Guid DrugDeclarationId { get; set; }

        EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }

    public interface IEXP_DrugDosageCollection
    {
        long Id { get; set; }
       long DrugDosageId { get; set; }

        EXP_DrugDosage EXP_DrugDosage { get; set; }
    }

}
