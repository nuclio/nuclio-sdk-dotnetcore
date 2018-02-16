using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace nuclio_sdk_dotnetcore
{
    public static class Helpers<T>
    {
        public static T Deserialize(string str)
        {    
            var bin = MessagePackSerializer.FromJson(str);
            return MessagePackSerializer.Deserialize<T>(bin);
        }

        public static string Serialize(T obj)
        {
            return MessagePackSerializer.ToJson(obj);
        }
    }
}