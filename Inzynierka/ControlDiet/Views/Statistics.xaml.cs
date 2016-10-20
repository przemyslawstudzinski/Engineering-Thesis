using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ApplicationToSupportAndControlDiet.Models;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.UI;
using ApplicationToSupportAndControlDiet.ViewModels;
using Windows.UI.Xaml;
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

    public sealed partial class Statistics : Page
    {

        public Nullable<DateTimeOffset> Date
        {
            get
            {
                return Globals.Date;
            }
            set
            {
                Globals.Date = value;
            }

        }

        public Statistics()
        {
            Repository<Day> repo = new Repository<Day>();
            DateTime dateTime = Converters.ConvertDateTimeOffsetToDateTime(Globals.Date.Value); 
            Day day = repo.FindDayByDate(dateTime);
            //User user = new User();
            //PIECHART
            this.InitializeComponent();
            Random rand = new Random();
            List<Microelements> financialStuffList = new List<Microelements>();
            financialStuffList.Add(new Microelements() { Name = "Protein", Amount = rand.Next(0, 200) }); //day.Protein * 4 });    //rand.Next(0, 200)
            financialStuffList.Add(new Microelements() { Name = "Fat", Amount = rand.Next(0, 200) }); //day.Fat * 9 });
            financialStuffList.Add(new Microelements() { Name = "Carbohydronate", Amount = rand.Next(0, 200) });// day.Carbohydrate * 4 });
            (PieChart.Series[0] as PieSeries).ItemsSource = financialStuffList;

            //PROGRESSBAR

            float maxValue = 2000; //user.day .....
            float dayValue = rand.Next(1500, 2000);// day.Energy;
            EnergyBar.Maximum = maxValue;
            EnergyBar.Value = dayValue;
            EnergyText.Text = Convert.ToString(dayValue) + "/" + Convert.ToString(maxValue);
            float difference = maxValue - dayValue;

            if (difference > 100)
            {
                EnergyBar.Foreground = new SolidColorBrush(Colors.Yellow);
                 WarningCal.Text = "";
            }
            else if (difference <= 100 && difference >= 0)
            {
                EnergyBar.Foreground = new SolidColorBrush(Colors.Green);
                WarningCal.Text = "";
            }
            else
            {
                EnergyBar.Foreground = new SolidColorBrush(Colors.Red);
                WarningCal.Text = "You eat too many calories";
            }
        }

        private void NextDay_Click(object sender, RoutedEventArgs e)
        {
            Date = Date.Value.AddDays(1);
            Globals.MainPage.NavigateTo(this);
        }

        private void PreviousDay_Click(object sender, RoutedEventArgs e)
        {
            Date = Date.Value.AddDays(-1);
            Globals.MainPage.NavigateTo(this);
        }

    }
}
