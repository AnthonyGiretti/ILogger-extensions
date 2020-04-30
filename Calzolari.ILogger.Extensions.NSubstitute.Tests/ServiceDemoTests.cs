using Calzolari.ILogger.Extensions.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Calzolari.ILogger.Extensions.NSubstitute.Tests
{
    public class ServiceDemoTests
    {
        private readonly IRepositoryDemo _myServiceMock;
        private readonly ILogger<ServiceDemo> _loggerMock;
        private readonly ServiceDemo _sut;

        public ServiceDemoTests()
        {
            _myServiceMock = Substitute.For<IRepositoryDemo>();
            _loggerMock = Substitute.For<ILogger<ServiceDemo>>();
            _sut = new ServiceDemo(_loggerMock, _myServiceMock);
        }

        [Fact]
        public void When_exception_is_raised_should_log_error_exception()
        {
            // Arrange
            var exception = new Exception("an error occured");
            _myServiceMock.Get(Arg.Any<string>()).Throws(exception);

            // Act
            _sut.GetData("test");

            // Assert
            _loggerMock.ReceivedMatchingArgs(LogLevel.Error, exception, "Error occured during Get execution");
        }
    }
}