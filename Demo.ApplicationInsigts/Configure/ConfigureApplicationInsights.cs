using System;
using System.Collections.Generic;
using System.Text;
using Demo.ApplicationInsigts.Interface;
using Microsoft.ApplicationInsights.Extensibility;

namespace Demo.ApplicationInsigts.Configure
{
    public static class ConfigureApplicationInsights
    {
        public static void Config(IApplicaitonInsightsConfig config)
        {
            TelemetryConfiguration.Active.InstrumentationKey = config.InstrumentationKey;
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new ApplicationInsightsContextConfig(config));
        }
    }
}
