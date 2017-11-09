using System;
using System.Collections.Generic;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKContractSignData
    {
        public Guid ContractId { get; set; }
        public string SignerFullName { get; set; }
        public string SignerDocTypeNameRu { get; set; }
        public string SignerDocTypeNameKz { get; set; }
        public string SignerDocNumber { get; set; }
        public string DeclarantOrganizationFormNameRu { get; set; }
        public string DeclarantOrganizationFormNameKz { get; set; }
        public string DeclarantNameRu { get; set; }
        public string DeclarantNameKz { get; set; }
        public string DeclarantBossPositionRu { get; set; }
        public string DeclarantBossPositionKz { get; set; }
        public string DeclarantBossLastName { get; set; }
        public string DeclarantBossFirstName { get; set; }
        public string DeclarantBossMiddleName { get; set; }
        public string DeclarantBossDocTypeRu { get; set; }
        public string DeclarantBossDocTypeKz { get; set; }
        public string ExpertOrganizationNameRu { get; set; }
        public string ExpertOrganizationNameKz { get; set; }
        public string ExpertOrganizationAddressNameRu { get; set; }
        public string ExpertOrganizationAddressNameKz { get; set; }
        public string ExpertOrganizationKbe { get; set; }
        public string ExpertOrganizationBin { get; set; }
        public string ExpertOrganizationBankSwift { get; set; }
        public string ExpertOrganizationBankIik { get; set; }
        public string ExpertOrganizationBankNameRu { get; set; }
        public string ExpertOrganizationBankNameKz { get; set; }
        public string SignerPositionRu { get; set; }
        public string SignerPositionKz { get; set; }
        public string SignerShortName { get; set; }
        public string DeclarantCountryNameRu { get; set; }
        public string DeclarantCountryNameKz { get; set; }
        public string DeclarantAddressLegalRu { get; set; }
        public string DeclarantAddressLegalKz { get; set; }
        public string DeclarantBin { get; set; }
        public string DeclarantBankBik { get; set; }
        public string DeclarantBankIik { get; set; }
        public string DeclarantBankNameRu { get; set; }
        public string DeclarantBankNameKz { get; set; }
        public List<OBKContractProduct> Products { get; set; }
    }
}
