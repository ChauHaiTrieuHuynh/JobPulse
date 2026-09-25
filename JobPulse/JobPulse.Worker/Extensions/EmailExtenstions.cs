using JobPulse.Service.Settings;
using JobPulse.Worker.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Extensions
{
    public static class EmailExtenstions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<EmailSetting>(
               configuration.GetSection("Email"));
        }
    }
}
