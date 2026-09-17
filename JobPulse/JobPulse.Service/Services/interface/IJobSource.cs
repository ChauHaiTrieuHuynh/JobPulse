using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public interface IJobSource
    {
        Task<string> FetchJobAsync();
    }
}
