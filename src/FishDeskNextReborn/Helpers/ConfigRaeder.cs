using System.IO;
using System.Text.Json;

namespace FishDeskNextReborn.Helpers
{
    public class ConfigHelper
    {
        public static Dictionary<string, string> Config = new Dictionary<string, string>();
        public static string configPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FishDeskNextReborn\\Config.json");
        public static void Read()
        {
            if (File.Exists(configPath))
            {
                Config = (Dictionary<string, string>)JsonSerializer.Deserialize(File.ReadAllText(configPath), typeof(Dictionary<string, string>))!;
            }
        }
        public static void Save()
        {
            string serialstring = JsonSerializer.Serialize(Config);
            File.Delete(configPath);
            File.Create(configPath).Close();
            File.WriteAllText(configPath, serialstring);
        }
    }
}
