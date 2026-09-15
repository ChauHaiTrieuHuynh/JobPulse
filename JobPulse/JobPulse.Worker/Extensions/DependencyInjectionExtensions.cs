using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJobPulseServiceDI(this IServiceCollection services)
        {
            return services;
        }

    }
}
