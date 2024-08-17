using FishDeskNextReborn.resource;
using FishDeskNextReborn.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FishDeskNextReborn.Views
{
    /// <summary>
    /// ColorMgmt.xaml 的交互逻辑
    /// </summary>
    public partial class ColorMgmt : Window
    {
        public ColorMgmt()
        {
            InitializeComponent();
            this.DataContext = new ColorMgmtModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["BackGroundBrush"] = ((sender as Button).Tag as FDNRBackGround).BackGroundBrush;

        }
    }
}
