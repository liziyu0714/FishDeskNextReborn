using System.Runtime.InteropServices;

namespace FishDeskNextReborn
{
    public class mutexHelper
    {
        public const string mutexName = "FishDeskNext.Mutex";
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOW = RegisterWindowMessage("WM_SHOWME");
        public static readonly int WM_LAUNCH = RegisterWindowMessage("WM_LAUNCH");
        public static readonly int WM_LAUNCH_SHOWINFO = RegisterWindowMessage("WM_LAUNCH_SHOWINFO");
        public static readonly int WM_LAUNCH_DEPLOY = RegisterWindowMessage("WM_LAUNCH_DEPLOY");
        public static readonly int WM_LAUNCH_TOGGLE_MODE = RegisterWindowMessage(" WM_LAUNCH_TOGGLE_MODE");
        public static Mutex? mutex = null;
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
        public static bool Detect()
        {
            return Mutex.TryOpenExisting(mutexName, out _);
        }
        public static void CreateMutex()
        {
            mutex = new Mutex(true, mutexName);
        }
        public static void DestroyMutex()
        {
            if (mutex != null)
                mutex.ReleaseMutex();
        }
    }
}
