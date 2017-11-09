using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Repository;

namespace PW.Ncels.Database.DataModel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OBK_AssessmentDeclaration : IEntity
    {

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

        public Guid? CountryId { get; set; }

        public Guid? CurrencyId { get; set; }

        #region contract

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        #endregion

        #region declatant

        public string NameKz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string AddressLegal { get; set; }
        public string AddressFact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankSwift { get; set; }
        public string BankBik { get; set; }
        public string ChiefLastName { get; set; }
        public string ChiefFirstName { get; set; }
        public string ChiefMiddleName { get; set; }
        public string ChiefPosition { get; set; }
        public string RegCertificateNumber { get; set; }
        public Guid? OrganizationFormId { get; set; }
        public string BankName { get; set; }
        public string BankIik { get; set; }

        #endregion

        #region expDocumentResult

        public bool ExpDocumentResult { get; set; }

        #endregion

        public Employee Applicant { get; set; }
        public OBK_Contract ObkContracts { get; set; }
        public List<OBK_RS_Products> ObkRsProductses { get; set; }
        public List<OBK_Procunts_Series> ObkProcuntsSeries { get; set; }
    }
}
