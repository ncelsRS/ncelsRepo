using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        //Дата создания записи
        [Required]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        //Дата изменеия заиписи
        [Required]
        public DateTime DateUpdate { get; set; } = DateTime.Now;
    }
}
