namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_StageExpDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_StageExpDocument()
        {
            OBK_ZBKCopy = new HashSet<OBK_ZBKCopy>();
        }

        public Guid Id { get; set; }

        public int? ProductId { get; set; }

        public int? ProductSeriesId { get; set; }

        public bool ExpResult { get; set; }

        public DateTime? ExpStartDate { get; set; }

        public DateTime? ExpEndDate { get; set; }

        public string ExpReasonNameRu { get; set; }

        public string ExpReasonNameKz { get; set; }

        public string ExpProductNameRu { get; set; }

        public string ExpProductNameKz { get; set; }

        public string ExpNomenclatureRu { get; set; }

        public string ExpNomenclatureKz { get; set; }

        public string ExpAddInfoRu { get; set; }

        public string ExpAddInfoKz { get; set; }

        [StringLength(50)]
        public string ExpConclusionNumber { get; set; }

        [StringLength(50)]
        public string ExpBlankNumber { get; set; }

        public bool ExpApplication { get; set; }

        [StringLength(50)]
        public string ExpApplicationNumber { get; set; }

        public string ExpExecutorSign { get; set; }

        public Guid? ExecutorId { get; set; }

        public Guid? AssessmentDeclarationId { get; set; }

        public int? RefReasonId { get; set; }

        public DateTime? RefuseDate { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Procunts_Series OBK_Procunts_Series { get; set; }

        public virtual OBK_Ref_Reason OBK_Ref_Reason { get; set; }

        public virtual OBK_RS_Products OBK_RS_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopy> OBK_ZBKCopy { get; set; }
    }
}
