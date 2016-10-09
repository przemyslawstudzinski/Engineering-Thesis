using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class Microelements
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
    public sealed partial class Help : Page
    {
        public Help()
        {
            this.InitializeComponent();
            Random rand = new Random();
            List<Microelements> financialStuffList = new List<Microelements>();
            financialStuffList.Add(new Microelements() { Name = "MSFT", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new Microelements() { Name = "AAPL", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new Microelements() { Name = "GOOG", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new Microelements() { Name = "BBRY", Amount = 700 });//rand.Next(0, 200) });          
            (BarChart.Series[0] as BarSeries).ItemsSource = financialStuffList;            
        }
    }
}
