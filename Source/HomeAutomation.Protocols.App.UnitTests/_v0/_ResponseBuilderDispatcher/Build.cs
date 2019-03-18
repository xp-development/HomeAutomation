using System;
using FluentAssertions;
using HomeAutomation.Protocols.App.v0;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using HomeAutomation.Protocols.App.v0.ResponseBuilders;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._ResponseBuilderDispatcher
{
  public class Build
  {
    [Fact]
    public void ShouldCallResponseBuilder()
    {
      var requestMock = new Mock<IRequest>();
      var responseBuilderMock = new Mock<IResponseBuilder>();
      responseBuilderMock.Setup(x => x.RequestType).Returns(requestMock.Object.GetType);

      var dispatcher = new ResponseBuilderDispatcher(new [] { responseBuilderMock.Object });

      dispatcher.Build(requestMock.Object);

      responseBuilderMock.Verify(x => x.Build(It.Is<IRequest>(y => y == requestMock.Object)));
    }

    [Fact]
    public void ShouldThrowExceptionIfNoProperResponseBuilderIsRegistered()
    {
      var requestMock = new Mock<IRequest>();

      var dispatcher = new ResponseBuilderDispatcher(new IResponseBuilder[0]);

      new Action(() => dispatcher.Build(requestMock.Object)).Should().Throw<ArgumentException>();
    }
  }
}