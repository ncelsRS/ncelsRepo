using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKReplacedBlanks
    {
        public Guid? Id { get; set; }
        public string CorruptedBlankNumber { get; set; }
        public string NewBlankNumber { get; set; }
    }
}