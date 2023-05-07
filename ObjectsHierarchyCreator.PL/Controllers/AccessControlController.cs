using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ObjectsHierarchyCreator.BE.AccessControl;
using ObjectsHierarchyCreator.BE.Utils;
using ObjectsHierarchyCreator.BL;
using ObjectsHierarchyCreator.PL.Utils;
using System.Reflection;

namespace ObjectsHierarchyCreator.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessControlController : ControllerBase
    {
        private readonly IOptions<AppConfig> _config;
        private readonly IAccessControlService _accessControlService;
        private readonly ILogger _logger;
        public AccessControlController(IAccessControlService accessControlService, IOptions<AppConfig> config, ILogger<AccessControlController> logger)
        {
            _accessControlService = accessControlService;
            _config = config;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public ActionResult GetAccessToken(AuthRequest request)
        {
            var response = _accessControlService.GetToken(request);

            if (response == null)
                return BadRequest(new ErrorMessage { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
