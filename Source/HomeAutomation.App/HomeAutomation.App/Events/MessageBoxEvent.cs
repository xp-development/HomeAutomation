namespace HomeAutomation.App.Events
{
  public class MessageBoxEvent : IEvent
  {
    public MessageBoxEvent(string title, string message, string accept, string cancel)
    {
      Title = title;
      Message = message;
      Accept = accept;
      Cancel = cancel;
    }

    public string Title { get; }
    public string Message { get; }
    public string Accept { get; }
    public string Cancel { get; }

    public bool Result { get; set; }
  }
}