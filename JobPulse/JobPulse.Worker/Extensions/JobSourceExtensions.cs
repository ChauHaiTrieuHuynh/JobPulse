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
            return services.Configure<JobSourceSetting>(
               configuration.GetSection("JobSources"));     
        }
    }
}
