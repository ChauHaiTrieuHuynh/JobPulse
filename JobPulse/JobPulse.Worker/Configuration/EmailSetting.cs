using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Configuration
{
    public class EmailSetting
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string FromAddress { get; set; } = string.Empty;
    }
}
