using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MotorAPI.IServices;
using MotorAPI.Models;
using MotorAPI.Models.Services;
using MotorAPI.Services;
using System;
using System.IO;
using System.Reflection;

namespace MotorAPI
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "MotorAPI", 
                    Version = "v1",
                    Description = "OpenAPI for TestWebApplication",
                    Contact = new OpenApiContact {Name = "Nick Pivanov", Email = "nick_anapa@mail.ru" }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddDbContext<MotorDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient(s => new MotorService<Motor>(s.GetRequiredService<MotorDbContext>()));
            services.AddTransient<IMotorService<Motor>, MotorService<Motor>>();
            services.AddTransient(s => new MotorPropertyService<MotorProperty>(s.GetRequiredService<MotorDbContext>()));
            services.AddTransient<IMotorPropertyService<MotorProperty>, MotorPropertyService<MotorProperty>>();
            services.AddTransient(s => new MeasurementService<Measurement>(s.GetRequiredService<MotorDbContext>()));
            services.AddTransient<IMeasurementService<Measurement>, MeasurementService<Measurement>>();

            services.AddTransient(s => new MotorFactory(s.GetRequiredService<IMotorPropertyService<MotorProperty>>()));
            services.AddTransient<IMotorFactory, MotorFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotorAPI v1"));
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
