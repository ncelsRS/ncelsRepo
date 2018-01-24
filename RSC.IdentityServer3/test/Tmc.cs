namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tmc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tmc()
        {
            LimsTmcTemps = new HashSet<LimsTmcTemp>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public Guid CreatedEmployeeId { get; set; }

        public int StateType { get; set; }

        public Guid TmcInId { get; set; }

        [StringLength(450)]
        public string Number { get; set; }

        public string Name { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        [StringLength(450)]
        public string Manufacturer { get; set; }

        [StringLength(450)]
        public string Serial { get; set; }

        public decimal Count { get; set; }

        public Guid? MeasureTypeDicId { get; set; }

        public decimal CountFact { get; set; }

        public decimal CountConvert { get; set; }

        public Guid? MeasureTypeConvertDicId { get; set; }

        public decimal CountActual { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ManufactureDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        public Guid? PackageDicId { get; set; }

        public Guid? TmcTypeDicId { get; set; }

        public Guid? StorageDicId { get; set; }

        [StringLength(450)]
        public string Safe { get; set; }

        [StringLength(450)]
        public string Rack { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceivingDate { get; set; }

        public DateTime? WriteoffDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsTmcTemp> LimsTmcTemps { get; set; }
    }
}
