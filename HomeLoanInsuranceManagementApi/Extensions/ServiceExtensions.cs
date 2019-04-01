using HomeLoanInsuranceManagementApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace DemoAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        public static void ConfigureValues(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<Settings>(options =>
            {
                options.MongoDBConnectionString = Configuration.GetSection("DBConnections:MongoConnectionString").Value;
                options.SqlServerConnectionString = Configuration.GetSection("DBConnections:SqlServerConnectionString").Value;
                options.NotesDatabase = Configuration.GetSection("DBConnections:NoteDataBase").Value;
                options.EmployeeDatabase = Configuration.GetSection("DBConnections:EmployeeDatabase").Value;
                options.Env = Configuration.GetSection("Enviroment:Value").Value;
               
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }

        public static void ConfigureRequestResponseLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        public static void ConfigureApplicationStartTimeHeaderMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationStartTimeHeaderMiddleWare>();
        }


    }
}
