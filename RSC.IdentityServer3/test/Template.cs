namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Template
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Template()
        {
            Documents = new HashSet<Document>();
        }

        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public int? Type { get; set; }

        public byte[] Data { get; set; }

        public int? TemplateType { get; set; }

        public byte[] Report { get; set; }

        public Guid? ConvertDictionaryTypeId { get; set; }

        public int? ConvertType { get; set; }

        [StringLength(4000)]
        public string DictionaryTypeValue { get; set; }

        [StringLength(4000)]
        public string DictionaryTypeId { get; set; }

        [StringLength(4000)]
        public string Value { get; set; }

        [StringLength(4000)]
        public string Validation { get; set; }

        public byte[] PrintForm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
