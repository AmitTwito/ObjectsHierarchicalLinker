using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ObjectsHierarchyCreator.BE;
using ObjectsHierarchyCreator.PL.Controllers;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.PL.Utilities.Middlewares
{
    public class JsonSerializationErrorCatcherMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JsonSerializationErrorCatcherMiddleware> _logger;

        public JsonSerializationErrorCatcherMiddleware(RequestDelegate next, ILogger<JsonSerializationErrorCatcherMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                _logger.LogError(errorMessage);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var error = new ErrorMessage { Message = errorMessage };
                var json = JsonConvert.SerializeObject(error);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
