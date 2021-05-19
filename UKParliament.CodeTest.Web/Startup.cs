using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web
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
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<PersonRepoActionResult>();
            services.AddTransient<RoomRepoActionResult>();
            services.AddTransient<RoomBookingRepoActionResult>();
            services.AddControllers();

            services.AddDbContext<RoomBookingsContext>(op => op.UseInMemoryDatabase("RoomBookings"));
            
            services.AddSwaggerGen();
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assembles = new List<Assembly>()
            {
                typeof(RoomBookingsContext).Assembly,
                typeof(IRoomService).Assembly,
                typeof(IPersonDataStoreRepo).Assembly,
                typeof(IRoomService).Assembly,
                typeof(IBookingsService).Assembly,
                typeof(IBookingDataStoreRepo).Assembly
            };

            foreach (var assembly in assembles)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

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
