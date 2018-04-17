using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class Reference : BaseEntity
    {
        public string Code { get; set; }
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
