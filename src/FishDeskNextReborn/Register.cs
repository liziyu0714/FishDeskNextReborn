using FishDeskNextReborn.Helpers;

namespace FishDeskNextReborn
{
    public class Register
    {
        public static void RegistAll()
        {
            RegistMessage();
            RegistArgs();
        }

        private static void RegistMessage()
        {
            broadcastHelper.Regist("Show window", (sender) => { sender.Visibility = System.Windows.Visibility.Visible; return 0; });
            broadcastHelper.Regist("Hide window", (sender) => { sender.Visibility = System.Windows.Visibility.Hidden; return 0; });
            broadcastHelper.Regist("Previous Desktop", (sender) => { sender.PreviousDesktop(); return 0; });
            broadcastHelper.Regist("Next Desktop", (sender) => { sender.NextDesktop(); return 0; });
            broadcastHelper.Regist("Open Deploy", (sender) => { sender.deployWindow.ShowDialog(); return 0; });
            broadcastHelper.Regist("Toggle Mode", (sender) => { sender.ChangeToggleMode(); return 0; });
            broadcastHelper.Regist("Open Desktop Manage Window", (sender) => { return 0; });
        }

        private static void RegistArgs()
        {
            argResolver.Register("-N", () => { broadcastHelper.Broadcast("Next Desktop"); });
            argResolver.Register("-P", () => { broadcastHelper.Broadcast("Previous Desktop"); });
            argResolver.Register("-D", () => { broadcastHelper.Broadcast("Open Deploy"); });
            argResolver.Register("-S", () => { broadcastHelper.Broadcast("Show window"); });
            argResolver.Register("-T", () => { broadcastHelper.Broadcast("Toggle Mode"); });
            argResolver.Register("--OpenDesktopMgmt", () => { });
            argResolver.Register("--Silent", () => { broadcastHelper.Broadcast("Hide window"); });
        }
    }
}
