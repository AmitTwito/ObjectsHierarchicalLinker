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
        private readonly IObjectEntitiesService _objectEntitiesService;
        private readonly ILogger<ObjectsHierarchyCreatorController> _logger;

        public ObjectsHierarchyCreatorController(IObjectEntitiesService objectEntitiesService, ILogger<ObjectsHierarchyCreatorController> logger)
        {
            _objectEntitiesService = objectEntitiesService;
            _logger = logger;
        }

        // GET: api/<ObjectsHierarchicalLinkerController>
        [HttpPost]
        [ProducesResponseType(typeof(List<HierarchyObject>), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        async public Task<ActionResult> SaveDataAndCreateHierarchy([FromBody] List<ObjectEntity> objects)
        {
            try
            {
                _logger.LogInformation("Started creating the objects hierarchy...");
                if(objects.Count == 0)
                {
                    _logger.LogWarning($"There are no objects in the hierarchy. Sending response: []");
                    return Ok(new List<HierarchyObject>());
                }
                var hierarchy = await Task.Run(() =>
                {
                    var entities = _objectEntitiesService.ValidateInput(objects);
                    this._objectEntitiesService.SaveObjectEntities(entities);
                    return this._objectEntitiesService.CreateAndGetHierarchy();
                });

                _logger.LogInformation($"Successfully created the objects hierarchy, sending response: \n{hierarchy.ToJsonString()}");
                return Ok(hierarchy.Objects);
            }
            catch (InvalidInputException e)
            {
                _logger.LogError("Error while checking the input");
                return LogErrorAndSendResponseByStatusCode("Error while checking the input", e, StatusCodes.Status400BadRequest);
            }
            catch (DALException e)
            {
                return LogErrorAndSendResponseByStatusCode("Error while doing a data access operation", e, StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                return LogErrorAndSendResponseByStatusCode("An error occurred while creating the objects hierarchy", e, StatusCodes.Status500InternalServerError);
            }

        }

        private ActionResult LogErrorAndSendResponseByStatusCode(string message, Exception e, int statusCode)
        {
            
            _logger.LogError(e.Message);
            return StatusCode(statusCode, new ErrorMessage() { message = e.Message });
        }
    }
}
