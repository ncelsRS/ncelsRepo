using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Models.Expertise;


namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugDosage : IDrugDeclarationCollection
    {
        public int Position { get; set; }
        public List<EXP_DrugWrapping> ExpDrugWrappings { get; set; }
        public List<EXP_DrugPrice> ExpDrugPrices { get; set; }
        public List<EXP_DrugSubstance> ExpDrugSubstances { get; set; }
        public List<EXP_DrugAppDosageResult> ExpDrugAppDosageResults { get; set; }
        public List<EXP_DrugAppDosageRemark> ExpDrugAppDosageRemarks { get; set; }

        /// <summary>
        /// № регистрации
        /// </summary>
        public string ReestrNumber { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public string ReestrRegDate { get; set; }

        /// <summary>
        /// Срок действия
        /// </summary>
        public string ReestrExpirationDate { get; set; }

        /// <summary>
        /// № НД
        /// </summary>
        public string NumberNd { get; set; }

    }
}