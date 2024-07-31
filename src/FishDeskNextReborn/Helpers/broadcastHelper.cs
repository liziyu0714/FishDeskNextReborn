using System.Runtime.InteropServices;

namespace FishDeskNextReborn.Helpers
{
    public class broadcastHelper
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        public const int HWND_BROADCAST = 0xffff;

        public delegate int BroadcastDelegate(MainWindow sender);
        public static Dictionary<string, BroadcastDelegate> broadcastMessages = new Dictionary<string, BroadcastDelegate>();
        public static Dictionary<string, int> MessagePtr = new Dictionary<string, int>();

        public static void Broadcast(string msg)
        {
            PostMessage(HWND_BROADCAST, MessagePtr[msg], IntPtr.Zero, IntPtr.Zero);
        }
        public static void Regist(string message, BroadcastDelegate broadcastDelegate)
        {
            MessagePtr[message] = RegisterWindowMessage(message);
            broadcastMessages[message] = broadcastDelegate;
        }

        public static int ProcessMessage(int messagePtr, FishDeskNextReborn.MainWindow sender)
        {
            if (MessagePtr.ContainsValue(messagePtr))
            {
                foreach (var message in MessagePtr)
                {
                    if (message.Value == messagePtr)
                    {
                        return broadcastMessages[message.Key].Invoke(sender);
                    }
                }
            }
            return -1;
        }
    }
}
