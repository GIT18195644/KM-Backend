using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DistributionPortal.Data;
using DistributionPortal.Data.Entities;

namespace DistributionPortal
{
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly IHostingEnvironment env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            config = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddCookie().AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = config["Tokens:Issuer"],
                    ValidAudience = config["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]))
                };
            });

            services.AddMvc(opt =>
            {
                if (env.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            .AddJsonOptions(options =>
            {
                var resolver = options.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    (resolver as DefaultContractResolver).NamingStrategy = null;
                }
            });

            //dependency injection
            services.AddDbContext<DPDBContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DistributionPortalConnectionString")));

            services.AddScoped<IDPRepository, DPRepository>();

            services.AddTransient<DPSeeder>();

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<UserDetails, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;

            }).AddEntityFrameworkStores<DPDBContext>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // other code remove for clarity 
            loggerFactory.AddFile("Logs/KMPortal_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<DPSeeder>();
                    seeder.Seed().Wait();
                }
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<DPSeeder>();
                    seeder.Seed().Wait();
                }
            }

            app.UseAuthentication();

            //Configure cors
            app.UseCors(options =>
                   options.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                );

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{Controller=Login}/{action=Index}/{id}");
            });
        }


    }
}
