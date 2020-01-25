using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        // Using constructor Injection to call IConfiguration to get data from appsettigns.json
        private IConfiguration config;
        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Default code
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync(this.config.GetValue<string>("RandomUrl"));
            //    });
            //});

            // Middleware 1
            app.Use(async (context, next) =>
            {
                logger.LogInformation("MiddleWare 1: Request");
                await next();
                logger.LogInformation("MiddleWare 1: Response");
            });


            // Middleware 2
            app.Use(async (context, next) =>
            {
                logger.LogInformation("MiddleWare 2: Request");
                await next();
                logger.LogInformation("MiddleWare 2: Response");
            });

            //Middleware 3
            app.Run(async (context) =>
            {
                logger.LogInformation("MiddleWare 3: Request");
                await context.Response.WriteAsync("MiddleWare 3: Final Response");
                logger.LogInformation("MiddleWare 3: Response");
            });

            //EmployeeManagement.Startup: Information: MiddleWare 1: Request
            //EmployeeManagement.Startup: Information: MiddleWare 2: Request
            //EmployeeManagement.Startup: Information: MiddleWare 3: Request
            //EmployeeManagement.Startup: Information: MiddleWare 3: Response
            //EmployeeManagement.Startup: Information: MiddleWare 2: Response
            //EmployeeManagement.Startup: Information: MiddleWare 1: Response
        }
    }
}
