using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System;
using ObjectsHierarchyCreator.BE.Utilities;
using Microsoft.Extensions.Logging;
using ObjectsHierarchyCreator.BL;
using System.Linq;
using Newtonsoft.Json;

namespace ObjectsHierarchyCreator.PL.Utilities.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppConfig _appConfig;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        public AuthenticationMiddleware(RequestDelegate next, IOptions<AppConfig> appSettings, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _appConfig = appSettings.Value;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Starting action {context.Request.Method} {context.Request.Path.Value}");
            _logger.LogInformation($"Checking Authorization");
            if (!context.Request.Headers.ContainsKey("Authorization"))
                await _next(context);
            else
            {
                try
                {
                    _logger.LogInformation($"Validating token..");

                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                    if (token != null)
                        setIsAuthenticatedToContext(context, token);
                    else
                        throw new Exception("Missing access token");

                    await _next(context);
                }
                catch (Exception ex)
                {
                    var errorMessage = "An error occurred while authenticating: " + ex.Message;
                    _logger.LogError(errorMessage);
                    errorMessage = "An error occurred while authenticating: Token could not be validated";
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var error = new ErrorMessage { Message = errorMessage };
                    var json = JsonConvert.SerializeObject(error);

                    await context.Response.WriteAsync(json);
                }
            }
        }

        private void setIsAuthenticatedToContext(HttpContext context, string token)
        {

            // Validate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig.JWTKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            context.Items["Authenticated"] = jwtToken != null;

        }
    }
}
