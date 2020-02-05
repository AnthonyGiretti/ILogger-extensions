using ExpectedObjects;
using NSubstitute;
using System;

namespace Microsoft.Extensions.Logging
{
    public static class ILoggerExtensions
    {
        /// <summary>
        /// Logger has been called with a specific <see cref="LogLevel"/> and <see cref="string"/> message
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="formattedMessage"></param>
        public static void ReceivedMatchingArgs(this ILogger logger, LogLevel logLevel, string formattedMessage)
        {
            logger.Received().Log(Arg.Is(logLevel),
                                  Arg.Is<EventId>(0),
                                  Arg.Is<object>(x => x.ToString() == formattedMessage),
                                  Arg.Is<Exception>(x => x == null),
                                  Arg.Any<Func<object, Exception, string>>());
        }


        /// <summary>
        /// Logger has been called with a specific <see cref="LogLevel"/>, <see cref="Exception"/> and <see cref="string"/> message
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="formattedMessage"></param>
        public static void ReceivedMatchingArgs(this ILogger logger, LogLevel logLevel, Exception exception, string formattedMessage)
        {
            if (exception == null)
            {
                logger.ReceivedMatchingArgs(logLevel, formattedMessage);
            }

            var expectedException = exception.ToExpectedObject();

            logger.Received().Log(Arg.Is(logLevel),
                                  Arg.Is<EventId>(0),
                                  Arg.Is<object>(x => x.ToString() == formattedMessage),
                                  Arg.Is<Exception>(x => expectedException.Equals(x)),
                                  Arg.Any<Func<object, Exception, string>>());
        }

        /// <summary>
        /// No logger has been called
        /// </summary>
        /// <param name="logger"></param>
        public static void DidNotReceiveMatchingLogArgs(this ILogger logger)
        {
            logger.DidNotReceive().Log(Arg.Any<LogLevel>(),
                                       Arg.Any<EventId>(),
                                       Arg.Any<object>(),
                                       Arg.Any<Exception>(),
                                       Arg.Any<Func<object, Exception, string>>());
        }

        /// <summary>
        /// No logger has been called with a specific <see cref="LogLevel"/>
        /// </summary>
        /// <param name="logger"></param>
        public static void DidNotReceiveMatchingLogArgs(this ILogger logger, LogLevel logLevel)
        {
            logger.DidNotReceive().Log(Arg.Is(logLevel),
                                       Arg.Any<EventId>(),
                                       Arg.Any<object>(),
                                       Arg.Any<Exception>(),
                                       Arg.Any<Func<object, Exception, string>>());
        }

        /// <summary>
        /// No logger has been called with a specific <see cref="LogLevel"/> and <see cref="string"/> message
        /// </summary>
        /// <param name="logger"></param>
        public static void DidNotReceiveMatchingLogArgs(this ILogger logger, LogLevel logLevel, string formattedMessage)
        {
            logger.DidNotReceive().Log(Arg.Is(logLevel),
                                       Arg.Any<EventId>(),
                                       Arg.Is<object>(x => x.ToString() == formattedMessage),
                                       Arg.Any<Exception>(),
                                       Arg.Any<Func<object, Exception, string>>());
        }
    }
}