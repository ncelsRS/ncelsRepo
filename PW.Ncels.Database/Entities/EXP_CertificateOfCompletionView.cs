namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_CertificateOfCompletionView
    {

        public string StatusStrLocal
        {
            get
            {
                if (TaskExecutorValue != null)
                {
                    return $"{StatusStr} ({TaskExecutorValue})";
                }
                return StatusStr;
            }
        }

        public partial class EXP_CertificateOfCompletionViewMetadata
        {
            public System.Guid Id { get; set; }
            public Nullable<int> DicStageId { get; set; }
            public Nullable<System.Guid> DrugDeclarationId { get; set; }
            public Nullable<decimal> TotalPrice { get; set; }
            public Nullable<System.Guid> StatusId { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }
            public Nullable<System.DateTime> ModifyDate { get; set; }
            public Nullable<System.DateTime> SendDate { get; set; }
            public Nullable<System.Guid> CreateEmployeeId { get; set; }
            public string TradeNameRu { get; set; }
            public string TradeNameKz { get; set; }
            public string TradeNameEn { get; set; }
            public string ManufacturerNameRu { get; set; }
            public string ManufacturerNameKz { get; set; }
            public string ManufacturerNameEn { get; set; }
            public string StatusStr { get; set; }
            public string StatusStrKz { get; set; }
            public string CreateEmployeeStr { get; set; }
            public string Stage { get; set; }
            public string StageKz { get; set; }
            public string CountryName { get; set; }
            public string CountryNameKz { get; set; }
            public string TaskExecutorValue { get; set; }
        }
    }
}