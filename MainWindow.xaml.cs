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
using System.Diagnostics;
using System.Security.Policy;

namespace FishDeskNextReborn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            CloseWindowBtn.Click += (e, sender) => { this.Close(); };
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://github.com/liziyu0714/FishDeskNextReborn") { CreateNoWindow = true });
        }

        private void GoToFlitterBtn_Click(object sender, RoutedEventArgs e)
        {
            FlitterWindow flitterWindow = new FlitterWindow();
            flitterWindow.ShowDialog();
        }
    }
}