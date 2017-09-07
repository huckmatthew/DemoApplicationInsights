using System;
using System.Reflection;
using Demo.ApplicationInsights.Configure;
using Demo.ApplicationInsights.Interface;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Demo.ApplicationInsights
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
            if (ConfigureApplicationInsights.ApplicationInsightsConfig.Enabled &&
                ConfigureApplicationInsights.ApplicationInsightsConfig.LoggingLevel >= LogLevel.Debug)
            {
                TrackEvent(LogLevel.Debug.ToString(), eventName, category, properties, authenticatedUserId);
            }
        }

        public void LogInfo(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            if (ConfigureApplicationInsights.ApplicationInsightsConfig.Enabled &&
                ConfigureApplicationInsights.ApplicationInsightsConfig.LoggingLevel >= LogLevel.Information)
            {
                TrackEvent(LogLevel.Information.ToString(), eventName, category, properties, authenticatedUserId);
            }
        }

        public void LogWarn(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            if (ConfigureApplicationInsights.ApplicationInsightsConfig.Enabled &&
                ConfigureApplicationInsights.ApplicationInsightsConfig.LoggingLevel >= LogLevel.Warning)
            {
                TrackEvent(LogLevel.Warning.ToString(), eventName, category, properties, authenticatedUserId);
            }
        }

        public string LogExecption(Exception ex, string category, dynamic properties, string authenticatedUserId)
        {
            return TrackException(ex, category, properties, authenticatedUserId);
        }

        public void LogError(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            if (ConfigureApplicationInsights.ApplicationInsightsConfig.Enabled &&
                ConfigureApplicationInsights.ApplicationInsightsConfig.LoggingLevel >= LogLevel.Error)
            {
                TrackEvent(LogLevel.Error.ToString(), eventName, category, properties, authenticatedUserId);
            }
        }

        public void LogFatal(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            if (ConfigureApplicationInsights.ApplicationInsightsConfig.Enabled &&
                ConfigureApplicationInsights.ApplicationInsightsConfig.LoggingLevel >= LogLevel.Critical)
            {
                TrackEvent(LogLevel.Critical.ToString(), eventName, category, properties, authenticatedUserId);
            }
        }

        public void TrackMetric(string eventName, string category, dynamic properties, string authenticatedUserId)
        {
            
            var metricInfo = new MetricTelemetry();
            metricInfo.Name = eventName;
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
            var eventToSave = new EventTelemetry {Name = eventName};
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
            var exceptionToSave = new ExceptionTelemetry {Exception = ex};
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