using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.DTOs
{
    public class JobPostingDto
    {
        public string ExternalJobId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string PostedDate { get; set; } = string.Empty;
        public string JobUrl { get; set; } = string.Empty;
        public int JobSourceType { get; set; }
    }
}
