# Calzolari-ILogger-extensions
Extensions on Ilogger for unit testing

![](https://github.com/AnthonyGiretti/Calzolari-ILogger-extensions/workflows/dotnetcore/badge.svg)
[![Nuget](https://img.shields.io/nuget/v/Calzolari.ILogger.Extensions.NSusbtitute)](https://www.nuget.org/packages/Calzolari.ILogger.Extensions.NSusbtitute/) (NSusbtitute dependency)

## Features

- Provides ILogger matching arguments check with a LogLevel and a message
- Provides ILogger matching arguments check with a LogLevel, an exception and a message
- Provides ILogger non matching arguments check

### Usage with NSubstitute

#### Argument matching check with a LogLevel, exception and a message

Service:
```csharp
public object GetData(string name)
{
    try 
    {
        return myService.Get(name);
    }
    catch(Exception e)
    {
        logger.LogError(e, "Error occured during Get execution");
        return string.Empty;
    }
}
```

Unit test:
```csharp
public void When_exception_is_raised_should_log_error_exception()
{
    // Arrange
    var exception = new Exception("an error occured")
    myServiceMock.Get(Arg.Any<string>()).Throws(exception);

    // Act
    sut.GetData("test");

    // Assert
    loggerMock.ReceivedMatchingArgs(LogLevel.Error, exception, "Error occured during Get execution");
}
```
