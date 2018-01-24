namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_AgreementProcSettings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_AgreementProcSettings()
        {
            EXP_AgreementProcSettingsActivities = new HashSet<EXP_AgreementProcSettingsActivities>();
        }

        public Guid Id { get; set; }

        public Guid AgreedDocTypeId { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettingsActivities> EXP_AgreementProcSettingsActivities { get; set; }
    }
}
