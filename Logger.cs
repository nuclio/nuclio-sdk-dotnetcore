using System;
using System.Collections.Generic;
using NetJSON;

namespace nuclio_sdk_dotnetcore
{
    public class Logger
    {
        public event EventHandler LogEvent;

        [NetJSONProperty("level")]
        public string Level { get; set; }

        [NetJSONProperty("message")]
        public string Message { get; set; }

        [NetJSONProperty("datetime")]
        public string DateTime { get; set; }

        [NetJSONProperty("with")]
        Dictionary<string, object> With { get; set; }

        public Logger()
        {
            With = new Dictionary<string, object>();
            DateTime = (System.DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
        }

        ///<summary>
        /// Log an error message
        /// e.g. context.Error("%s not responding after %d seconds", dbHost, timeout)
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Error(string format, params object[] args)
        {
            Log(LogLevel.Error, string.Format(format, args));
        }

        ///<summary>
        /// Log a warning message
        /// e.g. context.Warning("%s %.2f full", "memory", mem_full)
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Warning(string format, params object[] args)
        {
            Log(LogLevel.Warning, string.Format(format, args));
        }

        ///<summary>
        /// Log a debug message
        /// e.g. context.Debug("event with %d bytes", event.GetSize())
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Debug(string format, params object[] args)
        {
            Log(LogLevel.Debug, string.Format(format, args));
        }

        ///<summary>
        /// Log an info message
        /// e.g. context.Info("event with %d bytes", event.GetSize())
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Info(string format, params object[] args)
        {
            Log(LogLevel.Info, string.Format(format, args));
        }

        public void ErrorWith(string message, params object[] parameters)
        {
            Log(LogLevel.Error, message, EncodeWith(parameters));
        }

        public void WarnWith(string message, params object[] parameters)
        {
            Log(LogLevel.Warning, message, EncodeWith(parameters));
        }

        public void DebugWith(string message, params object[] parameters)
        {
            Log(LogLevel.Debug, message, EncodeWith(parameters));
        }

        public void InfoWith(string message, params object[] parameters)
        {
            Log(LogLevel.Info, message, EncodeWith(parameters));
        }

        private enum LogLevel
        {
            Error,
            Warning,
            Info,
            Debug
        }
        
        private Dictionary<string, object> EncodeWith(object[] with)
        {
            var withMap = new Dictionary<string, object>();
            if (with.Length % 2 != 0)
            {
                Console.WriteLine($"error: bad width length - {with.Length}");
                return withMap;
            }
            for (int i = 0; i < with.Length; i += 2)
            {
                withMap.Add(with[i].ToString(), with[i + 1]);
            }
            return withMap;
        }

        private void Log(LogLevel level, string message, Dictionary<string, object> with = null)
        {
            Level = level.ToString().ToLower();
            Message = message;
            if (with == null)
                with = new Dictionary<string, object>();
            With = with;
            if (LogEvent != null)
                LogEvent(this, new EventArgs());
        }

    }
}