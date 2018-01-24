namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractRegisterView
    {
        [Key]
        [Column(Order = 0)]
        public Guid ContractId { get; set; }

        public Guid? ContractParentId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ContractTypeRu { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ContractTypeKz { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(30)]
        public string ContractNumber { get; set; }

        [StringLength(61)]
        public string ContractDisplayNumber { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime ContractCreatedDate { get; set; }

        public DateTime? ContractSendDate { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public Guid? ContractSigner { get; set; }

        [StringLength(255)]
        public string DeclarantNameRu { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid ExecutorId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExecutorType { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContractStatusId { get; set; }

        [StringLength(2000)]
        public string ContractStatusNameRu { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StageStatusId { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(2000)]
        public string StageStatusNameRu { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string StageStatusCode { get; set; }

        [Key]
        [Column(Order = 11)]
        public Guid ContractStageId { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContractStageStageId { get; set; }

        public int? StageCOZResult { get; set; }

        public int? StageUOBKResult { get; set; }

        [StringLength(4000)]
        public string StageUOBKExecutor { get; set; }

        public int? StageDEFResult { get; set; }

        [StringLength(4000)]
        public string StageDEFExecutor { get; set; }
    }
}
