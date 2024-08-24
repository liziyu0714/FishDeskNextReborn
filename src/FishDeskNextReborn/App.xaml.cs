using FishDeskNextReborn.Helpers;
using FishDeskNextReborn.Resources;
using FishDeskNextReborn.window;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

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
                ConfigHelper.Read();
                this.Resources["BackGroundBrush"] = ReadAppBrush();
                ResourceDictionary dictionary = new ResourceDictionary { Source = new Uri($@"Resources\{CultureInfo.CurrentUICulture}.xaml", UriKind.RelativeOrAbsolute) };
                Application.Current.Resources.MergedDictionaries.Add (dictionary);
            }
        }

        private Brush ReadAppBrush()
        {
            if (!Directory.Exists(FDNRBackGround.CustomBrushesPath))
                Directory.CreateDirectory(FDNRBackGround.CustomBrushesPath);
            if (!ConfigHelper.Config.ContainsKey("BackGroundBrush"))
                ConfigHelper.Config["BackGroundBrush"] = FishDeskNextReborn.Resources.DefaultBackGroundBrush.DefaultBackGroundBrushList[0].BackGroundBrushString;
            return (Brush)XamlReader.Parse(ConfigHelper.Config["BackGroundBrush"]);
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
            ConfigHelper.Config["BackGroundBrush"] = XamlWriter.Save(this.Resources["BackGroundBrush"]);
            ConfigHelper.Save();
        }
    }

}
