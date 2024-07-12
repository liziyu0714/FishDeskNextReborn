using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using FishDeskNextReborn.window;
using FishDeskNextReborn.Helpers;
using FDNRBox;

namespace FishDeskNextReborn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[]? LaunchArgs;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Register.RegistAll();
            LaunchArgs = e.Args;
            //已存在实例
            if(mutexHelper.Detect())
            {
                argResolver.Resolve(e.Args);
                this.Shutdown();
            }
            //不存在实例
            else
            {
                mutexHelper.CreateMutex();
                DesktopManager.InitDesktops();
            }
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ErrorWindow errorWindow = new ErrorWindow(e.Exception);
            errorWindow.ShowDialog();
            App.Current.Shutdown();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            mutexHelper.DestroyMutex();
        }
    }

}
