using static FDNRBox.Win32APIWrapper;

namespace FDNRBox
{
    public static class DesktopManager
    {

        public static List<WindowsDesktop> DesktopList { get; set; } = new List<WindowsDesktop>();
        public static int DesktopCount { get => DesktopList.Count; }

        public static WindowsDesktop? GetInputDesktop()
        {
            //WindowsDesktop windowsDesktop = new WindowsDesktop();
            OpenInputDesktop(0, true, 0);
            return null;
        }

        public static void RefreshDesktops()
        {
            DesktopList.Clear();
            EnumDesktops(GetProcessWindowStation(), (name, para) =>
            {
                DesktopList.Add(new WindowsDesktop(name, DesktopCount));
                return true;
            }, 0);
        }

        public static void InitDesktops()
        {
            RefreshDesktops();
            InitNormalDesktop("FDNR1");
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

    }
}
