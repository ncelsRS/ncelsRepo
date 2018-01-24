namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_RegistrationExpSteps
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_RegistrationExpSteps()
        {
            Executors = new HashSet<Employee>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Duration { get; set; }

        public Guid RegistrationId { get; set; }

        public int Priority { get; set; }

        public int? RefId { get; set; }

        public Guid? SupervisingEmployeeId { get; set; }

        public virtual Employee SupervisingEmployee { get; set; }

        public virtual EXP_DIC_Stage EXP_DIC_Stage { get; set; }

        public virtual EXP_RegistrationTypes EXP_RegistrationTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Executors { get; set; }
    }
}
