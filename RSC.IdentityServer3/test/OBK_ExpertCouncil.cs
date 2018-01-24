namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ExpertCouncil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ExpertCouncil()
        {
            OBK_AssessmentDeclaration__OBK_ExpertCouncil = new HashSet<OBK_AssessmentDeclaration__OBK_ExpertCouncil>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public DateTime? ActualDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclaration__OBK_ExpertCouncil> OBK_AssessmentDeclaration__OBK_ExpertCouncil { get; set; }
    }
}
