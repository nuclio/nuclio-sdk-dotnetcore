using System;
using System.Collections.Generic;
using NetJSON;

namespace nuclio_sdk_dotnetcore
{
    public class Metric
    {
        [NetJSONProperty("duration")]
        public long Duration { get; set; }


    }
}