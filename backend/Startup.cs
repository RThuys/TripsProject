using System;

using backend.Data.Models;
using backend.Data.Repositories;
using backend.Data.Repositories.Interfaces;
using backend.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    });

                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IChildRepository, ChildRepository>();
            services.AddTransient<ISupervisorRepository, SupervisorRepository>();
            services.AddTransient<ITripChildRepository, TripChildRepository>();

            services.AddMvc().AddJsonOptions(options =>
            {
                // This is needed for referencing a many to many relationship without getting in an endless loop when returning items with an api call
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            }); ;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
