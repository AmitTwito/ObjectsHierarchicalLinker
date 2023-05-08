using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ObjectsHierarchyCreator.BL;
using ObjectsHierarchyCreator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ObjectsHierarchyCreator.BE;
using Microsoft.Extensions.Logging.Console;
using ObjectsHierarchyCreator.BE.Utilities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime;
using Microsoft.Extensions.Options;
using ObjectsHierarchyCreator.PL.Utilities.Middlewares;

namespace ObjectsHierarchyCreator.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
            .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new ObjectEntitiesConverter());
             });

            services.AddOptions();


            services.AddSingleton<IAccessControlRepository, AccessControlRepository>();
            services.AddSingleton<IAccessControlService, AccessControlService>();
            services.AddSingleton<IObjectEntitiesRepository, ObjectEntitiesRepository>();
            services.AddScoped<IObjectEntitiesService, ObjectEntitiesService>();
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ObjectsHierarchyCreator", Version = "v1" });
                c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer <TOKEN>`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ObjectsHierarchyCreator v1"));
            }

            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<JsonSerializationErrorCatcherMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
