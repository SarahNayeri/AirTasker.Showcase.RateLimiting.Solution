using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirTasker.Showcase.RateLimit.Authorization;
using AirTasker.Showcase.RateLimit.DataAccess;
using AirTasker.Showcase.RateLimit.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirTasker.Showcase.RateLimit
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
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<IAuthorizationHandler, RateLimitHandler>();
            services.AddSingleton<IRateLimitService, RateLimitService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RateLimitPolicy", policy =>
                    policy.Requirements.Add(new RateLimitRequirement(Constant.MaxRate, Constant.IntervalInSecond)));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "RateLimitScheme";
                options.AddScheme<RateLimitSchemeHandler>("RateLimitScheme", "RateLimitScheme");
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}