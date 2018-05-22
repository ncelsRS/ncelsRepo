using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Производитель для заявки на платеж и заявления
    /// </summary>
    public class MedicalDeviceManufacturer : BaseEntity
    {
        public Declarant Manufacture { get; set; }
        public DeclarantDetail ManufactureDetail { get; set; }
    }
}
