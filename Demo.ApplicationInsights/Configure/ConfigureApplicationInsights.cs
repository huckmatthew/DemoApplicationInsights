using System;
using Demo.ApplicationInsights.Interface;
using Microsoft.ApplicationInsights.Extensibility;

namespace Demo.ApplicationInsights.Configure
{
    public static class ConfigureApplicationInsights
    {
        public static IApplicaitonInsightsConfig ApplicationInsightsConfig { get; private set; }

        public static void Config(IApplicaitonInsightsConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            ApplicationInsightsConfig = config;

            TelemetryConfiguration.Active.InstrumentationKey = config.InstrumentationKey;
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new ApplicationInsightsContextConfig(config));
        }
    }
}
