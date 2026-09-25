using JobPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public interface IEmailService
    {
        Task SendEmailJobAsync(IEnumerable<JobPosting> jobs);
    }
}
