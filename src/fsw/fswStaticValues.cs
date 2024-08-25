using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsw
{
    public class fswStaticValues
    {
        public static List<string> SpecialFileNames { get; set; } = new List<string> { "workload.json", "commands.json", "workload", "commands" };
    }
}
