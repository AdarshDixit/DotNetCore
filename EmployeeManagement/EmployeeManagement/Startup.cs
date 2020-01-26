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
            // Adding mvc services
            services.AddMvc(mvcOptions => mvcOptions.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Route not found!!!");
            });


            //Inject ILogger<Startup> logger in Configure()
            //// Middleware 1
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MiddleWare 1: Request");
            //    await next();
            //    logger.LogInformation("MiddleWare 1: Response");
            //});


            //// Middleware 2
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MiddleWare 2: Request");
            //    await next();
            //    logger.LogInformation("MiddleWare 2: Response");
            //});

            ////Middleware 3
            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("MiddleWare 3: Request");
            //    await context.Response.WriteAsync("MiddleWare 3: Final Response");
            //    logger.LogInformation("MiddleWare 3: Response");
            //});

            //EmployeeManagement.Startup: Information: MiddleWare 1: Request
            //EmployeeManagement.Startup: Information: MiddleWare 2: Request
            //EmployeeManagement.Startup: Information: MiddleWare 3: Request
            //EmployeeManagement.Startup: Information: MiddleWare 3: Response
            //EmployeeManagement.Startup: Information: MiddleWare 2: Response
            //EmployeeManagement.Startup: Information: MiddleWare 1: Response

            // Adding default middleware (will land on Default.html)
            //app.UseDefaultFiles();

            // Modifying the default middleware
            //var defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("Foo.html");
            //app.UseDefaultFiles(defaultFilesOptions);

            // Adding static files from wwwroot (https://localhost:44380/images/Image.jpg)
            //app.UseStaticFiles();

            // Adding file server middleware to replace UseDefaultFiles and UseStaticFiles
            //app.UseFileServer();
        }
    }
}
