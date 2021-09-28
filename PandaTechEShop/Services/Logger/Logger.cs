using System;
using System.Collections.Generic;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;

namespace PandaTechEShop.Services
{
    public class Logger : ILogger
    {
        public void LogError(Exception exception = null, Dictionary<string, string> properties = null)
        {
            // TODO
            //Crashes.TrackError(exception, properties);
        }

        public void LogEvent(string name, string detail, Dictionary<string, string> properties = null)
        {
            // TODO
            //Analytics.TrackEvent($"{name} : {detail}", properties);
        }

        public void LogEvent(string name, Dictionary<string, string> properties = null)
        {
            // TODO
            //Analytics.TrackEvent($"Log: {name}", properties);
        }

        public void LogWarning(string name, Dictionary<string, string> properties = null)
        {
            // TODO
            //Analytics.TrackEvent($"Warning! {name}", properties);
        }
    }
}
