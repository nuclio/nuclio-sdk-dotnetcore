using System;
using System.Collections.Generic;
using NetJSON;

namespace nuclio_sdk_dotnetcore
{
    public class Logger
    {
        public enum LogLevel
        {
            Error,
            Warning,
            Info,
            Debug
        }

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

        public void Log(LogLevel level, string message, Dictionary<string, object> with = null)
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