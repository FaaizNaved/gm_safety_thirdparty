using System.Collections.Generic;
using System.Reflection.Emit;
using GM_Safety_Functional.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GM_Safety_Functional.Data;

public class FunctionalDbContext : DbContext
{
    public FunctionalDbContext(DbContextOptions<FunctionalDbContext> options) : base(options) { }

    public DbSet<FunctionalTask> FunctionalTasks => Set<FunctionalTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FunctionalTask>(e =>
        {
            
            e.HasKey(x => x.Id);
            e.Property(x => x.TaskName).HasMaxLength(200);
            e.Property(x => x.Module).HasMaxLength(100);
        });
    }
}
