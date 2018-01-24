namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcInView")]
    public partial class TmcInView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreatedEmployeeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [StringLength(24)]
        public string StateTypeValue { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [StringLength(4000)]
        public string OwnerEmployeeValue { get; set; }

        [StringLength(450)]
        public string Provider { get; set; }

        [StringLength(450)]
        public string ProviderBin { get; set; }

        [StringLength(450)]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastDeliveryDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsFullDelivery { get; set; }

        [StringLength(3)]
        public string IsFullDeliveryValue { get; set; }

        [StringLength(450)]
        public string PowerOfAttorney { get; set; }

        [StringLength(4000)]
        public string ExecutorEmployeeValue { get; set; }

        [StringLength(4000)]
        public string AgreementEmployeeValue { get; set; }

        public Guid? ExecutorEmployeeId { get; set; }

        public Guid? AgreementEmployeeId { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool IsScan { get; set; }

        [StringLength(3)]
        public string IsScanValue { get; set; }

        public int? Func1 { get; set; }

        public Guid? AccountantEmployeeId { get; set; }

        [StringLength(4000)]
        public string AccountantEmployeeValue { get; set; }
    }
}
