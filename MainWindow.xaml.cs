using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.IO;

namespace FishDeskNextReborn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
        byte bVk,    //虚拟键值
        byte bScan,// 一般为0
        int dwFlags,  //这里是整数类型  0 为按下，2为释放
        int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
    );

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public static void NextDesk()
        {
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x27, 0, 0, 0);
            keybd_event(0x27, 0, 2, 0);
            keybd_event(0x11, 0, 2, 0);
            keybd_event(0x5B, 0, 2, 0);

        }

        public static void PrevDesk()
        {
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x11, 0, 0, 0);
            keybd_event(0x25, 0, 0, 0);
            keybd_event(0x25, 0, 2, 0);
            keybd_event(0x11, 0, 2, 0);
            keybd_event(0x5B, 0, 2, 0);
        }

        public static void ShowDesk()
        {
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x44, 0, 0, 0);
            keybd_event(0x5B, 0, 2, 0);
            keybd_event(0x44, 0, 2, 0);
        }

       
    }
}