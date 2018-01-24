namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Products_SeriesCom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Products_SeriesCom()
        {
            OBK_Products_SeriesComRecord = new HashSet<OBK_Products_SeriesComRecord>();
        }

        public Guid Id { get; set; }

        public int ProductSerieId { get; set; }

        public bool IsError { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public virtual OBK_Procunts_Series OBK_Procunts_Series { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Products_SeriesComRecord> OBK_Products_SeriesComRecord { get; set; }
    }
}
