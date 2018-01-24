namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_countries
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_countries()
        {
            EXP_DrugExportTrade = new HashSet<EXP_DrugExportTrade>();
            EXP_DrugOtherCountry = new HashSet<EXP_DrugOtherCountry>();
            EXP_DrugSubstance = new HashSet<EXP_DrugSubstance>();
            EXP_DrugSubstanceManufacture = new HashSet<EXP_DrugSubstanceManufacture>();
            sr_register_names = new HashSet<sr_register_names>();
            sr_register_producers = new HashSet<sr_register_producers>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(254)]
        public string full_name { get; set; }

        [StringLength(510)]
        public string name_kz { get; set; }

        [StringLength(508)]
        public string full_name_kz { get; set; }

        [StringLength(100)]
        public string name_en { get; set; }

        [StringLength(3)]
        public string short_name { get; set; }

        public bool cis_sign { get; set; }

        public bool baltic_sign { get; set; }

        public bool foreign_sign { get; set; }

        public bool europe_sign { get; set; }

        public bool america_sign { get; set; }

        public bool kz_sign { get; set; }

        public bool block_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugExportTrade> EXP_DrugExportTrade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugOtherCountry> EXP_DrugOtherCountry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugSubstance> EXP_DrugSubstance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugSubstanceManufacture> EXP_DrugSubstanceManufacture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_names> sr_register_names { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_producers> sr_register_producers { get; set; }
    }
}
