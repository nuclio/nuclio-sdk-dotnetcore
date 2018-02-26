using System;
using System.Collections.Generic;
using MessagePack;

namespace nuclio_sdk_dotnetcore
{
    [MessagePackObject]
    public class Metric
    {
        [Key("duration")]
        public long Duration { get; set; }


    }
}