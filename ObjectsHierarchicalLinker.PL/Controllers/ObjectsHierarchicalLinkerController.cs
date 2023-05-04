﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObjectsHierarchicalLinker.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectsHierarchicalLinkerController : ControllerBase
    {
        // GET: api/<ObjectsHierarchicalLinkerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
