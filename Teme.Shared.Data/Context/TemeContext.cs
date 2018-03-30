using Microsoft.EntityFrameworkCore;

namespace Teme.Shared.Data.Context
{
    public class TemeContext : DbContext
    {
        public TemeContext()
        {
        }
        public TemeContext(DbContextOptions<TemeContext> options)
            : base(options)
        {
        }
        public DbSet<AuthUser> AuthUsers { get; set; }
    }
}
