using JobPulse.Data.Repositories;
using JobPulse.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJobPulseServiceDI(this IServiceCollection services)
        {
            services.AddScoped(
                typeof(IDataRepository<>),
                typeof(DataRepository<>));

            services.AddScoped<IJobSource, LinkedInJobSource>();
            services.AddScoped<IJobProcessingService, JobProcessingService>();

            //fetch job using addHttpClient to set user agent to avoid 403 error
            services.AddHttpClient<IJobSource, LinkedInJobSource>(client =>
            {
                //add default header to avoid refuse
                client.DefaultRequestHeaders.UserAgent.ParseAdd(
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/152.0.0.0 Safari/537.36");
            });

            services.AddScoped<IJobPulseService, JobPulseService>();
            return services;
        }

    }
}
