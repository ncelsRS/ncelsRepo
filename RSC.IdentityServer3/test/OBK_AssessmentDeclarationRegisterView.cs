namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclarationRegisterView
    {
        [Key]
        [Column(Order = 0)]
        public Guid DeclarationId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime? FirstSendDate { get; set; }

        [StringLength(255)]
        public string DeclarantName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StageStatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid ExecutorId { get; set; }

        [Key]
        [Column(Order = 4)]
        public string RegType { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid StageId { get; set; }

        [StringLength(2000)]
        public string StatusName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string StageStatusCode { get; set; }

        [StringLength(50)]
        public string StageCode { get; set; }

        public Guid? CountryId { get; set; }

        [StringLength(4000)]
        public string CountryNameRu { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(30)]
        public string ContractNumber { get; set; }

        public int? ProductsCount { get; set; }

        public string Series { get; set; }
    }
}
