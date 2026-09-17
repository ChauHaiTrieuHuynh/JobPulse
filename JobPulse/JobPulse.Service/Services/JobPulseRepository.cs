using JobPulse.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public class JobPulseRepository : IJobPulseRepository
    {
        public Task<IEnumerable<JobPostingDto>> GetJobPostingClient()
        {
            throw new NotImplementedException();
        }
    }
}
