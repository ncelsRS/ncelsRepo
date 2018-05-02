using System.ComponentModel.DataAnnotations.Schema;

namespace Teme.Shared.Data.Context
{
    public class UserForAction : BaseEntity
    {

        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
        public int UserId { get; set; }
        public int ExecutorId { get; set; }
    }
}