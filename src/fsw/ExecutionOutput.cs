using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsw
{
    public enum ExecutionState
    {
        Failed,
        Exception,
        Warning,
        Success
    }
    public class ExecutionOutput
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExecutionState State { get; set; }

        [JsonProperty("output")]
        public required string Output { get; set; }

        [JsonProperty("origin")]
        public required string OriginCommand { get; set; }  
    }
}
