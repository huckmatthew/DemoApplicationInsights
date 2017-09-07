

namespace Demo.ApplicationInsights.Interface
{
    public interface IApplicaitonInsightsConfig
    {
        bool Enabled { get; set; }
        LogLevel LoggingLevel { get; set; }
        string InstrumentationKey { get; set; }
        string ApplicationName { get; set; }
        string Environment { get; set; }
    }
}
