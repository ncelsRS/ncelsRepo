using System;
using PW.Ncels.Database.DataModel;
using System.Collections.Generic;

namespace PW.Prism.ViewModels.Commissions
{
    public class CommissionEmployee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
    }

    public class CommissionEmployeeDepartment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CommissionEmployeeDepartment> Departments { get; set; }
        public List<CommissionEmployee> Employees { get; set; }
    }
}