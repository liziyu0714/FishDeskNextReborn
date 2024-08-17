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

namespace FishDeskNextReborn.window
{
    /// <summary>
    /// globalMaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class globalMaskWindow : Window
    {
        public globalMaskWindow()
        {
            InitializeComponent();
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth;
            
        }
    }
}
