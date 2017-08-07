﻿using System;
using System.Collections.Generic;
using System.Text;
using Demo.ApplicationInsigts.Interface;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Demo.ApplicationInsigts.Configure
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
