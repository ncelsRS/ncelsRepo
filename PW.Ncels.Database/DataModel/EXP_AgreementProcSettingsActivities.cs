namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_AgreementProcSettingsActivities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_AgreementProcSettingsActivities()
        {
            EXP_AgreementProcSettingsTasks = new HashSet<EXP_AgreementProcSettingsTasks>();
        }

        public Guid Id { get; set; }

        public Guid SettingId { get; set; }

        public Guid ActivityTypeId { get; set; }

        public virtual Dictionary ActivityType { get; set; }

        public virtual EXP_AgreementProcSettings EXP_AgreementProcSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettingsTasks> EXP_AgreementProcSettingsTasks { get; set; }
    }
}
