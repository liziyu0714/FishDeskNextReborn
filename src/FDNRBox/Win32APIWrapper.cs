using System.Runtime.InteropServices;

namespace FDNRBox
{
    public static partial class Win32APIWrapper
    {
        #region 常量
        public static string ExplorerPath = @"C:\\Windows\\explorer.exe";
        #endregion

        #region 原生委托
        public delegate bool EnumDesktopProc(string lpszDesktop, int lParam);
        public delegate bool EnumWindowsProc(IntPtr hwnd, int lParam);
        #endregion

        #region 原生结构与类
        [Flags]
        public enum CreateProcessFlags : uint
        {
            DEBUG_PROCESS = 0x00000001,
            DEBUG_ONLY_THIS_PROCESS = 0x00000002,
            CREATE_SUSPENDED = 0x00000004,
            DETACHED_PROCESS = 0x00000008,
            CREATE_NEW_CONSOLE = 0x00000010,
            NORMAL_PRIORITY_CLASS = 0x00000020,
            IDLE_PRIORITY_CLASS = 0x00000040,
            HIGH_PRIORITY_CLASS = 0x00000080,
            REALTIME_PRIORITY_CLASS = 0x00000100,
            CREATE_NEW_PROCESS_GROUP = 0x00000200,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            CREATE_SEPARATE_WOW_VDM = 0x00000800,
            CREATE_SHARED_WOW_VDM = 0x00001000,
            CREATE_FORCEDOS = 0x00002000,
            BELOW_NORMAL_PRIORITY_CLASS = 0x00004000,
            ABOVE_NORMAL_PRIORITY_CLASS = 0x00008000,
            INHERIT_PARENT_AFFINITY = 0x00010000,
            INHERIT_CALLER_PRIORITY = 0x00020000,
            CREATE_PROTECTED_PROCESS = 0x00040000,
            EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
            PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000,
            PROCESS_MODE_BACKGROUND_END = 0x00200000,
            CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
            CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
            CREATE_DEFAULT_ERROR_MODE = 0x04000000,
            CREATE_NO_WINDOW = 0x08000000,
            PROFILE_USER = 0x10000000,
            PROFILE_KERNEL = 0x20000000,
            PROFILE_SERVER = 0x40000000,
            CREATE_IGNORE_SYSTEM_DEFAULT = 0x80000000,
        }

        [Flags]
        public enum DuplicateOptions : uint
        {
            DUPLICATE_CLOSE_SOURCE = 0x00000001,
            DUPLICATE_SAME_ACCESS = 0x00000002
        }

        [Flags]
        public enum DESKTOP_ACCESS_MASK : uint
        {
            DESKTOP_NONE = 0,
            DESKTOP_READOBJECTS = 0x0001,
            DESKTOP_CREATEWINDOW = 0x0002,
            DESKTOP_CREATEMENU = 0x0004,
            DESKTOP_HOOKCONTROL = 0x0008,
            DESKTOP_JOURNALRECORD = 0x0010,
            DESKTOP_JOURNALPLAYBACK = 0x0020,
            DESKTOP_ENUMERATE = 0x0040,
            DESKTOP_WRITEOBJECTS = 0x0080,
            DESKTOP_SWITCHDESKTOP = 0x0100,

            GENERIC_ALL = DESKTOP_READOBJECTS | DESKTOP_CREATEWINDOW | DESKTOP_CREATEMENU |
                            DESKTOP_HOOKCONTROL | DESKTOP_JOURNALRECORD | DESKTOP_JOURNALPLAYBACK |
                            DESKTOP_ENUMERATE | DESKTOP_WRITEOBJECTS | DESKTOP_SWITCHDESKTOP,
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SECURITY_ATTRIBUTES
        {
            public int nLength;
            public required string lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public int lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public int wShowWindow;
            public int cbReserved2;
            public byte lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        #endregion

        #region 原生函数
        #region 桌面相关
        [LibraryImport("user32.dll", EntryPoint = "EnumDesktopsA")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool EnumDesktops(IntPtr hwinsta, EnumDesktopProc lpEnumFunc, int lParam);
        [LibraryImport("user32.dll", EntryPoint = "GetProcessWindowStation")]
        public static partial IntPtr GetProcessWindowStation();
        [LibraryImport("user32.dll", EntryPoint = "EnumDesktopWindowsA")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode,
                                                    int dwFlags, long dwDesiredAccess, IntPtr lpsa);


        [DllImport("user32.dll")]
        public static extern bool CloseDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern IntPtr OpenDesktop(string lpszDesktop, int dwFlags, bool fInherit, long dwDesiredAccess);
        [DllImport("user32.dll")]
        public static extern IntPtr OpenInputDesktop(int dwFlags, bool fInherit, long dwDesiredAccess);
        [DllImport("user32.dll")]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        public static extern bool SwitchDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(int dwThreadId);
        [DllImport("user32.dll")]
        public static extern bool GetUserObjectInformation(IntPtr hObj,int nIndex,ref uint pvInfo,uint nLength,ref int[] lpnLengthNeeded);
        #endregion
        #region 其他
        [DllImport("Kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetEnvironmentStrings();

        [DllImport("kernel32.dll")]
        public static extern uint ResumeThread(IntPtr hThread);

        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern bool CreateProcess(String lpApplicationName, String? lpCommandLine,
                                                             SECURITY_ATTRIBUTES? lpProcessAttributes,
                                                             SECURITY_ATTRIBUTES? lpThreadAttributes,
                                                             bool bInheritHandles,
                                                             CreateProcessFlags dwCreationFlags,
                                                             String? lpEnvironment,
                                                             String? lpCurrentDirectory,
                                                             ref STARTUPINFO lpStartupInfo,
                                                             ref PROCESS_INFORMATION lpProcessInformation
                                                             );
        [DllImport("Kernel32.dll")]
        public static extern int GetLastError();
        #endregion

        #endregion

        #region 包装好的函数

        /// <summary>
        /// 在指定桌面上启动进程
        /// </summary>
        /// <param name="name">进程的完整路径</param>
        /// <param name="hDesktop">桌面名称</param>
        /// <returns>进程是否成功启动</returns>
        public unsafe static bool StartProcessOnDesktop(string name, string hDesktop)
        {
            STARTUPINFO sTARTUPINFO = new STARTUPINFO();
            sTARTUPINFO.cb = sizeof(STARTUPINFO);
            sTARTUPINFO.lpDesktop = hDesktop;
            PROCESS_INFORMATION pROCESS_INFORMATION = new PROCESS_INFORMATION();

            var retValue = CreateProcess(name, null, null, null, false, 0, null, null, ref sTARTUPINFO, ref pROCESS_INFORMATION);
            var lastError = GetLastError();
            if (lastError != 0)
                throw new Exception($"GetLastError return {lastError}");
            return retValue;
        }
        public unsafe static string GetDesktopName(IntPtr hDesktop)
        {
            IntPtr pvInfo = IntPtr.Zero;
            throw new NotImplementedException();
            
        }

        #endregion
    }
}
