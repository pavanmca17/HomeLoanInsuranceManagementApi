using HomeLoanInsuranceManagementApi.Middleware;
using HomeLoanInsuranceManagementApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HomeLoanInsuranceManagementApi.Extensions
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
                options.MongoDatabase = Configuration.GetSection("DBConnections:MongoDataBase").Value;               
                options.Env = Configuration.GetSection("Enviroment:Value").Value;
                options.RequestResponseLogging = Convert.ToBoolean(Configuration.GetSection("RequestResponseLogging").Value);


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

    }
}
