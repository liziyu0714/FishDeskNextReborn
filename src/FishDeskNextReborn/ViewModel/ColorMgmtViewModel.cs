using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using FishDeskNextReborn.resource;

namespace FishDeskNextReborn.ViewModel
{ 
    public class ColorMgmtModel
    {
        private RelayCommand hyperLinkCommand;
        public RelayCommand HyperLinkCommand
        {
            get
            {
                if (hyperLinkCommand == null)
                {
                    hyperLinkCommand = new RelayCommand(() =>
                    {
                        Process.Start("explorer.exe", FDNRBackGround.CustomBrushesPath);
                    });
                }
                return hyperLinkCommand;
            }

        }

 

        public List<FDNRBackGround> BackGrounds { get; set; }
        public ColorMgmtModel()
        {
            BackGrounds = new List<FDNRBackGround>( DefaultBackGroundBrush.DefaultBackGroundBrushList);
            foreach (var file in Directory.GetFiles(FDNRBackGround.CustomBrushesPath))
            {
                FileInfo fileInfo = new FileInfo(file);
                BackGrounds.Add(new FDNRBackGround(fileInfo.Name, File.ReadAllText(file)));
            }
        }

        public static void ChangeBackGround(object sender)
        {

        }

    }
}
