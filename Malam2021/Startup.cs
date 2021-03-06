using Malam2021.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Startup.Configuration = configuration;
        }
        // !! static IConfiguration Configuration { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(o => o.AddPolicy("CustomCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin();// WithOrigins("http://localhost:4200");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));
            services.AddControllersWithViews();
            services.AddSingleton<IDataAccessService, DataMokService>();


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Malam2021",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "andrey1de@gmail.com",
                        Name = "Dergachev Andrey @2021"

                    }
                });
            });
        }
        public static IWebHostEnvironment Environment {get;private set;}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Environment = env;
            app.UseCors("CustomCorsPolicy");
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Malam2021 v1"));
            //}
            app.UseHttpsRedirection();
            app.UseDefaultFiles();

            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
