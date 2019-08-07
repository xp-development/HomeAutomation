using System;

namespace HomeAutomation.Server.Core.DataAccessLayer
{
  public class EntityBase
  {
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime EditedDateTime { get; set; }
  }
}