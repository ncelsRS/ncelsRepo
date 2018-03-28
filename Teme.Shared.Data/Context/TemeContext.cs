using Microsoft.EntityFrameworkCore;

namespace Teme.Shared.Data.Context
{
    public class TemeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
