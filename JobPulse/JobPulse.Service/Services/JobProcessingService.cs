using JobPulse.Service.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public class JobProcessingService : IJobProcessingService
    {
        private readonly IJobPulseService _jobPulseService;
        private readonly ILogger _logger;

        public JobProcessingService(IJobPulseService jobPulseService, ILogger<JobProcessingService> logger)
        {
            _jobPulseService = jobPulseService;
            _logger = logger;

        }
        public async Task ProcessJobAsync()
        {
            try
            {
                // 1. Getting LinkedIn Job
                IEnumerable<JobPostingDto> linkedInJobList = await _jobPulseService.GetLinkedInJobPostingClient();
                IEnumerable<JobPostingDto> indeedJobList = new List<JobPostingDto>();

                // 2. Remove duplicate with db
                var newListJobs = await _jobPulseService.FilterNewJobsAsync(linkedInJobList, indeedJobList);

                _logger.LogInformation("newListJob {newListJobs}", newListJobs);
                // 3. Save the job

                // 4. Email to mobile device

            }
            catch (Exception)
            {
                _logger.LogError("Error in processing job service");
            }
        }
    }
}
