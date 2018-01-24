namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OBKContractProductsView")]
    public partial class OBKContractProductsView
    {
        public string NameRu { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OBK_RS_ProductsId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProdSeriesId { get; set; }

        public string ProdSeries { get; set; }

        public string ProdSeriesEndDate { get; set; }

        public string ProdSeriesParty { get; set; }

        public string ProdCountryNameRu { get; set; }

        public string ProdProducerNameRu { get; set; }

        [StringLength(250)]
        public string ProdShortName { get; set; }

        public Guid? ContractId { get; set; }
    }
}
