namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugCorespondence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DrugCorespondence()
        {
            EXP_DrugCorespondenceRemark = new HashSet<EXP_DrugCorespondenceRemark>();
        }

        public Guid Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public Guid? AuthorId { get; set; }

        public Guid? SignatoryId { get; set; }

        public Guid? MatchingId { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateSend { get; set; }

        public DateTime? DateRead { get; set; }

        [StringLength(50)]
        public string NumberLetter { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        public string Note { get; set; }

        public int StageId { get; set; }

        public int TypeId { get; set; }

        public int KindId { get; set; }

        public Guid StatusId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsReadOnly { get; set; }

        public int SubjectId { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        public virtual EXP_DIC_CorespondenceKind EXP_DIC_CorespondenceKind { get; set; }

        public virtual EXP_DIC_CorespondenceSubject EXP_DIC_CorespondenceSubject { get; set; }

        public virtual EXP_DIC_CorespondenceType EXP_DIC_CorespondenceType { get; set; }

        public virtual EXP_DIC_Stage EXP_DIC_Stage { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondenceRemark> EXP_DrugCorespondenceRemark { get; set; }
    }
}
