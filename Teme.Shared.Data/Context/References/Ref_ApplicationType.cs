using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Тип заявки для калькулятора(справчоник)
    /// </summary>
    public class Ref_ApplicationType : Reference
    {
        /// <summary>
        /// Регистрация, перерегистрация, внесение изменений
        /// </summary>
        [MaxLength(20)]
        public string ContractForm { get; set; }

        public virtual ICollection<Ref_ServiceType> Ref_ServiceTypes { get; set; }
    }
}
