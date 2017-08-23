using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Dynamic;
using Demo.ApplicationInsights.Interface;
using Microsoft.CSharp;

namespace Demo.ApplicationInsights
{
    public class AppInsightsTimer : IDisposable
    {
        public bool Sucess { get; set; }
        private long _start;
        private IAppLogger _appLogger;
        private string _userValue;
        private System.Diagnostics.Stopwatch _clock;
        private string _msgID;
        private dynamic _properties;
        private DateTimeOffset _startTime;

        public AppInsightsTimer(IAppLogger appLogger, string userValue)
        {
            Iniialize(appLogger, userValue, null, null);
        }

        public AppInsightsTimer(IAppLogger appLogger, string userValue, dynamic properties)
        {
            Iniialize(appLogger, userValue, properties, null);
        }

        public AppInsightsTimer(IAppLogger appLogger, string userValue, dynamic properties, string msgID)
        {
            Iniialize(appLogger, userValue, properties, msgID);
        }

        private void Iniialize(IAppLogger appLogger, string userValue, dynamic properties, string msgID)
        {
            _appLogger = appLogger;
            _userValue = userValue;
            _msgID = msgID;
            Sucess = true;
            _properties = properties;
            _clock = new Stopwatch();
            _startTime = DateTimeOffset.Now;
            _clock.Start();

        }

        public void Dispose()
        {
            if (_appLogger == null)
            {
                return;
            }
            _clock.Stop();
            _appLogger.TrackRequest(_userValue, _startTime, _clock.Elapsed, Sucess, _properties, _msgID);
        }
    }
}
