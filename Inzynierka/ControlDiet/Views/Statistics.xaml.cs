using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ApplicationToSupportAndControlDiet.Models;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.UI;
using ApplicationToSupportAndControlDiet.ViewModels;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// private ObservableCollection<Day> dayStatistics;
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
            this.InitializeComponent();
            Repository<Day> repo = new Repository<Day>();
            DateTime dateTime = Converters.ConvertDateTimeOffsetToDateTime(Globals.Date.Value); 
            Day day = repo.FindDayByDate(dateTime);

            Repository<User> repo2 = new Repository<User>();
            User user = repo2.FindUser();
            float maxValue;
            if (user ==null)
            {
                maxValue = 0;
                WarningCal.Text = "Complete information about your profile";
            }
            else
            {
                maxValue = user.TotalDailyEnergyExpenditure;
            }

            if (day == null) {
                EnergyBar.Value = 0;
                EnergyText.Text = "0 /" + Convert.ToString(maxValue);
                return;
            }
            //User user = new User();
            //PIECHART
            float dayValue = day.Energy;
            List<Microelements> financialStuffList = new List<Microelements>();
            financialStuffList.Add(new Microelements() { Name = "Protein", Amount = day.Protein * 4 });    
            financialStuffList.Add(new Microelements() { Name = "Fat", Amount = day.Fat * 9 });
            financialStuffList.Add(new Microelements() { Name = "Carbohydronate", Amount = day.Carbohydrate * 4 });
            (PieChart.Series[0] as PieSeries).ItemsSource = financialStuffList;

            //Table nutritions

            ProteinRow.Text= day.Protein.ToString();
            FatRow.Text = day.Fat.ToString();
            CarbohydronateRow.Text = day.Fat.ToString();
            FiberRow.Text = day.Fiber.ToString();
            SugarRow.Text = day.Sugar.ToString();
            //PROGRESSBAR

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
                if (user == null)
                {
                    WarningCal.Text = "Complete information about your profile";
                }
                else
                {
                    WarningCal.Text = "You eat too many calories";
                }
                
               
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

        private void DataPicker2_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (!sender.Date.Equals(Globals.Date)) { 
            Globals.MainPage.NavigateTo(this);
            }
        }
    }
}
