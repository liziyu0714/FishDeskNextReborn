using Newtonsoft.Json;
using fsw;
using System.Runtime.InteropServices;
namespace fsw.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WorkloadProperties workloadProperties = new WorkloadProperties()
            {
                WorkloadAssembly = "Example.dll",
                WorkloadAuthor = "liziyu0714",
                WorkloadFriendlyName = "A Example fsw workload",
                WorkloadGlobalName = "example",
                WorkloadMainClassName = "MainClass",
                WorkloadProjectAddress = "https://github.com/liziyu0714/FishDeskNextReborn",
                WorkloadVesrion = "0.0.1",
                WorkloadOsPlatform = OSType.win,
                PersistenceNeeded = false,
            };

            List<WorkloadCommands> commands = new List<WorkloadCommands>();
            commands.Add(new WorkloadCommands() { WorkloadCommand = "$executive --Test", WorkloadCommandDescription = "直接运行exe（并没有任何内容）" });
            commands.Add(new WorkloadCommands() { WorkloadCommand = "/fsw showInfo", WorkloadCommandDescription= "执行fsw内源命令"});
            commands.Add(new WorkloadCommands() { WorkloadCommand = "/Example.Test abc 123", WorkloadCommandDescription = "调用类库中的函数" });
            commands.Add(new WorkloadCommands() { WorkloadCommand = "#Example.Test(abc,123)" , WorkloadCommandDescription = "调用F#解释器解释命令"});
            Console.WriteLine(JsonConvert.SerializeObject(workloadProperties));
            Console.WriteLine(JsonConvert.SerializeObject(commands));

        }
    }
}
