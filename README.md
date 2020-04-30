# Calzolari-ILogger-extensions
Extensions on Ilogger for unit testing

![](https://github.com/AnthonyGiretti/Calzolari-ILogger-extensions/workflows/dotnetcore/badge.svg)
[![Nuget](https://img.shields.io/nuget/v/Calzolari.ILogger.Extensions.NSusbtitute)](https://www.nuget.org/packages/Calzolari.ILogger.Extensions.NSusbtitute/) (NSusbtitute dependency)

Must match your Microsoft.Extensions.Logging version in your project

### CURRENTLY NOT WORKING WITH Microsoft.Extensions.Logging 3+,vTRYING TO FIX IT ASAP

Examples: 

If you use ILogger from dependency Microsoft.Extensions.Logging (>= 3.1.3), use package Microsoft.Extensions.Logging (>= 3.1.3.x)

If you use ILogger from dependency Microsoft.Extensions.Logging (>= 3.1.2), use package Microsoft.Extensions.Logging (>= 3.1.2.x)

If you use ILogger from dependency Microsoft.Extensions.Logging (>= 3.1.1), use package Microsoft.Extensions.Logging (>= 3.1.1.x)

If you use ILogger from dependency Microsoft.Extensions.Logging (>= 2.2.0), use package Microsoft.Extensions.Logging (>= 2.2.0.x)

## Features

- Provides ILogger matching arguments check with a LogLevel, an exception and a message
- Provides ILogger matching arguments check with a LogLevel and a message
- Provides ILogger non matching arguments check (no logger called at all)
- Provides ILogger non matching arguments check with a LogLevel, an exception and a message
- Provides ILogger non matching arguments check with a LogLevel and a message

### Usage with NSubstitute

#### Matching argument check with a LogLevel, exception and a message

Service:
```csharp
public object GetData(string name)
{
    try 
    {
        return myService.Get(name);
    }
    catch (Exception e)
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
    var exception = new Exception("an error occured");
    myServiceMock.Get(Arg.Any<string>()).Throws(exception);

    // Act
    sut.GetData("test");

    // Assert
    loggerMock.ReceivedMatchingArgs(LogLevel.Error, exception, "Error occured during Get execution");
}
```

#### Matching argument check with a LogLevel and a message

Service:
```csharp
public object GetData(string name)
{
    logger.LogDebug("Calling Get method");
    return myService.Get(name);
}
```

Unit test:
```csharp
public void When_invoked_should_logDebug_should_be_called()
{
    // Arrange
    myServiceMock.Get(Arg.Any<string>()).Returns("Hello");

    // Act
    sut.GetData("test");

    // Assert
    loggerMock.ReceivedMatchingArgs(LogLevel.Debug, "Calling Get method");
}
```
#### Non matching argument check (logger hasn't been called at all)

Service:
```csharp
public object GetData(string name)
{
    try
    {
        var result = myService.Get(name);
        logger.LogDebug("Get method called");
        return result;
    }
    catch (Exception e)
    {
        return string.Empty;
    }
}
```

Unit test:
```csharp
public void When_invoked_logger_should_not_be_called()
{
    // Arrange
    myServiceMock.Get(Arg.Any<string>()).Throws(new Exception())

    // Act
    sut.GetData("test");

    // Assert
    loggerMock.DidNotReceiveMatchingArgs();
}
```
#### Non matching argument check for a specific LogLevel

Service:
```csharp
public object GetData(string name)
{
    try
    {
        var result = myService.Get(name);
        logger.LogInformation("Calling Get method");
        return result;
    }
    catch (Exception e)
    {
        logger.LogError("Error occured during Get execution");
        return string.Empty;
    }
}
```

Unit test:
```csharp
public void When_invoked_logError_should_not_be_called()
{
    // Arrange
    myServiceMock.Get(Arg.Any<string>()).Returns("Hello")

    // Act
    sut.GetData("test");

    // Assert
    loggerMock.DidNotReceiveMatchingArgs(LogLevel.Error);
}
```

#### Non matching argument check for a specific LogLevel and message

Service:
```csharp
public object GetData(string name)
{
    try
    {
        logger.LogDebug("Before calling Get method");
        var result = myService.Get(name);
        logger.LogDebug("After calling Get method");
        return result;
    }
    catch (Exception e)
    {
        logger.LogError("Error occured during Get execution");
        return string.Empty;
    }
}
```

Unit test:
```csharp
public void When_invoked_logDebug_after_calling_method_should_not_be_called()
{
    // Arrange
    myServiceMock.Get(Arg.Any<string>()).Throws(new Exception())

    // Act
    sut.GetData("test");

    // Assert
    loggerMock.ReceivedMatchingArgs(LogLevel.Debug, "Before calling Get method");
    loggerMock.ReceivedMatchingArgs(LogLevel.Error, "Error occured during Get execution");
    loggerMock.DidNotReceiveMatchingArgs(LogLevel.Debug, "After calling Get method");
}
```
