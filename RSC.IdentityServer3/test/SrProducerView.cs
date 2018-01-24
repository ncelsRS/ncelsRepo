namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SrProducerView")]
    public partial class SrProducerView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(1000)]
        public string Name { get; set; }

        [StringLength(500)]
        public string NameEng { get; set; }

        [StringLength(1000)]
        public string NameKz { get; set; }

        public long? FormTypeId { get; set; }

        [StringLength(255)]
        public string FormTypeFullName { get; set; }

        [StringLength(510)]
        public string FormTypeFullNameKz { get; set; }

        [StringLength(255)]
        public string FormTypeName { get; set; }

        [StringLength(510)]
        public string FormTypeNameKz { get; set; }
    }
}
