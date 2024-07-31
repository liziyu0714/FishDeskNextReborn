using FDNRBox;
using FishDeskNextReborn.Helpers;
using FishDeskNextReborn.window;
using System.Windows;

namespace FishDeskNextReborn
{
    /// <summary>
    /// App.xaml的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static string[]? LaunchArgs;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Register.RegistAll();
            LaunchArgs = e.Args;
            //已存在实例
            if (mutexHelper.Detect())
            {
                argResolver.Resolve(e.Args);
                this.Shutdown();
            }
            //不存在实例
            else
            {
                mutexHelper.CreateMutex();
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
