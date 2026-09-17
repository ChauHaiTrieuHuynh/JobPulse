using JobPulse.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Workers
{
    public class FetchJobWorker : BackgroundService
    {
        private readonly IJobSource _jobSource;
        private readonly ILogger<FetchJobWorker> _logger;
        public FetchJobWorker(IJobSource jobSource, ILogger<FetchJobWorker> logger)
        {
            _jobSource = jobSource;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    string html = await _jobSource.FetchJobAsync();
                    _logger.LogInformation(
                                "LinkedIn response received. HTML: {html}",
                                html);

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
