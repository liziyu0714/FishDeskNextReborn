﻿using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace FishDeskNextReborn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
        byte bVk,    //虚拟键值
        byte bScan,// 一般为0
        int dwFlags,  //这里是整数类型  0 为按下，2为释放
        int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
    );


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                switch (e.Args[0])
                {
                    case "-P":
                        {
                            ShowDesk();
                            PrevDesk();
                        }
                        break;
                    case "-C":
                        {
                            return;
                        }
                    case "-E":
                        {
                            employHelper.EmployApplication();
                        }
                        break;
                    default:
                        {
                            ShowDesk();
                            NextDesk();
                            killlistHelper.KILL();
                        }
                        break;
                }
            }
            else
            {
                ShowDesk();
                NextDesk();
                killlistHelper.KILL();
            }
            //App.Current.Shutdown();
        }
        public static void NextDesk()
        {
            /*
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x27, 0, 0, 0);
            keybd_event(0x27, 0, 2, 0);
            keybd_event(0x11, 0, 2, 0);
            keybd_event(0x5B, 0, 2, 0);
            */

        }

        public static void PrevDesk()
        {
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x25, 0, 0, 0);
            keybd_event(0x25, 0, 2, 0);
            keybd_event(0x11, 0, 2, 0);
            keybd_event(0x5B, 0, 2, 0);
        }

        public static void ShowDesk()
        {
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x44, 0, 0, 0);
            keybd_event(0x5B, 0, 2, 0);
            keybd_event(0x44, 0, 2, 0);
        }


        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"发生致命错误!{e.Exception.Message}");
            App.Current.Shutdown();
        }
    }

}
