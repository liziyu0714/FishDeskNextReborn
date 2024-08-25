using System;
using System.Collections.Generic;
using System.IO.Compression;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;
using System.Runtime.Versioning;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace fsw
{
    public class Workload
    {
        public WorkloadProperties Properties { get; set; }
        public List<WorkloadCommands>? Commands { get; set; }

        public Type? MainClass { get; set; }

        /// <summary>
        /// 用于从文件初始化workload
        /// </summary>
        /// <param name="pathToWorkloadFile">workload的路径</param>
        /// <param name="workloadReadConfig">workload读取配置</param>
        /// <exception cref="FileNotFoundException">当workload或Assembly路径无效时抛出此异常</exception>
        /// <exception cref="Exception">workload指定的主类未找到时抛出此异常</exception>
        public Workload(FileInfo pathToWorkloadFile, fswConfig config)
        {
            if (!pathToWorkloadFile!.Exists)
                throw new FileNotFoundException("Invalid Workload File");
            ZipArchive workloadZipArchive = ZipFile.OpenRead(pathToWorkloadFile.FullName);
            workloadZipArchive.ExtractToDirectory(config.workloadReadConfig.TmpFilePath);
            DirectoryInfo directoryInfo = new DirectoryInfo(config.workloadReadConfig.TmpFilePath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (!fswStaticValues.SpecialFileNames.Exists((s) => s == file.Name))
                {
                    File.Copy(file.FullName, Path.Combine(config.workloadReadConfig.fswWorkDirectory, file.Name));
                }
                if (file.Name == "workload.json")
                    Properties = JsonConvert.DeserializeObject<WorkloadProperties>(file.FullName)!;
                if (file.Name == "command.json")
                    Commands = JsonConvert.DeserializeObject<List<WorkloadCommands>>(file.FullName)!;
                File.Delete(file.FullName);
            }

            Properties!.IsBuiltIn = false;
            DirectoryInfo fswWorkDirectory = new(config.workloadReadConfig.fswWorkDirectory);
            if (Properties.WorkloadAssembly != "" && Properties.WorkloadMainClassName != "")
            {
                FileInfo assemblyfile = Array.Find(fswWorkDirectory.GetFiles(), (info) => info.Name == Properties.WorkloadAssembly)!;
                byte[] buffer = File.ReadAllBytes(assemblyfile.FullName);
                Assembly assembly = Assembly.Load(buffer);
                MainClass = Array.Find(assembly.GetTypes(), (t) => t.Name == Properties.WorkloadAssembly)!;
                if (MainClass == null || MainClass!.Name != Properties.WorkloadMainClassName)
                    throw new Exception("MainClass not found!");
            }

        }

        /// <summary>
        /// 用于从现成的属性、预设指令集与主类创建workload
        /// </summary>
        /// <param name="properties">属性</param>
        /// <param name="commands">预设指令集</param>
        /// <param name="mainClass">主类</param>
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
        public List<Workload> Workloads { get; }
        public List<object> PersistentWorkloadClass { get; set; }

        public Dictionary<string,string> CommandMapping { get; set; }
        public void Init()
        {

        }

        public fswCommandHost()
        {
            Workloads = new List<Workload>();
            PersistentWorkloadClass = new List<object>();
            CommandMapping = new Dictionary<string,string>();
        }

        public fswCommandHost(string[] pathsToworkload, fswConfig config)
        {
            Workloads = new List<Workload>();
            PersistentWorkloadClass = new List<object>();
            CommandMapping = new Dictionary<string,string>();
            foreach (string path in pathsToworkload)
            {
                Workload workload = new Workload(new FileInfo(path), config);
                AddWorkload(workload);
            }
        }

        /// <summary>
        /// 添加workload
        /// </summary>
        /// <param name="workload">workload</param>
        public void AddWorkload(Workload workload)
        {
            if (workload.Properties.PersistenceNeeded && workload.MainClass != null)
                PersistentWorkloadClass.Add(Activator.CreateInstance(workload.MainClass)!);
            if(workload.Commands != null)
            {
                foreach (WorkloadCommands commands in workload.Commands)
                {
                    CommandMapping[commands.WorkloadCommand] = commands.WorkloadOriginCommand;
                }
            }
            Workloads.Add(workload);
        }

        /// <summary>
        /// 卸载指定的workload
        /// </summary>
        /// <param name="workloadName">workload的全局名称</param>
        public void RemoveWorkload(string workloadName)
        {
            foreach(Workload workload in Workloads)
            { 
                if(workload.Properties.WorkloadGlobalName == workloadName)
                {
                    //移除持久化类
                    if(workload.MainClass != null)
                        foreach(object obj in PersistentWorkloadClass)
                        {
                            if(obj.GetType() == workload.MainClass)
                                PersistentWorkloadClass.Remove(obj);
                        }

                    //移除命令映射
                    if(workload.Commands != null)
                        foreach(WorkloadCommands commands in workload.Commands)
                        {
                            foreach(KeyValuePair<string,string> pair in CommandMapping)
                            {
                                if(pair.Key == commands.WorkloadCommand)
                                    CommandMapping.Remove(pair.Value);
                            }
                        }
                    //卸载workload
                    Workloads.Remove(workload);
                }
            }
        }

        /// <summary>
        /// 执行指令并返回结果
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public string Execute(string command)
        {
            //截取第一个字符
            switch (command[0])
            {
                case '$':
                    return ExecuteSingleSystemShell(command.Substring(1));
                case '/':
                    throw new NotImplementedException();
                case '#':
                    return ExecuteFSharpCommand(command.Substring(1));
                default:
                    return $"Unknow Command Header \"{command[0]}\"";
            }
        }

        public string ExecuteSingleSystemShell(string shelllCommand)
        {

            string output = "";
            shelllCommand = shelllCommand.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = shelllCommand;
                p.StartInfo.FileName = shelllCommand;
                p.StartInfo.FileName = shelllCommand;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine(shelllCommand);
                p.StandardInput.AutoFlush = true;
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            return output;
        }

        private bool DetectFSharp()
        {
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.CreateNoWindow=true;
                process.StartInfo.FileName = "dotnet";
                try
                {
                    process.Start();
                }
                catch
                {
                    return false;
                }
            }

            return true;

        }

        public string ExecuteFSharpCommand(string fSharpCommand)
        {
            if (!DetectFSharp())
                return "dotnet SDK not found.You need to intall dotnet to execute F# command.";
            return fSharpCommand;
        }
    }
}
