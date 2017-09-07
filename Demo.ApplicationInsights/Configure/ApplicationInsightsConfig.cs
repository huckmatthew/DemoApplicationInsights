using Demo.ApplicationInsights.Interface;


namespace Demo.ApplicationInsights.Configure
{
    public class ApplicationInsightsConfig : IApplicaitonInsightsConfig
    {
        public bool Enabled { get; set; }
        public LogLevel LoggingLevel { get; set; }
        public string InstrumentationKey { get; set; }
        public string ApplicationName { get; set; }
        public string Environment { get; set; }
    }
}
