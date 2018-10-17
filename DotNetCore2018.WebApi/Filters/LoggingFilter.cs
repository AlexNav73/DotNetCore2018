using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Filters
{
    public class LogActionAttribute : Attribute, IFilterFactory
    {
        private readonly bool _enable;

        public bool IsReusable => true;

        public LogActionAttribute(bool enable = true)
        {
            _enable = enable;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var logger = (ILogger<LoggingFilter>)serviceProvider.GetService(typeof(ILogger<LoggingFilter>));
            return new LoggingFilter(logger, _enable);
        }

        private class LoggingFilter : IActionFilter
        {
            private readonly bool _enable;
            private readonly ILogger<LoggingFilter> _logger;

            public LoggingFilter(ILogger<LoggingFilter> logger, bool enable)
            {
                _logger = logger;
                _enable = enable;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (_enable)
                {
                    _logger.LogTrace($"Before executing. Controller: {context.Controller.GetType().FullName} Action: {context.ActionDescriptor.DisplayName}");
                }
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (_enable)
                {
                    _logger.LogTrace($"After executing. Controller: {context.Controller.GetType().FullName} Action: {context.ActionDescriptor.DisplayName}");
                }
            }
        }
    }
}