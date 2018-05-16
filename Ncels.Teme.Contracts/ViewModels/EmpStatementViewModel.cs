using System;

namespace Ncels.Teme.Contracts.ViewModels
{
    public class EmpStatementViewModel
    {
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
    }
}
