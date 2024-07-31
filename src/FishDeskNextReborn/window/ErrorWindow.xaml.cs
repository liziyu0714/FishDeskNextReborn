using System.Windows;

namespace FishDeskNextReborn.window
{
    /// <summary>
    /// ErrorWindow.xaml的交互逻辑
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
