using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HomeLoanInsuranceManagementApi
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

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var Issues = new CustomBadRequest(context);

                        return new BadRequestObjectResult(Issues);
                    };
                });

                // api versioning
                services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));

                services.AddHttpClient(Configuration);

                services.ConfigureValues(Configuration);

                services.AddDBContext<ProductContext>(Configuration);

                services.AddTransient<INoteRepository, NoteRepository>();
                services.AddTransient<IProductRepository, ProductRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();

                services.AddCors(policy =>
                {
                    policy.AddPolicy("CorsPolicy", options => options.AllowAnyHeader()
                                                                     .AllowAnyMethod()
                                                                     .AllowAnyOrigin());
                });

                //services.AddDistributedRedisCache(options =>
                //{
                //    options.InstanceName = Configuration.GetSection("RedisCacheDetails:HostName").Value;
                //    options.Configuration =  Configuration.GetSection("RedisCacheDetails:ConnectionString").Value;
                //});


            }

            // This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDistributedCache distributedCache)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                if (env.IsStaging())
                {

                }

                if (env.IsProduction())
                {

                }

                //string appStartTimeKey = "app-Last-Start-Time";
                //var serverStartTimeString = DateTime.Now.ToString();
                //byte[] val = Encoding.UTF8.GetBytes(serverStartTimeString);
                //distributedCache.SetAsync(appStartTimeKey, val);
                //app.ConfigureApplicationStartTimeHeaderMiddleWare();
                app.ConfigureRequestResponseLoggingMiddleware();
                app.ConfigureCustomExceptionMiddleware();

                app.UseCors("CorsPolicy");
                app.UseMvc();


            }
        }
    }
