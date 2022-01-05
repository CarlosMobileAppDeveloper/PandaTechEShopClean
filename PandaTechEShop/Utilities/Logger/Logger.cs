using System;
using System.Collections.Generic;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;

namespace PandaTechEShop.Utilities.Logger
{
    public class Logger : ILogger
    {
        public void LogError(Exception exception = null, Dictionary<string, string> properties = null)
        {
            // TODO
            //Crashes.TrackError(exception, properties);

            Console.ForegroundColor = ConsoleColor.Red;
            System.Diagnostics.Debug.WriteLine(exception);
            Console.ResetColor();
        }

        public void LogEvent(string name, string detail, Dictionary<string, string> properties = null)
        {
            // TODO
            //Analytics.TrackEvent($"{name} : {detail}", properties);

            Console.ForegroundColor = ConsoleColor.Cyan;
            System.Diagnostics.Debug.WriteLine($"Log {name} : {detail}");
            Console.ResetColor();
        }

        public void LogEvent(string name, Dictionary<string, string> properties = null)
        {
            // TODO
            //Analytics.TrackEvent($"Log: {name}", properties);

            Console.ForegroundColor = ConsoleColor.Cyan;
            System.Diagnostics.Debug.WriteLine($"Log {name}");
            Console.ResetColor();
        }

        public void LogWarning(string name, Dictionary<string, string> properties = null)
        {
            // TODO
            //Analytics.TrackEvent($"Warning! {name}", properties);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Diagnostics.Debug.WriteLine($"Warning! {name}");
            Console.ResetColor();
        }
    }
}
