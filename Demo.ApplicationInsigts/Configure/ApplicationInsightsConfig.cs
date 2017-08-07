using System;
using System.Collections.Generic;
using System.Text;
using Demo.ApplicationInsigts.Interface;

namespace Demo.ApplicationInsigts.Configure
{
    public class ApplicationInsightsConfig : IApplicaitonInsightsConfig
    {
        public bool Enabled { get; set; }
        public string InstrumentationKey { get; set; }
        public string ApplicationName { get; set; }
        public string Environment { get; set; }
    }
}
