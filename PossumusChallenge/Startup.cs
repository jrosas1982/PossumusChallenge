using Core.Infraestructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using API.PossumusChallenge.Filters;
using API.PossumusChallenge.Services;

namespace PossumusChallenge
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
            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PossumusChallenge",
                    Description = "CRUD de Productos y Categorias"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                        },
                        new List<string>()
                    }
                });
                config.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Ingresar tu ApiKey de integracion:",
                    Name = ApiKeyAuth.ApiKeyHeaderName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
            services.AddServices();
            services.AddControllers().AddNewtonsoftJson(x =>
                            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")), ServiceLifetime.Transient);
            services.AddCoreServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "API.PossumusChallenge v1"));

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
