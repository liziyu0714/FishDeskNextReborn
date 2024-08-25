using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsw
{
    public class WorkloadReadConfig
    {
        public required string TmpFilePath { get; set; }
        public required string fswWorkDirectory { get; set; }
    }

    public class fswConfig
    {
        public required WorkloadReadConfig workloadReadConfig { get; set; }
    }
}
