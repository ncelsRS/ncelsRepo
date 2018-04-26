using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Дата создания сущности
        /// </summary>
        [Column(Order = 101)]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Дата изменения сущности
        /// </summary>
        [Column(Order = 102)]
        public DateTime DateUpdate { get; set; } = DateTime.Now;

        public virtual string ClassName => this.GetType().FullName;
    }
}
