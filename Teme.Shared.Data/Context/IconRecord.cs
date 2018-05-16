using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class IconRecord : BaseEntity
    {
        public IconRecord()
        {
            AuthUsers = new HashSet<AuthUser>();
        }
        public int IconId { get; set; }
        [ForeignKey("IconId")]
        public virtual Icon Icon { get; set; }

        public int AuthUserId { get; set; }
        [ForeignKey("AuthUserId")]
        public virtual AuthUser AuthUser { get; set; }
        public int AuthRoleId { get; set; }
        [MaxLength(500)]
        public string ValueField { get; set; }
        [MaxLength(500)]
        public string DisplayField { get; set; }
        [MaxLength(2000)]
        public string Note { get; set; }

        public virtual ICollection<AuthUser> AuthUsers { get; set; }
    }
}
