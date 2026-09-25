using JobPulse.Data.Models;
using JobPulse.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public interface IJobPulseService
    {
        Task<IEnumerable<JobPostingDto>> GetLinkedInJobPostingClient();
        Task<IEnumerable<JobPosting>> FilterNewJobsAsync(IEnumerable<JobPostingDto> linkedInJobsPosting, IEnumerable<JobPostingDto> indeedJobPosting);
        Task SaveJobsAsync(IEnumerable<JobPosting> jobs);
    }
}
