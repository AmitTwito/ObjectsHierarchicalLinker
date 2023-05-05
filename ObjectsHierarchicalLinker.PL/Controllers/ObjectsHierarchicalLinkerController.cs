using Microsoft.AspNetCore.Mvc;
using ObjectsHierarchicalLinker.BE;
using ObjectsHierarchicalLinker.BL;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.PL.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ObjectsHierarchicalLinkerController : ControllerBase
    {
        private readonly IObjectsBL _objectsBL;
        public ObjectsHierarchicalLinkerController(IObjectsBL objectsBL)
        {
            _objectsBL = objectsBL;
        }

        // GET: api/<ObjectsHierarchicalLinkerController>
        [HttpPost]
        async public Task<object> ParseAndCreateHierarchy([FromBody] ObjectEntity[] objects)
        {
            // List<ObjectEntity> objectsEntities = this._objectsBL.ParseInputAndGetObjectEntities(objects);
            var hierarchy = await Task.Run(() =>
            {
                this._objectsBL.SaveObjectEntities(objects.ToList());
                return this._objectsBL.CreateAndGetHeirarchy();
            });

            return Ok(hierarchy.ToJsonObject());
        }
    }
}
