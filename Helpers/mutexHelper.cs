using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FishDeskNextReborn
{
    public class mutexHelper
    {
        public const string mutexName = "FishDeskNext.Mutex";
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOW = RegisterWindowMessage("WM_SHOWME");
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
