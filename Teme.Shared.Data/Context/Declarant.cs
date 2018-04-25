using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Заявитель
    /// </summary>
    public class Declarant : BaseEntity
    {
        public Declarant()
        {
            DeclarantContract = new HashSet<Contract>();
            ManufacturContract = new HashSet<Contract>();
            PayerContract = new HashSet<Contract>();
        }
        /// <summary>
        /// ИИН и БИН
        /// </summary>
        [MaxLength(12)]
        public string IdNumber { get; set; }
        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public string NameEn { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Ref_Country Ref_Country { get; set; }

        /// <summary>
        /// Огранизационная форма
        /// </summary>
        public int? OrganizationFormId { get; set; }
        [ForeignKey("OrganizationFormId")]
        public Ref_OrganizationForm Ref_OrganizationForm { get; set; }

        /// <summary>
        /// true резидент, false не резидент
        /// </summary>
        public bool IsResident { get; set; }

        /// <summary>
        /// подтверждени
        /// </summary>
        public bool IsConfirmed { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Contract> DeclarantContract { get; set; }
        public virtual ICollection<Contract> ManufacturContract { get; set; }
        public virtual ICollection<Contract> PayerContract { get; set; }
    }
}
