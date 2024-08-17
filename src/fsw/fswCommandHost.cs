using System;
using System.Collections.Generic;
using System.IO.Compression;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace fsw
{
    public class Workload
    {
        public WorkloadProperties Properties { get; set; }
        public List<WorkloadCommands> Commands { get; set; }

        public Type MainClass { get; set; }

        public Workload(FileInfo pathToWorkloadFile,WorkloadReadConfig workloadReadConfig)
        {
            if (!pathToWorkloadFile!.Exists)
                throw new FileNotFoundException("Invalid Workload File");
            ZipArchive workloadZipArchive = ZipFile.OpenRead(pathToWorkloadFile.FullName);
            workloadZipArchive.ExtractToDirectory(workloadReadConfig.TmpFilePath);
            DirectoryInfo directoryInfo = new DirectoryInfo(workloadReadConfig.TmpFilePath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if(!fswStaticValues.SpecialFileNames.Exists((s) => s == file.Name))
                {
                    File.Copy(file.FullName,Path.Combine(workloadReadConfig.fswWorkDirectory,file.Name));
                }
                if(file.Name == "workload.json")
                    Properties = JsonConvert.DeserializeObject<WorkloadProperties>(file.FullName)!;
                if(file.Name == "command.json")
                    Commands = JsonConvert.DeserializeObject<List<WorkloadCommands>>(file.FullName)!;
                File.Delete(file.FullName);
            }

            if(Commands == null) Commands = new List<WorkloadCommands>();
            Properties!.IsBuiltIn = false;
            DirectoryInfo fswWorkDirectory = new(workloadReadConfig.fswWorkDirectory);
            if (Properties.WorkloadAssembly != "" && Properties.WorkloadMainClassName != "")
            {
                FileInfo assemblyfile = Array.Find(fswWorkDirectory.GetFiles(), (info) => info.Name == Properties.WorkloadAssembly)!;
                byte[] buffer = File.ReadAllBytes(assemblyfile.FullName);
                Assembly assembly = Assembly.Load(buffer);
                MainClass = Array.Find(assembly.GetTypes(), (t) => t.Name == Properties.WorkloadAssembly)!;
                if (MainClass == null || MainClass!.Name != Properties.WorkloadMainClassName)
                    throw new Exception("MainClass not found!");
            }
            else throw new FileNotFoundException("Assembly not found!");
        }

        public Workload(WorkloadProperties properties, List<WorkloadCommands> commands, Type mainClass)
        {
            Properties = properties;
            Commands = commands;
            MainClass = mainClass;
            Properties.IsBuiltIn = true;
        }
    }
    public class fswCommandHost
    {
    }
}
