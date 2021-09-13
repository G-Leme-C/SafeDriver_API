using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SafeDriver.Domain.Data;
using AutoMapper;
using SafeDriver.API.MapProfiles;

namespace SafeDriver.API
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
            ConfigureDbContext(services);
            ConfigureSwaggerService(services);
            ConfigureAutoMapping(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SafeDriverAPI");
                c.RoutePrefix = string.Empty;
            });

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
    
        public void ConfigureSwaggerService(IServiceCollection services) {
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() {
                    Title = "SafeDriver API for SafeDriver App",
                    Version = "v1"
                });
            });
        }

        public void ConfigureDbContext(IServiceCollection services) {
            services.AddDbContext<SafeDriverDbContext>(options => {
                options
                .UseNpgsql(Configuration.GetConnectionString("DefaultSafeDriverDatabase"))
                .UseSnakeCaseNamingConvention();
            });
        }

        public void ConfigureAutoMapping(IServiceCollection services) {
            services.AddAutoMapper(typeof(DriverProfile));
        }
    }
}
