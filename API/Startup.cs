using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance;
using Core.Repositories;
using Core;
using Core.Services;
using API.Middleware;
using API.Errors;
using AutoMapper;
using API.Helpers;
using API.Hubs;
using Hangfire;
using API.Jobs;

namespace API
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
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
            services.AddHangfireServer();

            // Add the processing server as IHostedService


            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllows",
                                  builder =>
                                  {
                                      builder.WithHeaders("*");
                                      builder.WithMethods("*");
                                      builder.WithOrigins("http://localhost:3000");
                                      builder.AllowCredentials();
                                  });
            });
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDataProtection();
            services.AddDbContext<mmiletaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("mmiletaDatabase")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMemoryRepository, MemoryRepository>();
            services.AddTransient<ICheckActivityJob, CheckActivityJob>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IBackgroundJobService, BackgroundJobService>();

            services.AddScoped<INotificationHub, NotificationHub>();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddAuthentication("mmiletaAuthCookieScheme")
                    .AddCookie("mmiletaAuthCookieScheme");

            services.AddSignalR();


            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobService backgroundJobService, ICheckActivityJob job)
        {

            // if (env.IsDevelopment())
            //  {
            //     app.UseDeveloperExceptionPage();
            //   }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            // app.UseHttpsRedirection();
            app.UseCors("MyAllows");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard();

            app.UseMiddleware<AddLastActivityTimeStampMiddleware>();

            backgroundJobService.ScheduleRecurringJob(
                () => job.CheckActivity(),
                "*/2 * * * *"
                );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessagingHub>("/messaginghub");
                endpoints.MapHub<NotificationHub>("/notificationhub");
            });
        }
    }
}
