using AccountsDepartment.Services;
using DataAccess;
using DataAccess.Models;
using DataAccess.Services;
using HRAgencyAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace HRAgencyAPI
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
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HRAgencyAPI",
                    Version = "v1",
                    Description = "OpenAPI for HR Agency",
                    Contact = new OpenApiContact { Name = "Nick Pivanov", Email = "nick_anapa@mail.ru" }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddDbContext<HRAgencyDBContext>(o => o.UseSqlite(Configuration.GetConnectionString("Default")));

            services.AddTransient(s => new EmployeeService<EmployeeBase>(s.GetRequiredService<HRAgencyDBContext>()));
            services.AddTransient<IEmployeeService<EmployeeBase>, EmployeeService<EmployeeBase>>();

            services.AddTransient(s => new PersonelExpenseService<EmployeeBase>(s.GetRequiredService<HRAgencyDBContext>()));
            services.AddTransient<IPersonelExpenseService<EmployeeBase>, PersonelExpenseService<EmployeeBase>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRAgencyAPI v1"));
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
