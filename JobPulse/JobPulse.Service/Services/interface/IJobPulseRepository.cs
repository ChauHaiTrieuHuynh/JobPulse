using JobPulse.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public interface IJobPulseRepository
    {
        Task<IEnumerable<JobPostingDto>> GetLinkedInJobPostingClient();

    }
}
