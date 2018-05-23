using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class OrganizationAddress : BaseEntity
    {
        /// <summary>
        /// Область
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Конечный полный адрес
        /// </summary>
        public string Address { get; set; }
    }
}
