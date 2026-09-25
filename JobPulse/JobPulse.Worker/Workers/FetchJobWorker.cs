using JobPulse.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Workers
{
    public class FetchJobWorker : BackgroundService
    {
        //factory used to create a DI new scope
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FetchJobWorker> _logger;
        public FetchJobWorker(IServiceScopeFactory scopeFactory, ILogger<FetchJobWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (IServiceScope scope = _scopeFactory.CreateScope())
                    {
                        IJobProcessingService jobProcess =
                            scope.ServiceProvider.GetRequiredService<IJobProcessingService>();

                        await jobProcess.ProcessJobAsync();
                    } // Auto dispose after 'using'

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching job data");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(60),
                    stoppingToken);
            }
        }
    }
}
