namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_RegistrationTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_RegistrationTypes()
        {
            EXP_RegistrationExpSteps = new HashSet<EXP_RegistrationExpSteps>();
            EXP_RegistrationTypes1 = new HashSet<EXP_RegistrationTypes>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public int? RefId { get; set; }

        public virtual EXP_DIC_Type EXP_DIC_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_RegistrationExpSteps> EXP_RegistrationExpSteps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_RegistrationTypes> EXP_RegistrationTypes1 { get; set; }

        public virtual EXP_RegistrationTypes EXP_RegistrationTypes2 { get; set; }
    }
}
