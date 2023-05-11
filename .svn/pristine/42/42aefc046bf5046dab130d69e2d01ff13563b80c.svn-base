using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using eSyaMCServices.WebAPI.Utility;
using Newtonsoft.Json.Serialization;
using DL_eSyaMCServices = eSyaMCServices.DL.Entities;
using eSyaMCServices.IF;
using eSyaMCServices.DL.Repository;

namespace eSyaMCServices.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<ISpecialtyUnitsRepository, SpecialtyUnitsRepository>();
            services.AddScoped<ICommonDataRepository, CommonDataRepository>();
            DL_eSyaMCServices.eSyaEnterprise._connString = Configuration.GetConnectionString("dbConn_eSyaEnterprise");

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApikeyAuthAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //for cross origin support
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            //For displying response same as model property case avoid camel case
            services
           .AddMvc()
           .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes
                    .MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                    .MapRoute(name: "api", template: "api/{controller}/{action}/{id?}");
            });

            app.UseMvc();
        }
    }
}
