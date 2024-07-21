using HandyControl.Controls;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows;

namespace FishDeskNextReborn.window
{
    /// <summary>
    /// DeployWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeployWindow : System.Windows.Window
    {
        public DeployWindow()
        {
            InitializeComponent();
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
                    Arguments = "-D"
                };

                try
                {
                    Process.Start(processStartInfo);
                }
                catch (Exception ex)
                {
                    HandyControl.Controls.MessageBox.Show($"需要管理员权限!{ex.Message}");
                }
                Application.Current.Shutdown();
            }
        }
        public static void CreateShortcut(string directory, string shortcutName, string targetPath, string description, string iconLocation, string arguments)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            string shortcutPath = System.IO.Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            shortcut.WorkingDirectory = System.IO.Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Arguments = arguments;
            shortcut.Save();//保存快捷方式
        }

        private void RunAsAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            RestartAsAdmin();
        }

        private void GenerateLinkBtn_Click(object sender, RoutedEventArgs e)
        {
            FileInfo path = new FileInfo(Environment.ProcessPath!);
            CreateShortcut(path.DirectoryName!, "FDNR Next", path.FullName, "Go to next desktop", path.FullName, "-N");
            CreateShortcut(path.DirectoryName!, "FDNR Previous", path.FullName, "Go to previous desktop", path.FullName, "-P");
            CreateShortcut(path.DirectoryName!, "FDNR Deploy", path.FullName, "Open FDNR Deploy Tool", path.FullName, "-D");
            CreateShortcut(path.DirectoryName!, "FDNR Config", path.FullName, "Open FDNR Config", path.FullName, "");
            Growl.Info("部署快捷方式完成");
        }
    }
}
