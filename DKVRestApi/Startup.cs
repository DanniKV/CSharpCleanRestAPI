using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CustomerApp.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using CustomerApp.Core.Entity;
using Newtonsoft.Json;

namespace DKVRestApi
{
    public class Startup
    {


        /*
        //For offline Database
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
            //FakeDB.Initialize();
        }
        */

        private IConfiguration _conf { get; }

        private IHostingEnvironment _env { get; set; }

        //For online Database / Azure
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _conf = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            //In-Memory Database FakeSQL DB
            services.AddDbContext<CustomerAppContext>(
                opt => opt.UseInMemoryDatabase("FakeSQL-DB"));
                */

            if (_env.IsDevelopment())
            {
                services.AddDbContext<CustomerAppContext>(
                opt => opt.UseSqlite("Data Source=CustomerApp.db"));
            }
            //For SQLite DB.. Needs actual lists and tables
            //ConnectionString fra Azure (Online)
            else if (_env.IsProduction())
            {
                services.AddDbContext<CustomerAppContext>(
                    opt => opt.UseSqlServer(_conf.GetConnectionString("defaultConnection")));
            }

            //dependency injection
            //Copied from Program 
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();

            //For Ignoring Loop References
            services.AddMvc().AddJsonOptions(Options => {
                Options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            /* For Cors Options
            services.AddCors(options =>
            //AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder
                .WithOrigins("http:/localhost:5000").AllowAnyHeader().AllowAnyMethod()
                .WithOrigins(" https://shopappdkv.firebaseapp.com").AllowAnyHeader().AllowAnyMethod()
                )); 
            ; */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //for database on startup
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<CustomerAppContext>();

                    DBInitializer.SeedDB(ctx);
                }

            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<CustomerAppContext>();
                    ctx.Database.EnsureCreated();
                }
                app.UseHsts();
            }
            //Cors
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
