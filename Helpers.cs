using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace nuclio_sdk_dotnetcore
{
    internal static class StringHelpers
    {
        internal static string DecodeString(byte[] value)
        {
            var decodedString = Encoding.ASCII.GetString(value);
            return decodedString;
        }
        internal static byte[] EncodeString(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            return data;
        }
    }
    public static class NuclioSerializationHelpers<T>
    {
        private static NetJSON.NetJSONSettings _settings;
        static NuclioSerializationHelpers()
        {
            _settings = GetJsonSettings();
        }
        public static T Deserialize(string str)
        {
            return NetJSON.NetJSON.Deserialize<T>(str, _settings);
        }

        public static string Serialize(T obj)
        {
            return NetJSON.NetJSON.Serialize<T>(obj, _settings);
        }

        private static NetJSON.NetJSONSettings GetJsonSettings()
        {
            var settings = new NetJSON.NetJSONSettings();
            settings.CaseSensitive = false;
            settings.CamelCase = false;
            settings._caseComparison = StringComparison.InvariantCultureIgnoreCase;
            settings.DateFormat = NetJSON.NetJSONDateFormat.EpochTime;
            settings.TimeZoneFormat = NetJSON.NetJSONTimeZoneFormat.Utc;
            settings.UseEnumString = true;
            return settings;
        }
    }
}