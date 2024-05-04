using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishDeskNextReborn
{
    public class killlistHelper
    {
        public static string dataFolder = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FishDeskNextReborn");
        public static string killlistPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"FishDeskNextReborn\\killlist");
        public static List<string> Readkilllist()
        {
            List<string> list = new List<string>();
            if(File.Exists(killlistPath))
            {
                using (StreamReader sr = new StreamReader(killlistPath))
                {
                    while(!sr.EndOfStream)
                    list.Add(sr.ReadLine());
                }
            }
            list.RemoveAll(x => x == "");
            return list;
        }

        public static void Savekilllist(List<string> list)
        {
            if(!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
            if(File.Exists(killlistPath))
                File.Delete(killlistPath);
            File.Create(killlistPath).Close();
            list.RemoveAll(x => x == "");
            using (StreamWriter sw = new StreamWriter(killlistPath))
            {
                foreach(string line in list)
                {
                    sw.WriteLine(line);
                }
            }

            return;
        }
    }
}
