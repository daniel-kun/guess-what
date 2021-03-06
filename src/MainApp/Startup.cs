﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Io.GuessWhat.MainApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Db connection services:
            services.Configure<Repositories.Settings>(Configuration);
            services.AddSingleton<Repositories.IChecklistRepository, Repositories.ChecklistRepository>();

            // Configure CloudConvert service:
            services.Configure<Services.Settings>(Configuration);
            services.AddSingleton<Services.ICloudConverterService, Services.CloudConverterService>();

            // Configure Azure Blob Storage service:
            services.AddSingleton<Services.IAzureBlobStorageService, Services.AzureBlobStorageService>();

            // Configure Spam Detection service:
            services.AddSingleton<Services.ISpamDetectionService, Services.SpamDetectionService>();

            services.AddMvc();
            services.AddApplicationInsightsTelemetry(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();
            // Add Application Insights to the request pipeline to track HTTP request telemetry data.
            app.UseApplicationInsightsRequestTelemetry();
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
            // Track data about exceptions from the application. Should be configured after all error handling middleware in the request pipeline.
            app.UseApplicationInsightsExceptionTelemetry();
        }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            builder.AddUserSecrets();

            if (env.IsEnvironment("Development"))
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

    }
}
