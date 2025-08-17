using gm_safety_thirdparty.Models;
using Microsoft.EntityFrameworkCore;

namespace gm_safety_thirdparty.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ThirdPartyVendor> ThirdPartyVendors { get; set; }
    }
}
