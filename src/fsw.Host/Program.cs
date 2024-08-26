using Newtonsoft.Json;
using fsw;
using System.Runtime.InteropServices;
namespace fsw.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            fsw.fswCommandHost host = new fsw.fswCommandHost();
            WorkloadReadConfig workloadReadConfig = new WorkloadReadConfig() { fswWorkDirectory = @"H:\repos\fsw\work", TmpFilePath = @"H:\repos\fsw\tmp" };
            fswConfig fswConfig = new fswConfig() { WorkloadReadConfig= workloadReadConfig};
            Workload workload = new Workload(new FileInfo(@"H:\repos\fsw\eg.zip"),fswConfig);

        }
    }
}
