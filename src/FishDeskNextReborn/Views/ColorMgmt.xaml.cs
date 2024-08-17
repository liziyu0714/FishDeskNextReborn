using FishDeskNextReborn.Helpers;
using FishDeskNextReborn.resource;
using FishDeskNextReborn.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
