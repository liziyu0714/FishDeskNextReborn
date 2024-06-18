using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows;
using System.IO;
using IWshRuntimeLibrary;

namespace FishDeskNextReborn
{
    //什么时候高兴了再来写吧
    public class employHelper
    {
        public static string dataFolder = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FishDeskNextReborn\\App");
        public static void EmployApplication()
        {
            MessageBox.Show("FDNR 部署工具(BuildIn),按\"确定\"继续");

            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            if (!windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                if (MessageBox.Show("要提升权限吗?", "FNDR", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    RestartAsAdmin();
                }
            }
            else MessageBox.Show("正在使用管理员权限部署", "FNDR", MessageBoxButton.OK, MessageBoxImage.Information);
            
            if(MessageBox.Show("要将应用程序复制到APPDATA中还是保留在此位置?如果要进行复制,选择\"是\"。","FDNR",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
            if(MessageBox.Show("要在开始菜单中生成快捷方式，还是仅在当前文件夹下生成?如果要在开始菜单中生成,选择\"是\"","FDNR",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        public static void RestartAsAdmin()
        {
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            if (!windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName,
                    UseShellExecute = true,
                    Verb = "runas",
                    Arguments = "-E"
                };

                try
                {
                    Process.Start(processStartInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"需要管理员权限!{ex.Message}");
                }
                Application.Current.Shutdown();
            }
        }
        public static void CopyApplication()
        {
            FileInfo file = new FileInfo(Environment.ProcessPath!);
            DirectoryInfo directoryInfo = file.Directory!;
            if(Directory.Exists(dataFolder))
            {
                MessageBox.Show("正在删除旧的部署文件，按\"确定\"继续");
                Directory.Delete(dataFolder, true );
                MessageBox.Show("旧的部署文件已清除，按\"确定\"继续");
            }
            Directory.CreateDirectory(dataFolder);

            Directory.Move(directoryInfo.FullName, dataFolder);
            MessageBox.Show("已部署文件至APPDATA，按\"确定\"继续");

        }
        public static void GenerateLinks()
        {
            
        }
        public static void CreateShortcut(string directory, string shortcutName, string targetPath,string description = null, string iconLocation = null)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Save();//保存快捷方式
        }
    }
}
