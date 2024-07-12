namespace FDNRBox
{
    public class WindowsDesktop
    {
        private IntPtr hDesktop = IntPtr.Zero;
        private string lpszDesktop = "";
        private bool isOpen = false;

        public IntPtr DesktopPointer { get => hDesktop;  }
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
        public void OpenDesktopByName(string name)
        {
            hDesktop = Win32APIWrapper.OpenDesktop(name, 0, true, (long)Win32APIWrapper.ACCESS_MASK.GENERIC_ALL);
            if (hDesktop != IntPtr.Zero)
            {
                isOpen = true;
                lpszDesktop = name;
            }
            else
            {
                hDesktop = Win32APIWrapper.CreateDesktop(name, IntPtr.Zero, IntPtr.Zero, 0, (long)Win32APIWrapper.ACCESS_MASK.GENERIC_ALL, IntPtr.Zero);
                if (hDesktop != IntPtr.Zero)
                {
                    isOpen = true;
                    lpszDesktop = name;
                }
                else    throw new Exception("Operation Failed");
            }
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
}
