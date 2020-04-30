using Microsoft.Extensions.Logging;
using System;

namespace Calzolari.ILogger.Extensions.Services
{
    public class ServiceDemo
    {
        private readonly ILogger<ServiceDemo> _logger;
        private readonly IRepositoryDemo _repositoryDemo;

        public ServiceDemo(ILogger<ServiceDemo> logger, IRepositoryDemo repositoryDemo)
        {
            _logger = logger;
            _repositoryDemo = repositoryDemo;
        }

        public object GetData(string name)
        {
            try
            {
                _logger.LogDebug("Calling Get method");
                _logger.LogInformation("Calling Get method");
                var result = _repositoryDemo.Get(name);
                _logger.LogDebug("Get method called");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occured during Get execution");
                return string.Empty;
            }
        }
    }
}
