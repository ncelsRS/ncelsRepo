using System;

namespace PW.Prism.ViewModels.Commissions
{
    public class CommissionDrugDeclarationListItemModel
    {
        public int? Id { get; set; }
        public long DrugDosageId { get; set; }
        public string Number { get; set; }
        public string DeclarationNumber { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public int? ConclusionId { get; set; }
        public string ConclusionName { get; set; }
        public string ConclusionComment { get; set; }
        public string StageName { get; set; }
        public Guid DosageStageId { get; set; }
        public string ProducerCountryName { get; set; }
        public string ProducerNameRu { get; set; }
        public DateTime? ProducerDocDate { get; set; }
        public DateTime? ProducerDocExpiryDate { get; set; }
        public string DosageFormName { get; set; }
        public string DeclarationAtxName { get; set; }
        public string DeclarationMnnName { get; set; }
        public string DosageSaleTypeName { get; set; }
        public string NtdNameRu { get; set; }
        public bool IsRepeat { get; set; }
        
        public string ResultNameRu { get; set; }
        public DateTime? ResultDate { get; set; }
        public string ResultCreatorShortName { get; set; }
    }
}