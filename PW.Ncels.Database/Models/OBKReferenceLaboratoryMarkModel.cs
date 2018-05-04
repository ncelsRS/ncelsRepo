﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public class OBKReferenceLaboratoryMarkModel
    {
        public Guid Id { get; set; }

        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }

        public List<Guid> RegulationList { get; set; }
    }
}
