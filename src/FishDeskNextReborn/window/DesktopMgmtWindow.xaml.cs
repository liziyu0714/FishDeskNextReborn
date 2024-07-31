using FDNRBox;
using System.Windows;

namespace FishDeskNextReborn.window
{
    /// <summary>
    /// DesktopMgmtWindow.xaml的交互逻辑
    /// </summary>
    public partial class DesktopMgmtWindow : Window
    {
        public DesktopMgmtWindow()
        {
            InitializeComponent();

            DesktopManager.RefreshDesktops();
            DesktopsDataGrid.ItemsSource = DesktopManager.DesktopList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
