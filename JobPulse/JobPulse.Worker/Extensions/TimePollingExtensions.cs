using JobPulse.Worker.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Extensions
{
    public static class TimePollingExtensions
    {
        public static IServiceCollection AddTimePollingService(this IServiceCollection services) { 
            return services.Configure<JobPollingSetting>(options =>
            {
                options.IntervalMinutes = 60;
            });
        }
    }
}
