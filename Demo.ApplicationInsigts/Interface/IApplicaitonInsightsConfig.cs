namespace Demo.ApplicationInsigts.Interface
{
    public interface IApplicaitonInsightsConfig
    {
        bool Enabled { get; set; }
        string InstrumentationKey { get; set; }
        string ApplicationName { get; set; }
        string Environment { get; set; }
    }
}
