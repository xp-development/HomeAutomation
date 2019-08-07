using HomeAutomation.Server.Core.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Server.Core.UnitTests
{
  public abstract class DatabaseTestBase
  {
    protected DatabaseTestBase()
    {
      ServerDatabaseContext.Options = new DbContextOptionsBuilder<ServerDatabaseContext>()
        .UseInMemoryDatabase(databaseName: "Testing")
        .Options;
    }
  }
}