using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.IO;
using System.Diagnostics;
using System.Security.Policy;
using HandyControl.Controls;
using System.Windows.Interop;
using FishDeskNextReborn.window;

namespace FishDeskNextReborn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //挂载事件
            CloseWindowBtn.Click += (e, sender) => { this.Visibility = Visibility.Hidden; };
            DragBtn.PreviewMouseLeftButtonDown += (e, sender) => { this.DragMove(); };
            this.Loaded += (e,sender) => { Growl.Warning("EOL Warning:此应用的支持将会在再可预见的将来终止"); };
            ntfIcon.Click += (e, sender) => { this.Visibility = Visibility.Visible; };
            ntfIcon.MouseDoubleClick += (e, sender) => { App.NextDesk(); };
            miNextDesktop.Click += (e, sender) => { App.NextDesk(); };
            miPreviousDesktop.Click += (e, sender) => { App.PrevDesk(); };
            miOpenConfig.Click += (e, sender) => { this.Visibility = Visibility.Visible; };
            miExit.Click += (e, sender) => { Application.Current.Shutdown(); };
            OpenDeployBtn.Click += (e, sender) =>
            {
                DeployWindow deployWindow = new DeployWindow();
                deployWindow.ShowDialog();
            };
            DebugBtn.Click += (e, sender) => { Test(); };

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://github.com/liziyu0714/FishDeskNextReborn") { CreateNoWindow = true });
        }

        private void GoToFlitterBtn_Click(object sender, RoutedEventArgs e)
        {
            FlitterWindow flitterWindow = new FlitterWindow();
            flitterWindow.ShowDialog();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            hwndSource!.AddHook(WndProc);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == mutexHelper.WM_SHOW)
            {
                this.Visibility = Visibility.Visible;
                this.Focus();
            }

            if(msg == mutexHelper.WM_LAUNCH_SHOWINFO)
            {
                Growl.InfoGlobal("当程序处于TaskView模式时，切换的是\"任务视图\"中的桌面;切换为Win32模式时,会调用win32api切换正真的Desktop");
                this.Visibility = Visibility.Hidden;
            }

            if(msg == mutexHelper.WM_LAUNCH_DEPLOY)
            {
                DeployWindow deployWindow = new DeployWindow();
                deployWindow.ShowDialog();
                this.Visibility = Visibility.Hidden;    
            }

            if(msg == mutexHelper.WM_LAUNCH_TOGGLE_MODE)
            {
                
            }
            return IntPtr.Zero;
        }

        private void Test()
        {
            //IntPtr wst = DesktopUtils.GetProcessWindowStation();
            //DesktopUtils.EnumDesktops(wst, (name,lp) => { System.Windows.MessageBox.Show(name);return true; }, 0);
        }
    }
}