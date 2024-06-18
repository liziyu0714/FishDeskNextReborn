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
    /// ErrorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(Exception exception)
        {
            InitializeComponent();
            this.txtblockErrorDesp.Text = $"{exception.Message}";
            this.txtblockErrorType.Text = $"{exception.GetType()}";
            this.txtblockErrorStack.Text = $"{exception.StackTrace}";

        }
    }
}
