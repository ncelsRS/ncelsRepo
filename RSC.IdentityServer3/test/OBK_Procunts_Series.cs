namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Procunts_Series
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Procunts_Series()
        {
            OBK_Products_SeriesCom = new HashSet<OBK_Products_SeriesCom>();
            OBK_StageExpDocument = new HashSet<OBK_StageExpDocument>();
            OBK_TaskMaterial = new HashSet<OBK_TaskMaterial>();
        }

        public int Id { get; set; }

        public string Series { get; set; }

        public string SeriesStartdate { get; set; }

        public string SeriesEndDate { get; set; }

        public string SeriesParty { get; set; }

        public int OBK_RS_ProductsId { get; set; }

        public long? SeriesMeasureId { get; set; }

        public int? Quantity { get; set; }

        public bool? Available { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Products_SeriesCom> OBK_Products_SeriesCom { get; set; }

        public virtual OBK_RS_Products OBK_RS_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_StageExpDocument> OBK_StageExpDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskMaterial> OBK_TaskMaterial { get; set; }
    }
}
