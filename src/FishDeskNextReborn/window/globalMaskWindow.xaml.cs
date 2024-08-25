using System.Windows;

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
