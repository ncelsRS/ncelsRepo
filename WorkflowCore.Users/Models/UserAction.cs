﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowCore.Users.Models
{
    public class UserAction
    {
        public string User { get; set; }
        public string OutcomeValue { get; set; }
        public object Value { get; set; }
    }
}
