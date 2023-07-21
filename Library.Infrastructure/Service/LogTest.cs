using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class LogTest
    {
        private readonly ILogger<LogTest> _logger;
        public LogTest(ILogger<LogTest> logger)
        {
            _logger = logger;
            var message = $" LogTest logger created at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(message);
        }
    }
}
