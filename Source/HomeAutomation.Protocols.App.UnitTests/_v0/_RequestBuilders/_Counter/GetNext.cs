using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestBuilders._Counter
{
  public class GetNext
  {
    [Fact]
    public void Usage()
    {
      var counter = new Counter();

      counter.GetNext().Should().Equal(0x01, 0x00);
      counter.GetNext().Should().Equal(0x02, 0x00);
      counter.GetNext().Should().Equal(0x03, 0x00);
    }
  }
}