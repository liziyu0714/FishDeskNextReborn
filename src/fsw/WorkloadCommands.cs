using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsw
{
    public class WorkloadCommands
    {
        [JsonProperty("command")]
        public required string WorkloadCommand;
        [JsonProperty("description")]
        public required string WorkloadCommandDescription;
        [JsonProperty("origin")]
        public required string WorkloadOriginCommand;
    }
}
