using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;
using PW.Ncels.Database.Entities;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Recources;
using PW.Ncels.Database.Repository;

namespace PW.Ncels.Database.DataModel
{
    [Serializable]
    [XmlRoot()]
    /// <summary>
    /// Заявление на ЛС
    /// </summary>
    public partial class EXP_DrugDeclaration : IEntity
    {
        [OnSerializing]
        internal void OnSerializedMethod(StreamingContext context)
        {
            EXP_DirectionToPays = null;
            EXP_DrugDeclarationCom = null;
            EXP_DrugDeclarationFieldHistory = null;
            EXP_DrugDeclarationHistory = null;
            EXP_DirectionToPays = null;
            foreach (var e in EXP_DrugOtherCountry)
            {
                e.EXP_DrugDeclaration = null;
            }
            foreach (var e in EXP_DrugExportTrade)
            {
                e.EXP_DrugDeclaration = null;
            }
            foreach (var e in EXP_DrugPatent)
            {
                e.EXP_DrugDeclaration = null;
            }
            foreach (var e in EXP_DrugOrganizations)
            {
                e.EXP_DrugDeclaration = null;
            }
          /*  foreach (var e in EXP_DrugPrice)
            {
                e.EXP_DrugDeclaration = null;
            }*/
            foreach (var e in EXP_DrugProtectionDoc)
            {
                e.EXP_DrugDeclaration = null;
            }
         
            foreach (var e in EXP_DrugType)
            {
                e.EXP_DrugDeclaration = null;
            }
            foreach (var e in EXP_DrugUseMethod)
            {
                e.EXP_DrugDeclaration = null;
            }
            /*      foreach (var e in EXP_DrugWrapping)
                  {
                      e.EXP_DrugDeclaration = null;
                  }*/

        }
        public string ObjectId
        {
            get
            {
                if (Id == Guid.Empty)
                {
                    return null;
                }
                return Id.ToString();
            }
        }

        public string EditorId { get; set; }

        [Display(Name = "AccelerationDate", ResourceType = typeof(ResourceSetting))]
        public string AccelerationDateStr
        {
            get { return DateHelper.GetDate(AccelerationDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    AccelerationDate = dateTemp.Value;
                }
            }
        }
        /// <summary>
        /// Срок действия
        /// </summary>
        public string GmpExpiryDateStr
        {
            get { return DateHelper.GetDate(GmpExpiryDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    GmpExpiryDate = dateTemp.Value;
                }
            }
        }
        
        public string FirstSendDateStr
        {
            get { return DateHelper.GetDate(FirstSendDate); }
            set
            {
                var dateTemp = DateHelper.GetDate(value);
                if (dateTemp != null)
                {
                    FirstSendDate = dateTemp.Value;
                }
            }
        }

        public List<EXP_DIC_PrimaryOTD> ExpDicPrimaryOtds { get; set; }
        public List<EXP_DrugExportTrade> ExpDrugExportTrades { get; set; }
        public List<EXP_DrugPatent> ExpDrugPatents { get; set; }
        public List<EXP_DrugType> ExpDrugTypes { get; set; }
        //        public List<EXP_DrugWrapping> ExpDrugWrappings { get; set; }
        //        public List<EXP_DrugSubstance> ExpDrugSubstances { get; set; }
        public List<EXP_DrugOtherCountry> ExpDrugOtherCountries { get; set; }
        public List<EXP_DrugProtectionDoc> ExpDrugProtectionDocs { get; set; }
        public List<EXP_DrugOrganizations> ExpDrugOrganizationses { get; set; }
        //        public List<EXP_DrugPrice> ExpDrugPrices { get; set; }

        public List<EXP_DrugDosage> ExpDrugDosages { get; set; }
        public List<EXP_DrugChangeType>  ExpDrugChangeTypes{ get; set; }
        public List<sr_atc_codes> AtcCodeses { get; set; }

        public List<string> MethodUseIds { get; set; }

        public MultiSelectList MethodUseList { get; set; }
        
        public bool IsSelect { get; set; }

        public bool IsExist { get; set; }
        public Employee Applicant { get; set; }

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

        public bool IsShowChange { get; set; }
        public bool IsShowRemark { get; set; }
        public bool IsShowConclision { get; set; }

        public List<EXP_ExpertiseStageRemark> ExpExpertiseStageRemarks { get; set; }
        public List<EXP_DrugCorespondence> Letters { get; set; }

        public List<ConclusionSafetyReport> ConclusionSafetyReports { get; set; }
    }

}
