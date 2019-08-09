using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Server.Core.DataAccessLayer
{
  public class ServerDatabaseContext : DbContext
  {
    public static DbContextOptions<ServerDatabaseContext> Options { get; set; }

    public ServerDatabaseContext()
      : base((DbContextOptions) Options ?? new DbContextOptions<DbContext>())
    {
    }

    public DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if(Options != null)
        optionsBuilder.UseInMemoryDatabase("Testing");
      else
        optionsBuilder.UseSqlite(@"Data Source=Server.db");
    }

    public override int SaveChanges()
    {
      ChangeTracker.Entries<EntityBase>().Where(x => x.State == EntityState.Added).ToList().ForEach(x =>
      {
        x.Entity.Guid = Guid.NewGuid();
        x.Entity.CreatedDateTime = DateTime.Now;
        x.Entity.EditedDateTime = DateTime.Now;
      });

      ChangeTracker.Entries<EntityBase>().Where(x => x.State == EntityState.Modified).ToList().ForEach(x =>
      {
        x.Entity.Guid = Guid.NewGuid();
        x.Entity.EditedDateTime = DateTime.Now;
      });

      return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Room>().HasKey(p => p.Id);
      modelBuilder.Entity<Room>().Property(s => s.Id).ValueGeneratedOnAdd();
    }
  }
}