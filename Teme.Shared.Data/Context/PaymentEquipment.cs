using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
  /// <summary>
  /// Упаковка
  /// </summary>
  public class PaymentEquipment : BaseEntity
  {
    /// <summary>
    /// Заявка на платеж
    /// </summary>
    public int PaymentId { get; set; }
    [ForeignKey("PaymentId")]
    public Payment Payment { get; set; }

    /// <summary>
    /// Тип комплектации ИМН и МТ
    /// </summary>
    public int EquipmentTypeId { get; set; }
    [ForeignKey("EquipmentTypeId")]
    public Ref_EquipmentType Ref_EquipmentType { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ID
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Мордель
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Производитель
    /// </summary>
    public string Manufacturer { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    public int? CountryId { get; set; }
    [ForeignKey("CountryId")]
    public Ref_Country Ref_Country { get; set; }
  }
}
