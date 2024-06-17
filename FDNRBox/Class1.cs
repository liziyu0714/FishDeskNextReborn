
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace FDNRBox
{
    public static class Win32APIWrapper
    {
        public delegate bool EnumDesktopProc(string lpszDesktop, int lParam);
        public delegate bool EnumWindowsProc(IntPtr hwnd, int lParam);

        public static int DF_ALLOWOTHERACCOUNTHOOK = 0x0001;
        public enum ACCESS_MASK
        {
            Default = 0,
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000
        }
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public uint lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDesktops(IntPtr hwinsta, EnumDesktopProc lpEnumFunc, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr GetProcessWindowStation();
        [DllImport("user32.dll")]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern bool CreateDesktop(string lpszDesktop, string lpszDevice, string pDevmod, int dwflags, ACCESS_MASK dwDesiredAccess, SECURITY_ATTRIBUTES lpsa);

        [DllImport("user32.dll")]
        public static extern bool CloseDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern IntPtr OpenDesktop(string lpszDesktop, int dwFlags, bool fInherit, ACCESS_MASK dwDesiredAccess);
        [DllImport("user32.dll")]
        public static extern IntPtr OpenInputDesktop(int dwFlags, bool fInherit, ACCESS_MASK dwDesiredAccess);
        [DllImport("user32.dll")]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern bool SwitchDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(int dwThreadId);
        [DllImport("Kernel32.dll")]
        public static extern int GetCurrentThreadId();
        [DllImport("Kernel32.dll")]
        public static extern int GetLastError();
    }

    public class WindowsDesktop
    {
        private IntPtr hDesktop = IntPtr.Zero;
        private string lpszDesktop = "";
        private bool isOpen = false;

        public nint DesktopPointer { get => hDesktop;  }
        public string DesktopName { get => lpszDesktop; }
        public bool IsOpen { get => isOpen;}

        public WindowsDesktop()
        { 
            hDesktop = new IntPtr(0);
            lpszDesktop = "";
            isOpen = false;
        }

        public WindowsDesktop(string name)
        {
            OpenDesktopByName(name);
        }

        public WindowsDesktop(string name, int flags, bool inherit, Win32APIWrapper.ACCESS_MASK access_mask)
        {
            OpenDesktopByName(name, flags, inherit, access_mask);
        }

        public void OpenDesktopByName(string name)
        {
            hDesktop = Win32APIWrapper.OpenDesktop(name, 0, true, Win32APIWrapper.ACCESS_MASK.Default);
            if (hDesktop != IntPtr.Zero)
            {
                isOpen = true;
                lpszDesktop = name;
            }
            else throw new Exception("Operation failed.");
        }
        
        public void OpenDesktopByName(string name , int flags , bool inherit, Win32APIWrapper.ACCESS_MASK access_mask)
        {
            hDesktop = Win32APIWrapper.OpenDesktop(name, flags, inherit, access_mask);
            if (hDesktop != IntPtr.Zero) 
            { 
                isOpen = true;
                lpszDesktop = name;
            }
            else { throw new Exception("Operation failed"); }
        }

        public void OpenDesktopByPointer(IntPtr pointer)
        {

        }

        public void Close()
        {
            if(isOpen)
            {
                Win32APIWrapper.CloseDesktop(hDesktop);
                isOpen = false;
            }

        }

        public void SwitchToThis()
        {
            if(isOpen)
            {
                Win32APIWrapper.SwitchDesktop(hDesktop);
            }
        }
    }
    public static class DesktopManager
    {
        public static List<WindowsDesktop> DesktopList { get; set; } = new List<WindowsDesktop>();
        
        public static WindowsDesktop GetInputDesktop()
        {
            WindowsDesktop windowsDesktop = new WindowsDesktop();
            Win32APIWrapper.OpenInputDesktop(0, true, Win32APIWrapper.ACCESS_MASK.Default);
            return windowsDesktop;
        }

    }
}
