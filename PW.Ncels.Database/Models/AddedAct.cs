using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class AddedAct
    {
        public string Number { get; set; }
        public string Worker { get; set; }
        public DateTime? ActDate { get; set; }
        public Guid Id { get; set; }
    }
}