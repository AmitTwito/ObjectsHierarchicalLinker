using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ObjectsHierarchyCreator.BE.Utilities;
using ObjectsHierarchyCreator.PL.Utilities;
using System;

namespace ObjectsHierarchyCreator.PL.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        private readonly ILogger<CustomController> _logger;
        public CustomController(ILogger<CustomController> logger)
        {
            _logger = logger;
        }

        protected virtual ActionResult LogErrorAndSendResponseByStatusCode(Exception e, int statusCode)
        {
            _logger.LogError(e.Message);
            _logger.LogError(e.StackTrace);
            return StatusCode(statusCode, new ErrorMessage() { Message = e.Message });
        }
    }
}
