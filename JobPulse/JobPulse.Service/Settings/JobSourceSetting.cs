using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Settings
{
    public class LinkedInSettings
    {
        public string BaseUrl { get; set; } = string.Empty;

        public List<string> Keywords { get; set; } = new();

        public string Location { get; set; } = string.Empty;

        public string GeoId { get; set; } = string.Empty;

        public string TimeRange { get; set; } = string.Empty;
    }

    public class IndeedSettings
    {

    }
}
