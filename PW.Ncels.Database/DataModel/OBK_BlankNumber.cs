namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_BlankNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_BlankNumber()
        {
            OBK_BlankNumber1 = new HashSet<OBK_BlankNumber>();
        }

        public Guid Id { get; set; }

        public Guid? Object_Id { get; set; }

        public int? Number { get; set; }

        public short? BlankTypeId { get; set; }

        public Guid? EmployeeId { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Decommissioned { get; set; }

        public bool? Corrupted { get; set; }

        public DateTime? DecommissionedDate { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime? CorruptDate { get; set; }

        public Guid? CorruptEmployee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_BlankNumber> OBK_BlankNumber1 { get; set; }

        public virtual OBK_BlankNumber OBK_BlankNumber2 { get; set; }

        public virtual OBK_BlankType OBK_BlankType { get; set; }
    }
}
