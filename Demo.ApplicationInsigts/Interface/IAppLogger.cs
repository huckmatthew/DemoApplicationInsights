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

        void LogDebug(string eventName, string category, dynamic properties, string authenticatedUserId);
        void LogInfo(string eventName, string category, dynamic properties, string authenticatedUserId);
        void LogWarn(string eventName, string category, dynamic properties, string authenticatedUserId);
        string LogExecption(Exception ex, string category, dynamic properties, string authenticatedUserId);
        void LogError(string eventName, string category, dynamic properties, string authenticatedUserId);
        void LogFatal(string eventName, string category, dynamic properties, string authenticatedUserId);
        void TrackMetric(string eventName, LogLevel level, string category, dynamic properties, string authenticatedUserId);
        void TrackRequest(string eventName, DateTimeOffset startTime, TimeSpan duration, bool sucess, dynamic properties, string requestId);
        AppInsightsTimer TrackTimer(string userValue, dynamic properties, string timerId);
        AppInsightsTimer TrackTimer(string userValue);

    }
}
