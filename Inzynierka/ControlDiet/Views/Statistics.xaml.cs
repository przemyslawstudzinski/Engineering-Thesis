using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ApplicationToSupportAndControlDiet.Models;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.UI;
using ApplicationToSupportAndControlDiet.ViewModels;
using Windows.UI.Xaml;

namespace ApplicationToSupportAndControlDiet.Views
{
    public sealed partial class Statistics : Page
    {
        private const int CALORIES_OF_PROTEIN_IN_GRAM = 4;
        private const int CALORIES_OF_CARBOHYDRATE_IN_GRAM = 4;
        private const int CALORIES_OF_FAT_IN_GRAM = 9;

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
            Day day = FindDay();
            User user;
            float maxValue;
            FindUser(out user, out maxValue);

            if (day == null)
            {
                EnergyBar.Value = 0;
                EnergyText.Text = "0 /" + maxValue.ToString("N0");
                return;
            }
            else
            {
                double dayValue = day.Energy;
                InitializeProgressBar(user, maxValue, dayValue);
            }
            InitializeTable(day);
            InitializePieChart(day);
        }

        private void InitializeTable(Day day)
        {
            ProteinRow.Text = day.Protein.ToString("N1");
            FatRow.Text = day.Fat.ToString("N1");
            CarbohydronateRow.Text = day.Carbohydrate.ToString("N1");
            FiberRow.Text = day.Fiber.ToString("N1");
            SugarRow.Text = day.Sugar.ToString("N1");
        }

        private void InitializePieChart(Day day)
        {
            List<Microelements> financialStuffList = new List<Microelements>();
            financialStuffList.Add(new Microelements() { Name = "Protein",
                Amount = day.Protein *  CALORIES_OF_PROTEIN_IN_GRAM});
            financialStuffList.Add(new Microelements() { Name = "Fat",
                Amount = day.Fat * CALORIES_OF_FAT_IN_GRAM });
            financialStuffList.Add(new Microelements() { Name = "Carbohydronate",
                Amount = day.Carbohydrate * CALORIES_OF_CARBOHYDRATE_IN_GRAM });
            (PieChart.Series[0] as PieSeries).ItemsSource = financialStuffList;
        }

        private void FindUser(out User user, out float maxValue)
        {
            Repository<User> repo2 = new Repository<User>();
            user = repo2.FindUser();
            if (user == null)
            {
                maxValue = 0;
                WarningCal.Text = "Complete information about your profile";
            }
            else
            {
                maxValue = user.TotalDailyEnergyExpenditure;
            }
        }

        private static Day FindDay()
        {
            Repository<Day> repo = new Repository<Day>();
            DateTime dateTime = DateTimeOffsetToDateTimeConverter.ConvertDateTimeOffsetToDateTime(Globals.Date.Value);
            Day day = repo.FindDayByDate(dateTime);
            return day;
        }

        private void InitializeProgressBar(User user, double maxValue, double dayValue)
        {
            EnergyBar.Maximum = maxValue;
            EnergyBar.Value = dayValue;
            EnergyText.Text = dayValue.ToString("N0") + "/" + maxValue.ToString("N0");
            double difference = maxValue - dayValue;

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

        private class Microelements
        {
            public string Name { get; set; }
            public double Amount { get; set; }
        }
    }
}