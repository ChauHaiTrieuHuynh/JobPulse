using JobPulse.Service.Settings;
using JobPulse.Worker.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Extensions
{
    public static class JobSourceExtensions
    {
        public static IServiceCollection AddJobSourceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LinkedInSettings>(configuration.GetSection("JobSources:LinkedIn"));

            return services;
        }
    }
}
