using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public interface IJobProcessingService
    {
        Task ProcessJobAsync();
    }
}
