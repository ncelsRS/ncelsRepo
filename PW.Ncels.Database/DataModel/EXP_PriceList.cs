namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_PriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_PriceList()
        {
            EXP_DirectionToPays_PriceList = new HashSet<EXP_DirectionToPays_PriceList>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [StringLength(450)]
        public string NameRu { get; set; }

        [StringLength(450)]
        public string NameKz { get; set; }

        [StringLength(450)]
        public string NameEn { get; set; }

        public decimal? PriceRegisterForeign { get; set; }

        public decimal? PriceRegisterForeignNds { get; set; }

        public decimal? PriceReRegisterForeign { get; set; }

        public decimal? PriceReRegisterForeignNds { get; set; }

        public decimal? PriceRegisterKz { get; set; }

        public decimal? PriceRegisterKzNds { get; set; }

        public decimal? PriceReRegisterKz { get; set; }

        public decimal? PriceReRegisterKzNds { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DirectionToPays_PriceList> EXP_DirectionToPays_PriceList { get; set; }
    }
}
