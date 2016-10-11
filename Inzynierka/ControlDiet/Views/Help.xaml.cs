using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using ApplicationToSupportAndControlDiet.Models;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class Microelements
    {
        public string Name { get; set; }
        public float Amount { get; set; }
    }
    public sealed partial class Help : Page
    {
        public Help()
        {
            Day day = new Day();
            //User user = new User();
            //PIECHART
            this.InitializeComponent();
            Random rand = new Random();
            List<Microelements> financialStuffList = new List<Microelements>();
            financialStuffList.Add(new Microelements() { Name = "Protein", Amount = day.Protein  * 4 });    //rand.Next(0, 200)
            financialStuffList.Add(new Microelements() { Name = "Fat", Amount = day.Fat * 9 });
            financialStuffList.Add(new Microelements() { Name = "Carbohydronate", Amount = day.Carbohydrate * 4 });          
            (PieChart.Series[0] as PieSeries).ItemsSource = financialStuffList;

            //PROGRESSBAR
            
            float maxValue = 2000;
            float dayValue = day.Energy;
            EnergyBar.Maximum = maxValue;
            EnergyBar.Value = dayValue;
            EnergyText.Text = Convert.ToString(dayValue) + "/" + Convert.ToString(maxValue);
            float difference = maxValue - dayValue;
            if (difference>100)
            {
                EnergyBar.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            else if ( difference<=100 && difference>=-100)
            {
                EnergyBar.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                EnergyBar.Foreground = new SolidColorBrush(Colors.Red);
            }

        }
    }

}
