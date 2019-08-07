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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Room>().HasKey(p => p.Id);
      modelBuilder.Entity<Room>().Property(s => s.Id).ValueGeneratedOnAdd();
      modelBuilder.Entity<Room>().Property(s => s.CreatedDateTime).ValueGeneratedOnAdd();
      modelBuilder.Entity<Room>().Property(s => s.EditedDateTime).ValueGeneratedOnAddOrUpdate();
      modelBuilder.Entity<Room>().Property(s => s.Guid).ValueGeneratedOnAdd();
    }
  }
}