using System;

namespace PW.Ncels.Database.Repository.EMP
{
    public class EmpContractDeclarantSignData
    {
        public bool IsResident { get; set; }
        public string NameKz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string Bin { get; set; }
        public string BossLastName { get; set; }
        public string BossFirstName { get; set; }
        public string BossMiddleName { get; set; }
        public string BossPositionRu { get; set; }
        public string BossPositionKz { get; set; }
        public string AddressLegal { get; set; }
        public string AddressFact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string BankIik { get; set; }
        public string BankBik { get; set; }
        public string Iin { get; set; }
        public string BankAccount { get; set; }
    }
}