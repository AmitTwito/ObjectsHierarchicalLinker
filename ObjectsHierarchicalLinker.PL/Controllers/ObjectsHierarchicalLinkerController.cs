using Microsoft.AspNetCore.Mvc;
using ObjectsHierarchicalLinker.BE;
using ObjectsHierarchicalLinker.BL;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public object SaveAndLink([FromBody] ObjectModel[] objectModels)
        {
            return this._objectsBL.SaveObjectsAndGetLinkedHeirarchy(objectModels);
        }
    }
}
