using gm_safety_thirdparty.Models;
using Microsoft.EntityFrameworkCore;
//using gm_safety_thirdparty.Models;

namespace gm_safety_thirdparty.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<ThirdPartyVendor> ThirdPartyVendors { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map to your Postgres table
            modelBuilder.Entity<Employee>()
                .ToTable("employees_data");

            base.OnModelCreating(modelBuilder);
        }
    }
}