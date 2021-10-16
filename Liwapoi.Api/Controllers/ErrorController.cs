using Liwapoi.Api.Models.ResponseModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace Liwapoi.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {

        private readonly ILogger _logger;

        public ErrorController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500;

            if (exception is UnauthorizedAccessException)
            {
                code = 401;
            }

            Response.StatusCode = code;

            _logger.Error(exception);

            return new ErrorResponse(exception);
        }
    }
}
