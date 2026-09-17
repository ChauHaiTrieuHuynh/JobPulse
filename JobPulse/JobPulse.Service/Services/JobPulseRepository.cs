using JobPulse.Service.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public class JobPulseRepository : IJobPulseRepository
    {
        private readonly IJobSource _jobSource;
        private readonly ILogger _logger;

        public JobPulseRepository(IJobSource jobSource, ILogger logger)
        {
            _jobSource = jobSource;
            _logger = logger;
        }
        public Task<IEnumerable<JobPostingDto>> GetJobPostingClient()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Job Source ");
                //sending warning email later
                //To Do List
                throw;
            }
        }
    }
}
