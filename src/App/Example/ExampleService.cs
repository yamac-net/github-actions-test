using Microsoft.Extensions.Logging;
using System;

namespace App.Example
{
    public class ExampleService : IExampleService
    {
        private readonly ILogger _logger;

        public ExampleService(ILogger<ExampleService> logger)
        {
            _logger = logger;
        }

        public DateTime GetCurrentTime()
        {
            _logger.LogDebug("debug");
            return DateTime.Now;
        }
    }
}
