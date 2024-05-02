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
            if (File.Exists("killlist"))
            {
                foreach (string prog in File.ReadAllLines("killlist"))
                {
                    if(prog != "")
                    {
                        progs.Add(prog);
                        ProgListTxtbox.Text += $"{prog}\n";
                    }
                    
                }
            }
            else File.Create("killlist");
        }

        private void SaveList()
        {
            progs = new List<string>();
            foreach(string t in ProgListTxtbox.Text.Split('\n'))
            {
                t.Replace("\t", "");
                t.Replace("\n", "");
                progs.Add(t);
            }
            if (File.Exists("killlist"))
                File.Delete("killlist");
            File.AppendAllLines("killlist", progs);
        }

        private void AddProg()
        {
            progs.Add(InputProgTxtbox.Text);
            ProgListTxtbox.Text += $"{InputProgTxtbox.Text}\n";
            InputProgTxtbox.Text = "";
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
    }
}
