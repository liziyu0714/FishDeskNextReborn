using FDNRBox;
using FishDeskNextReborn.Helpers;
using FishDeskNextReborn.window;
using HandyControl.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using static FishDeskNextReborn.Helpers.taskviewToggler;

namespace FishDeskNextReborn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public DeployWindow deployWindow = new DeployWindow();
        public DesktopMgmtWindow desktopMgmtWindow = new DesktopMgmtWindow();
        public FlitterWindow flitterWindow = new FlitterWindow();
        public ToggleDesktopMode togglemode = ToggleDesktopMode.TaskView;

        public enum ToggleDesktopMode
        {
            Win32, TaskView
        }


        public MainWindow()
        {
            InitializeComponent();

            //挂载事件
            RegistEvents();

            this.Loaded += (object sender, RoutedEventArgs e) =>
            {
                //解析参数
                if (App.LaunchArgs != null && App.LaunchArgs.Length > 0)
                {
                    argResolver.Resolve(App.LaunchArgs);
                    this.Visibility = Visibility.Hidden;
                }

            };


        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource? hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            hwndSource!.AddHook(WndProc);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            return broadcastHelper.ProcessMessage(msg, this);
        }

        private void RegistEvents()
        {
            CloseWindowBtn.Click += (e, sender) => { this.Visibility = Visibility.Hidden; };
            DragBtn.PreviewMouseLeftButtonDown += (e, sender) => { this.DragMove(); };
            ntfIcon.Click += (e, sender) => { this.Visibility = Visibility.Visible; };
            ntfIcon.MouseDoubleClick += (e, sender) => { NextDesktop(); };
            miNextDesktop.Click += (e, sender) => { NextDesktop(); };
            miPreviousDesktop.Click += (e, sender) => { PreviousDesktop(); };
            miOpenConfig.Click += (e, sender) => { this.Visibility = Visibility.Visible; };
            miExit.Click += (e, sender) => { Application.Current.Shutdown(); };
            OpenDeployBtn.Click += (e, sender) => { deployWindow = new DeployWindow(); deployWindow.ShowDialog(); };
            DebugBtn.Click += (e, sender) => { Test(); };
            linkOpenHyperLink.Click += (e, sender) => { Process.Start(new ProcessStartInfo("cmd", $"/c start https://github.com/liziyu0714/FishDeskNextReborn") { CreateNoWindow = true }); };
            GoToFlitterBtn.Click += (e, sender) => { flitterWindow = new FlitterWindow(); flitterWindow.ShowDialog(); };
            DesktopMgmtOpenBtn.Click += (e, sender) => { desktopMgmtWindow = new DesktopMgmtWindow(); desktopMgmtWindow.ShowDialog(); };
        }

        private void Test()
        {
            DesktopManager.SwitchToDesktop("FDNR1");
        }

        public void NextDesktop()
        {
            if (togglemode == ToggleDesktopMode.TaskView)
            {
                ShowDesk();
                NextDesk();
                ShowDesk();
                killlistHelper.KILL();
            }
            else DesktopManager.NextDesktop();
        }
        public void PreviousDesktop()
        {
            if (togglemode == ToggleDesktopMode.TaskView)
            {
                ShowDesk();
                PrevDesk();
                ShowDesk();
                killlistHelper.KILL();
            }
            else DesktopManager.PrevDesktop();
        }

        public void ChangeToggleMode()
        {
            if (togglemode == ToggleDesktopMode.TaskView)
            {
                togglemode = ToggleDesktopMode.Win32;
                DesktopManager.InitDesktops(["FDNR1", "FDNR2", "FDNR3", "FDNR4", "FDNR5"]);
                Growl.InfoGlobal("已切换到Win32模式");
            }

            else
            {
                togglemode = ToggleDesktopMode.TaskView;
                Growl.InfoGlobal("已切换到Taskview模式");
            }
        }
    }
}