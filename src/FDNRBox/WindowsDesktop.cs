namespace FDNRBox
{
    public class WindowsDesktop
    {

        private IntPtr desktopPointer;
        private string desktopName;
        private int desktopId;

        public nint DesktopPointer { get => desktopPointer; set => desktopPointer = value; }
        public string DesktopName { get => desktopName; set => desktopName = value; }
        public int DesktopId { get => desktopId; set => desktopId = value; }

        public WindowsDesktop(string name, int id)
        {
            desktopPointer = Win32APIWrapper.CreateDesktop(name, IntPtr.Zero, IntPtr.Zero, 0, (long)Win32APIWrapper.DESKTOP_ACCESS_MASK.GENERIC_ALL, IntPtr.Zero);
            desktopName = name;
            desktopId = id;
        }

        public void Close()
        {
            Win32APIWrapper.CloseDesktop(DesktopPointer);
        }

        public void SwitchToThis()
        {
            Win32APIWrapper.SwitchDesktop(DesktopPointer);
        }
    }
}
