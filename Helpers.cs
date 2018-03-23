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
using System.Text;
using Utf8Json;

namespace Nuclio.Sdk
{
    public static class NuclioSerializationHelpers<T>
    {
        public static T Deserialize(string str)
        {
            return Utf8Json.JsonSerializer.Deserialize<T>(str);
        }

        public static string Serialize(T obj)
        {
            return Utf8Json.JsonSerializer.ToJsonString(obj);
        }
    }

    public sealed class UnixTimestampInt64DateTimeFormatter : IJsonFormatter<DateTime>
    {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver)
        {
            var ticks = (long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
            writer.WriteInt64(ticks);
        }

        public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var ticks = reader.ReadInt64();
            return UnixEpoch.AddSeconds(ticks);
        }
    }
}