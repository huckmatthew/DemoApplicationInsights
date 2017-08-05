using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ApplicationInsigts.Interface
{
    public interface IAppLogger
    {
        TelemetryClient AppLogger { get; }
        void Flush();
        void TrackEvent(string eventName, LogLevel level, string category, dynamic properties, string authenticatedUserId);
        void TrackMetric(string eventName, LogLevel level, string category, dynamic properties, string authenticatedUserId);
        void TrackRequest(string eventName, DateTimeOffset startTime, TimeSpan duration, bool sucess, dynamic properties, string requestId);
        string TrackException(Exception ex, dynamic properties, string authenticatedUserId);
        AppInsightsTimer TrackTimer(string userValue, dynamic properties, string timerId);
        AppInsightsTimer TrackTimer(string userValue);

    }
}
