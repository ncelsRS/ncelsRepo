using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
  /// <summary>
  /// Тип производителя
  /// </summary>
  public class PaymentPlatform : BaseEntity
  {
    /// <summary>
    /// Заявка на платеж
    /// </summary>
    public int PaymentId { get; set; }
    [ForeignKey("PaymentId")]
    public Payment Payment { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    public int? CountryId { get; set; }
    [ForeignKey("CountryId")]
    public Ref_Country Ref_Country { get; set; }

    public string NameRu { get; set; }
    public string NameKz { get; set; }
    public string NameEn { get; set; }

    /// <summary>
    /// Юридический адрес
    /// </summary>
    [MaxLength(255)]
    public string LegalAddress { get; set; }

    /// <summary>
    /// Фактический адрес
    /// </summary>
    [MaxLength(255)]
    public string FactAddress { get; set; }

  }
}
