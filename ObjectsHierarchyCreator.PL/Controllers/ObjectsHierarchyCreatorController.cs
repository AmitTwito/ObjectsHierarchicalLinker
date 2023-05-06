using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectsHierarchyCreator.BE;
using ObjectsHierarchyCreator.BL;
using ObjectsHierarchyCreator.PL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.PL.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ObjectsHierarchyCreatorController : ControllerBase
    {
        private readonly IObjectEntitiesBL _objectEntitiesBL;
        private readonly ILogger<ObjectsHierarchyCreatorController> _logger;

        public ObjectsHierarchyCreatorController(IObjectEntitiesBL objectsBL, ILogger<ObjectsHierarchyCreatorController> logger)
        {
            _objectEntitiesBL = objectsBL;
            _logger = logger;
        }

        // GET: api/<ObjectsHierarchicalLinkerController>
        [HttpPost]
        [ProducesResponseType(typeof(List<ObjectsHierarchy>), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        async public Task<ActionResult> SaveDataAndCreateHierarchy([FromBody] List<ObjectEntity> objects)
        {
            try
            {
                _logger.LogInformation("Started creating the objects hierarchy...");

                var hierarchy = await Task.Run(() =>
                {
                    var entities = _objectEntitiesBL.ValidateInput(objects);
                    this._objectEntitiesBL.SaveObjectEntities(entities);
                    return this._objectEntitiesBL.CreateAndGetHeirarchy();
                });

                _logger.LogInformation($"Successfully created the objects hierarchy, sending response: \n{hierarchy.ToJsonString()},");
                return Ok(hierarchy.Objects);
            }

            catch (InvalidInputException e)
            {
                return LogErrorAndSendResponseByStatusCode("Error while checking the input", e, StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {

                return LogErrorAndSendResponseByStatusCode("An error occurred while creating the objects hierarchy", e, StatusCodes.Status500InternalServerError);
            }

        }

        private ActionResult LogErrorAndSendResponseByStatusCode(string message, Exception e, int statusCode)
        {
            _logger.LogError("Error while checking the input");
            _logger.LogError(e.Message);
            return StatusCode(statusCode, new ErrorMessage() { message = e.Message });
        }
    }
}
