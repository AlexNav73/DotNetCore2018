using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Filters
{
    public class LogActionAttribute : TypeFilterAttribute
    {
        public LogActionAttribute() : base(typeof(LoggingFilter))
        {
        }

        private class LoggingFilter : IActionFilter
        {
            private readonly ILogger<LoggingFilter> _logger;

            public LoggingFilter(ILogger<LoggingFilter> logger)
            {
                _logger = logger;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _logger.LogTrace($"Before executing. Controller: {context.Controller.GetType().FullName} Action: {context.ActionDescriptor.DisplayName}");
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.LogTrace($"After executing. Controller: {context.Controller.GetType().FullName} Action: {context.ActionDescriptor.DisplayName}");
            }
        }
    }
}