using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FishDeskNextReborn
{
    /// <summary>
    /// FlitterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FlitterWindow : Window
    {
        private List<string> progs = new List<string>();
        public FlitterWindow()
        {
            InitializeComponent();
        }

        private void LoadList()
        {
            ProgListTxtbox.Text = "";
            progs = killlistHelper.Readkilllist();
            foreach (string s in progs)
            {
                ProgListTxtbox.Text += $"{s}\n";
            }
        }

        private void SaveList()
        {
            progs.Clear();
            foreach (string s in ProgListTxtbox.Text.Split('\n'))
            {
                progs.Add(s);
            }
            killlistHelper.Savekilllist(progs);
        }

        private void AddProg()
        {
            progs.Add(InputProgTxtbox.Text);
            ProgListTxtbox.Text += $"{InputProgTxtbox.Text}\n";
            InputProgTxtbox.Text = "";
            killlistHelper.Savekilllist(progs);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProg();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadList();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadList();
        }

        private void OpenProcessWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessNames processNames = new ProcessNames();
            processNames.ShowDialog(); 
        }

        private void ProgListTxtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveList();
        }
    }
}
