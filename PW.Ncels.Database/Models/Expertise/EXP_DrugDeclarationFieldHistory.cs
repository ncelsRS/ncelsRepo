﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DrugDeclarationFieldHistory
    {
        public string CreateDateStr => CreateDate.ToString("dd/MM/yyyy HH:mm");
        public string OwnerName => Employee?.DisplayName;
    }
}
