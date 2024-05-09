using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows;

namespace FishDeskNextReborn
{
    //什么时候高兴了再来写吧
    public class employHelper
    {
        public static void EmployApplication()
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
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"需要管理员权限!{ex.Message}");
                }
            }


        }
    }
}
