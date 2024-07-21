using System.Windows;
using static FDNRBox.Win32APIWrapper;

namespace FDNRBox
{
    public static class DesktopManager
    {

        public static List<WindowsDesktop> DesktopList { get; set; } = new List<WindowsDesktop>();
        public static int DesktopCount { get => DesktopList.Count; }

        public static int NowDesktop = 0;

        public static WindowsDesktop? GetInputDesktop()
        {
            WindowsDesktop windowsDesktop;
            IntPtr inputDesk = OpenInputDesktop(0, true, 0);
            
            return null;
        }

        public static void RefreshDesktops()
        {
            DesktopList.Clear();
            EnumDesktops(GetProcessWindowStation(), (name, para) =>
            {
                DesktopList.Add(new WindowsDesktop(name, DesktopCount));
                if(name == "Default")
                    NowDesktop = DesktopCount - 1;
                return true;
            }, 0);
        }

        public static void InitDesktops(List<string> DesktopNames)
        {
            RefreshDesktops();
            foreach (string desktopName in DesktopNames)
            {
                InitNormalDesktop(desktopName);
            }
        }

        public static void DestroyDesktops()
        {

        }

        public static void SwitchToDesktop(string name)
        {
            foreach (WindowsDesktop desktop in DesktopList)
            {
                if (desktop.DesktopName == name)
                {
                    Thread thread = new Thread(() =>
                    {
                        if (SetThreadDesktop(desktop.DesktopPointer))
                            SwitchDesktop(desktop.DesktopPointer);
                    });
                    thread.Start();

                }

            }
        }

        private static void InitNormalDesktop(string name)
        {
            WindowsDesktop desktop = new WindowsDesktop(name, DesktopCount);
            DesktopList.Add(desktop);
            IntPtr org = GetThreadDesktop(GetCurrentThreadId());
            Thread thread = new Thread(() =>
            {
                if (SetThreadDesktop(desktop.DesktopPointer))
                {
                    StartProcessOnDesktop(ExplorerPath, desktop.DesktopName);
                    SetThreadDesktop(org);
                }
            });
            thread.Start();

        }

        public static void NextDesktop()
        {
            NowDesktop = (NowDesktop + 1) % DesktopCount;
            SwitchToDesktop(DesktopList[NowDesktop].DesktopName);
        }

        public static void PrevDesktop()
        {
            NowDesktop = (NowDesktop - 1) % DesktopCount;
            SwitchToDesktop(DesktopList[NowDesktop].DesktopName);
        }

    }
}
