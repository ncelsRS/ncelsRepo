namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_CertificateOfCompletion
    {
        public string Stage
        {
            get
            {
                if (EXP_DIC_Stage != null)
                {
                    return EXP_DIC_Stage.NameRu;
                }
                return string.Empty;
            }
        }

        public string StatusStr
        {
            get
            {
                if (Status != null)
                {
                    return Status.Name;
                }
                return string.Empty;
            }
        }

        public string CreateEmployeeStr
        {
            get
            {
                if (Status != null)
                {
                    return Status.Name;
                }
                return string.Empty;
            }
        }

        public string TradeName
        {
            get
            {
                if (EXP_DrugDeclaration != null)
                {
                    return EXP_DrugDeclaration.NameRu;
                }
                return string.Empty;
            }
        }

        public class EXP_CertificateOfCompletionMetadata
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

            public virtual Dictionary Status { get; set; }
            public virtual Employee CreateEmployee { get; set; }
            public virtual EXP_DIC_Stage EXP_DIC_Stage { get; set; }
            public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
        }
    }
}

