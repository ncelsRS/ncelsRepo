using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        [Required]
        [MaxLength(12)]
        public string IdNumber { get; set; }
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
        public string NameEn { get; set; }
        /// <summary>
        /// true резидент, false не резидент
        /// </summary>
        [Required]
        public bool IsResident { get; set; }
        /// <summary>
        /// подтверждени
        /// </summary>
        [Required]
        public bool IsConfirmed { get; set; } = false;

        public virtual ICollection<Contract> DeclarantContract { get; set; }
        public virtual ICollection<Contract> ManufacturContract { get; set; }
        public virtual ICollection<Contract> PayerContract { get; set; }
    }
}
