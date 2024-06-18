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

            this.Loaded += (sender, e) => LoadList();
            this.Closed += (sender,e) => SaveList();
            this.AddBtn.Click += (sender, e) => AddProg();
            this.LoadBtn.Click += (sender, e) => LoadList();
            this.ProgListTxtbox.LostFocus += (sender, e) => SaveList();
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


        private void OpenProcessWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessNames processNames = new ProcessNames();
            processNames.ShowDialog(); 
        }

    }
}
