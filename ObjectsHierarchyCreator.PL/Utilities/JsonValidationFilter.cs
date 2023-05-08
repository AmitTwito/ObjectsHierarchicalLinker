using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using ObjectsHierarchyCreator.BE;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ObjectsHierarchyCreator.PL.Utilities
{
    public class JsonValidationFilter : IActionFilter, IOrderedFilter
    {
        // For setting the order of executing filters.
        // it will run after most other filters, allowing it to catch any exceptions thrown during JSON deserialization.
        public int Order => int.MaxValue - 10;
        public readonly ILogger<JsonValidationFilter> _logger;
        public JsonValidationFilter(ILogger<JsonValidationFilter> logger)
        {
            _logger = logger;
        }
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                Converters = { new ObjectEntitiesConverter() }
            };


            try
            {
                var requestBody = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();



                var objects = JsonSerializer.Deserialize<List<ObjectEntity>>(requestBody, jsonOptions);
                context.ActionArguments["objects"] = objects;
            }
            catch (JsonException ex)
            {
                var errorMessage = ex.Message;
                _logger.LogError(errorMessage);

                var error = new ErrorMessage { Message = errorMessage };

                context.Result = new BadRequestObjectResult(error);
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No operation
        }
    }
}
