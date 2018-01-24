namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_LetterFromEdo
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string IdEdo { get; set; }

        public long? LetterPortalEdoID { get; set; }

        [StringLength(50)]
        public string LetterRegNomer { get; set; }

        public DateTime? LetterRegDate { get; set; }

        [StringLength(100)]
        public string UserEdo { get; set; }

        [StringLength(250)]
        public string LetterText { get; set; }

        public virtual OBK_LetterPortalEdo OBK_LetterPortalEdo { get; set; }
    }
}
