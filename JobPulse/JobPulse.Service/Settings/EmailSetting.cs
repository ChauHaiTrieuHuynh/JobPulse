using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Settings
{
    public class EmailSetting
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
