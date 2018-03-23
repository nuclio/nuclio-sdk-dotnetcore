//  Copyright 2017 The Nuclio Authors.
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nuclio.Sdk
{
    public class Logger
    {
        public event EventHandler LogEvent;

        [DataMember(Name = "level")]
        public string Level { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "datetime")]
        public string DateTime { get; set; }

        [DataMember(Name = "with")]
        public Dictionary<string, object> With { get; set; }

        ///<summary>
        /// Log an error message
        /// e.g. context.Logger.Error("{0} not responding after {1} seconds", dbHost, timeout)
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Error(string format, params object[] args)
        {
            Log(LogLevel.Error, string.Format(format, args));
        }

        ///<summary>
        /// Log a warning message
        /// e.g. context.Logger.Warning("{0} {1} full", "memory", mem_full)
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Warning(string format, params object[] args)
        {
            Log(LogLevel.Warning, string.Format(format, args));
        }

        ///<summary>
        /// Log a debug message
        /// e.g. context.Logger.Debug("event with {0} bytes", event.GetSize())
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Debug(string format, params object[] args)
        {
            Log(LogLevel.Debug, string.Format(format, args));
        }

        ///<summary>
        /// Log an info message
        /// e.g. context.Logger.Info("event with {0} bytes", event.GetSize())
        /// <param name="format">Message format</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void Info(string format, params object[] args)
        {
            Log(LogLevel.Info, string.Format(format, args));
        }

        ///<summary>
        /// Log an error message
        /// e.g. context.Logger.ErrorWith("bad request", "error", "daffy not found", "time", 7)
        /// <param name="format">Message</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void ErrorWith(string message, params object[] parameters)
        {
            Log(LogLevel.Error, message, EncodeWith(parameters));
        }

        ///<summary>
        /// Log an error message
        /// e.g. context.Logger.WarnWith("system overload", "resource", "memory", "used", 0.9)
        /// <param name="format">Message</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void WarnWith(string message, params object[] parameters)
        {
            Log(LogLevel.Warning, message, EncodeWith(parameters));
        }

        ///<summary>
        /// Log an error message
        /// e.g. context.Logger.DebugWith("event", "body_size", 2339, "content-type", "text/plain")
        /// <param name="format">Message</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
        public void DebugWith(string message, params object[] parameters)
        {
            Log(LogLevel.Debug, message, EncodeWith(parameters));
        }

        ///<summary>
        /// Log an error message
        /// e.g. context.Logger.InfoWith("event processed", "time", 0.3, "count", 9009)
        /// <param name="format">Message</param> 
        /// <param name="args">Formatting arguments</param>    
        ///</summary>
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
            DateTime = (System.DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
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