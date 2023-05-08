using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using ObjectsHierarchyCreator.BE;
using ObjectsHierarchyCreator.BE.AccessControl;
using ObjectsHierarchyCreator.BE.Utilities;
using ObjectsHierarchyCreator.BL;
using ObjectsHierarchyCreator.PL.Utilities;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessControlController : CustomController
    {
        private readonly IOptions<AppConfig> _config;
        private readonly IAccessControlService _accessControlService;
        private readonly ILogger _logger;
        public AccessControlController(IAccessControlService accessControlService, IOptions<AppConfig> config, ILogger<AccessControlController> logger)
            : base(logger)
        {
            _accessControlService = accessControlService;
            _config = config;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("~/api/auth/token")]
        public async Task<ActionResult> GetAccessToken(AuthRequest request)
        {
            _logger.LogInformation($"Authentication attempt with username '{request.Username}'. Generating access token...");
            try
            {
                var token = await Task.Run(() =>
                {
                    return _accessControlService.GetAccessToken(request);
                });

                if (token == null)
                {
                    _logger.LogWarning($"Failed authentication attempt with '{request.Username}'. Incorrect username or password.");
                    return Unauthorized(new ErrorMessage { Message = "Invalid username or password" });
                }

                _logger.LogInformation($"Successfully generated access token for {request.Username}.");

                return Ok(token);
            }
            catch (Exception e)
            {
                return LogErrorAndSendResponseByStatusCode(e, StatusCodes.Status500InternalServerError);
            }

        }
    }
}
