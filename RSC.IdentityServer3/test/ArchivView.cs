namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArchivView")]
    public partial class ArchivView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public DateTime? DocumentDate { get; set; }

        [StringLength(4000)]
        public string DisplayName { get; set; }

        [StringLength(512)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentType { get; set; }

        [StringLength(4000)]
        public string NomenclatureDictionaryValue { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string Code { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string DepartmentsValue { get; set; }

        [StringLength(4000)]
        public string Summary { get; set; }

        [StringLength(900)]
        public string Deed { get; set; }

        [StringLength(900)]
        public string Book { get; set; }

        [StringLength(900)]
        public string Akt { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OrganizationId { get; set; }
    }
}
