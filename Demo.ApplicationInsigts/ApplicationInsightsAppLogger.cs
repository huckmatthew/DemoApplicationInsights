using System;
using System.Dynamic;
using System.Reflection;
using Demo.ApplicationInsigts.Interface;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Demo.ApplicationInsigts
{
    public class ApplicationInsightsAppLogger : IAppLogger
    {
        static ApplicationInsightsAppLogger instance;
        private readonly TelemetryClient _telemetry;
        public TelemetryClient AppLogger
        {
            get { return _telemetry; }
        }
        public static ApplicationInsightsAppLogger GetLogger()
        {
            if (instance == null)
                instance = new ApplicationInsightsAppLogger();
            return instance;
        }

        private ApplicationInsightsAppLogger()
        {
            _telemetry = new TelemetryClient();
        }

        public void Flush()
        {
            if (_telemetry != null)
            {
                _telemetry.Flush();
            }
        }

        public void LogDebug(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            TrackEvent("DEBUG", eventName, category, properties, authenticatedUserId);
        }

        public void LogInfo(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            TrackEvent("INFO", eventName, category, properties, authenticatedUserId);
        }

        public void LogWarn(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            TrackEvent("WARN", eventName, category, properties, authenticatedUserId);

        }

        public string LogExecption(Exception ex, string category, dynamic properties, string authenticatedUserId)
        {
            return TrackException(ex, category, properties, authenticatedUserId);
        }

        public void LogError(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            TrackEvent("ERROR", eventName, category, properties, authenticatedUserId);

        }

        public void LogFatal(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            throw new NotImplementedException();
        }

        public void TrackMetric(string eventName, LogLevel level, string category, dynamic properties, string authenticatedUserId)
        {
            var matt = new TelemetryContext();
            var metricInfo = new MetricTelemetry();
            metricInfo.Name = eventName;
            metricInfo.Properties["Level"] = level.ToString();
            metricInfo.Properties["Category"] = category;
            if (properties != null)
            {
                var props = properties.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    metricInfo.Properties[p.Name] = p.GetValue(properties, null).ToString();
                }
            }
            _telemetry.TrackMetric(metricInfo);
        }

        public void TrackRequest(string eventName, DateTimeOffset startTime, TimeSpan duration, bool sucess, dynamic properties, string requestID)
        {
            var context = new RequestTelemetry();
            context.Name = eventName;
            context.Duration = duration;
            if (!string.IsNullOrWhiteSpace(requestID))
            {
                context.Id = requestID;
            }
            if (properties != null)
            {
                var props = properties.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    context.Properties[p.Name] = p.GetValue(properties, null).ToString();
                }
            }
            _telemetry.TrackRequest(context);// eventName, DateTimeOffset.Now, elapsed, "200", sucess);
        }

        public AppInsightsTimer TrackTimer(string userValue)
        {
            return new AppInsightsTimer(this, userValue, null, null);
        }

        public AppInsightsTimer TrackTimer(string userValue, dynamic properties, string timerID)
        {
            return new AppInsightsTimer(this, userValue, properties, timerID);
        }

        private void TrackEvent(string level, string eventName, string category, dynamic properties, string authenticatedUserId = null)
        {
            var eventToSave = new EventTelemetry();
            eventToSave.Name = eventName;
            eventToSave.Properties["Level"] = level;
            eventToSave.Properties["Category"] = category;
            if (properties != null)
            {
                var props = properties.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    eventToSave.Properties[p.Name] = p.GetValue(properties, null).ToString();
                }
            }
            if (!string.IsNullOrEmpty(authenticatedUserId))
            {
                eventToSave.Context.User.AuthenticatedUserId = authenticatedUserId;
            }
            _telemetry.TrackEvent(eventToSave);
        }

        private string TrackException(Exception ex, string requestID, dynamic properties, string authenticatedUserId = null)
        {
            var exceptionToSave = new ExceptionTelemetry();
            exceptionToSave.Exception = ex;
            string errorId = Guid.NewGuid().ToString();
            if (!string.IsNullOrWhiteSpace(requestID))
            {
                exceptionToSave.ProblemId = requestID;
            }
            exceptionToSave.Properties["ErrorId"] = errorId;
            exceptionToSave.Properties["ErrorMessage"] = ex.Message;
            if (properties != null)
            {
                var props = properties.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    exceptionToSave.Properties[p.Name] = p.GetValue(properties, null).ToString();
                }
            }
            if (!string.IsNullOrEmpty(authenticatedUserId))
            {
                exceptionToSave.Context.User.AuthenticatedUserId = authenticatedUserId;
            }
            _telemetry.TrackException(exceptionToSave);
            return errorId;
        }

    }
}