using System;

namespace HomeAutomation.Protocols.App.v0
{
  public class RequestTypeAttribute : Attribute
  {
    public RequestTypeAttribute(byte requestType0, byte requestType1, byte requestType2, byte requestType3)
    {
      RequestType0 = requestType0;
      RequestType1 = requestType1;
      RequestType2 = requestType2;
      RequestType3 = requestType3;
    }

    public byte RequestType0 { get; }
    public byte RequestType1 { get; }
    public byte RequestType2 { get; }
    public byte RequestType3 { get; }

    private bool Equals(RequestTypeAttribute other)
    {
      return base.Equals(other) && RequestType0 == other.RequestType0 && RequestType1 == other.RequestType1 &&
             RequestType2 == other.RequestType2 && RequestType3 == other.RequestType3;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((RequestTypeAttribute) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = base.GetHashCode();
        hashCode = (hashCode * 397) ^ RequestType0.GetHashCode();
        hashCode = (hashCode * 397) ^ RequestType1.GetHashCode();
        hashCode = (hashCode * 397) ^ RequestType2.GetHashCode();
        hashCode = (hashCode * 397) ^ RequestType3.GetHashCode();
        return hashCode;
      }
    }
  }
}