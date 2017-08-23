using Demo.ApplicationInsights.Interface;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Demo.ApplicationInsights.Configure
{
    public class ApplicationInsightsContextConfig : Microsoft.ApplicationInsights.Extensibility.ITelemetryInitializer
    {
        //private string _applicationName;
        //private string _environment;
        private IApplicaitonInsightsConfig _config;
        public ApplicationInsightsContextConfig(IApplicaitonInsightsConfig config)
        {
            _config = config;
        }

        void ITelemetryInitializer.Initialize(ITelemetry telemetry)
        {
            if (_config.Enabled)
            {
                telemetry.Context.Properties["ApplicationName"] = _config.ApplicationName;
                telemetry.Context.Properties["Environment"] = _config.Environment;
            }
        }
    }
}
