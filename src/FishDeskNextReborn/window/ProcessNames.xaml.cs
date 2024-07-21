using System.Diagnostics;
using System.Windows;

namespace FishDeskNextReborn
{
    /// <summary>
    /// ProcessNames.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessNames : Window
    {
        public ProcessNames()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                Processes.Text += $"{p.ProcessName}\n";
            }
        }
    }
}
