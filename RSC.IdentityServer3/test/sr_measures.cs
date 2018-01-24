namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_measures
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_measures()
        {
            EXP_DrugDeclaration = new HashSet<EXP_DrugDeclaration>();
            EXP_DrugDeclaration1 = new HashSet<EXP_DrugDeclaration>();
            EXP_DrugDeclaration2 = new HashSet<EXP_DrugDeclaration>();
            EXP_DrugDeclaration3 = new HashSet<EXP_DrugDeclaration>();
            EXP_DrugDosage = new HashSet<EXP_DrugDosage>();
            EXP_DrugDosage1 = new HashSet<EXP_DrugDosage>();
            EXP_DrugDosage2 = new HashSet<EXP_DrugDosage>();
            EXP_DrugDosage3 = new HashSet<EXP_DrugDosage>();
            EXP_DrugSubstance = new HashSet<EXP_DrugSubstance>();
            EXP_DrugWrapping = new HashSet<EXP_DrugWrapping>();
            EXP_DrugWrapping1 = new HashSet<EXP_DrugWrapping>();
            OBK_ActReception = new HashSet<OBK_ActReception>();
            OBK_Procunts_Series = new HashSet<OBK_Procunts_Series>();
            sr_register_drugs = new HashSet<sr_register_drugs>();
            sr_register = new HashSet<sr_register>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(510)]
        public string name_kz { get; set; }

        [StringLength(250)]
        public string short_name { get; set; }

        [StringLength(500)]
        public string short_name_kz { get; set; }

        public bool block_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDosage> EXP_DrugDosage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDosage> EXP_DrugDosage1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDosage> EXP_DrugDosage2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDosage> EXP_DrugDosage3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugSubstance> EXP_DrugSubstance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugWrapping> EXP_DrugWrapping { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugWrapping> EXP_DrugWrapping1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Procunts_Series> OBK_Procunts_Series { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_drugs> sr_register_drugs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register> sr_register { get; set; }
    }
}
