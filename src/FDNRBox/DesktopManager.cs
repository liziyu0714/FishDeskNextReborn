using System.Diagnostics;

namespace FDNRBox
{
    public static class DesktopManager
    {
        public static List<WindowsDesktop> DesktopList { get; set; } = new List<WindowsDesktop>();
        
        public static WindowsDesktop GetInputDesktop()
        {
            WindowsDesktop windowsDesktop = new WindowsDesktop();
            Win32APIWrapper.OpenInputDesktop(0, true, 0);
            return windowsDesktop;
        }

        public static void RefreshDesktops()
        {
            DesktopList.Clear();
            Win32APIWrapper.EnumDesktops(Win32APIWrapper.GetProcessWindowStation(), (name, para) => { DesktopList.Add(new WindowsDesktop(name)); return true; }, 0);
        }

        public static void InitDesktops()
        {
            RefreshDesktops();
            InitNormalDesktop("FDNR1");
            InitNormalDesktop("FDNR2");
            InitNormalDesktop("FDNR3"); 
            InitNormalDesktop("FDNR4");
            InitNormalDesktop("FDNR5");
        }

        public static void DestroyDesktops()
        {

        }

        private static void InitNormalDesktop(object name)
        {
            WindowsDesktop windowsDesktop = new WindowsDesktop((string)name);
            DesktopList.Add(windowsDesktop);
            var a = 0;
            WindowsDesktop result = DesktopList.Find((desk) => (desk.DesktopName! == name));
            if( Win32APIWrapper.SetThreadDesktop(result.DesktopPointer))
                Process.Start(new ProcessStartInfo("explorer.exe"));
            else 
                a = Win32APIWrapper.GetLastError();
        }
        private static void InitNormalDesktopWrapper(string name)
        {

            //InitNormalDesktop(name);
        }
       
    
    }
}
