using System.Collections.Generic;
using System.Reflection.Emit;
using GM_Safety_Notification.Models;
using Microsoft.EntityFrameworkCore;

namespace GM_Safety_Notification.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(e =>
        {
            
            e.HasKey(x => x.Id);
            e.Property(x => x.Recipient).HasMaxLength(200);
            e.Property(x => x.Message).HasMaxLength(1000);
        });
    }
}
