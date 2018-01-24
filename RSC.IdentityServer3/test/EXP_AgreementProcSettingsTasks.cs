namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_AgreementProcSettingsTasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_AgreementProcSettingsTasks()
        {
            EXP_AgreementProcSettingsTasks1 = new HashSet<EXP_AgreementProcSettingsTasks>();
        }

        public Guid Id { get; set; }

        public Guid ActivityId { get; set; }

        public Guid TaskTypeId { get; set; }

        public Guid? ParentTaskId { get; set; }

        public Guid? ExecutorId { get; set; }

        public int OrderNum { get; set; }

        public virtual Dictionary TaskType { get; set; }

        public virtual Employee Executor { get; set; }

        public virtual EXP_AgreementProcSettingsActivities EXP_AgreementProcSettingsActivities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettingsTasks> EXP_AgreementProcSettingsTasks1 { get; set; }

        public virtual EXP_AgreementProcSettingsTasks EXP_AgreementProcSettingsTasks2 { get; set; }
    }
}
