using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.API.Context;

namespace SmartSchool.API
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
            services.AddDbContext<SmartContext>(
                context => context.UseMySql(Configuration.GetConnectionString("MySqlConnection"))
                );

            services.AddControllers()
                    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
                {
                    foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(
                            description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                            {
                                Title = "SmartSchool API",
                                Version = description.ApiVersion.ToString(),

                                License = new Microsoft.OpenApi.Models.OpenApiLicense
                                {
                                    Name = "SmartSchool License",
                                    Url = new Uri("http://mit.com")
                                },
                                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                                {
                                    Name = "Matheusss Ferreira",
                                    Email = "",
                                    Url = new Uri("http://programadamente.com")
                                }
                            });
                    }

                    var xmlCOmentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlCOmentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCOmentsFile);

                    options.IncludeXmlComments(xmlCOmentsFullPath);
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    await context.Response.WriteAsync("<html><body>\r\n");
                    await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    //if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    //{
                    await context.Response.WriteAsync($"{exceptionHandlerPathFeature?.Error.Message}<br><br>\r\n");
                    await context.Response.WriteAsync($"{exceptionHandlerPathFeature?.Error.StackTrace}<br><br>\r\n");
                    //}

                    await context.Response.WriteAsync("</body></html>\r\n");
                    await context.Response.WriteAsync(new string(' ', 512));
                });
            });

            app.UseRouting();
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                    options.RoutePrefix = "";
                });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
