using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
  /// <summary>
  /// Заявка на платеж
  /// </summary>
    class Payment : BaseEntity
    {
    /// <summary>
    /// Id Договора
    /// </summary>
    public int ContractId { get; set; }
    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; }

    /// <summary>
    /// Номер регистрационного удостоверения
    /// </summary>
    public string CardNumber { get; set; }

    /// <summary>
    /// Тип ИМН/МТ
    /// </summary>
    public bool IsTypeImnMt { get; set; }

    /// <summary>
    /// Торговое название
    /// </summary>
    public string TradeName { get; set; }

    /// <summary>
    /// Тип услуги
    /// </summary>
    public int ServiceTypeId { get; set; }
    [ForeignKey("ServiceTypeId")]
    public virtual Ref_ServiceType Ref_ServiceType { get; set; }
    
  }
}
