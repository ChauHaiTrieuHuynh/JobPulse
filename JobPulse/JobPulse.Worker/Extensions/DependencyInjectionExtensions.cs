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
            return services;
        }

    }
}
