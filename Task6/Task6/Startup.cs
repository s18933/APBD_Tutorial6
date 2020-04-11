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
using Microsoft.OpenApi.Models;
using Task6.MiddleWare;
using Task6.Services;

namespace Task6
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
            services.AddTransient<IDbService, SqlServerDbService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService dbService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           
            app.UseMiddleware<CustomLoginMiddleware>();
            app.Use(async (context, next) =>
             {
                 if (!context.Request.Headers.ContainsKey("Index"))
                 {
                     context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                     await context.Response.WriteAsync("Index number required!");
                     return;
                 }
                 else
                 {
                     string index = context.Request.Headers["Index"].ToString();
                     var existStudent = dbService.GetStudentByIndex(index);
                     if (existStudent == 0)
                     {
                         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                         await context.Response.WriteAsync("Student does not exist in the database");
                         return;
                     }
                 }
                 await next();
             });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
