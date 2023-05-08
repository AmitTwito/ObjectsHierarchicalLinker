using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using ObjectsHierarchyCreator.BE.AccessControl;
using ObjectsHierarchyCreator.PL.Utilities;
using System;
using System.Linq;
using System.Net;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeFilter : Attribute, IAuthorizationFilter
{
    public readonly ILogger<AuthorizeFilter> _logger;
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;
        var isAuthenticated = httpContext.Items["Authenticated"];
        if (!httpContext.Request.Headers.ContainsKey("Authorization") || isAuthenticated == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized - Missing Token" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else if (!(bool)isAuthenticated)
            context.Result = new JsonResult(new { message = "Unauthorized - Invalid token" }) { StatusCode = StatusCodes.Status401Unauthorized };

    }
}