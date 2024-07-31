using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace FishDeskNextReborn.resource
{
    public class FDNRBackGround
    {
        public static string CustomBrushesPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FishDeskNextReborn\\Brushes");
        public string BackGroundBrushString {  get; set; }
        public string BackGroundName {  get; set; }

        public Brush BackGroundBrush
        {
            get
            {
                try
                {
                    var brush = (Brush)XamlReader.Parse(BackGroundBrushString);
                    return brush;
                }
                catch
                {
                }
                return Brushes.White;
            }
        }

        public FDNRBackGround(string backGroundName, string backGroundBrush)
        {
            BackGroundBrushString = backGroundBrush;
            BackGroundName = backGroundName;
        }
    }

    public class DefaultBackGroundBrush
    {
        public static List<FDNRBackGround> DefaultBackGroundBrushList = new List<FDNRBackGround>()
        {
            new("FDNR经典(内置)", "<LinearGradientBrush StartPoint=\"0,0\" EndPoint=\"1,0\" SpreadMethod=\"Reflect\" Opacity=\"1\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><LinearGradientBrush.GradientStops><GradientStop Color=\"#FFFBC2EB\" Offset=\"0\" /><GradientStop Color=\"#FFA6C1EE\" Offset=\"1\" /></LinearGradientBrush.GradientStops></LinearGradientBrush>"),
            new ("幸草橙(内置)", "<SolidColorBrush xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">#FFFFC878</SolidColorBrush>"),
            new ("朴素白(内置)", "<SolidColorBrush xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">#FFFF</SolidColorBrush>")

        };


    }
}
