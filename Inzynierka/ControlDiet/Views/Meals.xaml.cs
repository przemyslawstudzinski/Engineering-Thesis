using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ApplicationToSupportAndControlDiet.ViewModels;
using ApplicationToSupportAndControlDiet.Models;
using System.Collections.ObjectModel;
using ApplicationToSupportAndControlDiet.Views;
using System.Collections.Generic;

namespace ApplicationToSupportAndControlDiet
{
    public sealed partial class MealsPage : Page
    {
        private ObservableCollection<Meal> items;
        private Repository<Day> dayRepository;
        private Repository<Meal> mealRepository;
        private Day choosenDay;

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

        public MealsPage()
        {
            this.InitializeComponent();
            dayRepository = new Repository<Day>();
            mealRepository = new Repository<Meal>();
            UpdateDay();
            if (choosenDay != null)
            {
                items = new ObservableCollection<Meal>(choosenDay.MealsInDay);
            }

            Repository<User> repo2 = new Repository<User>();
            User user = repo2.FindUser();
            if (user == null)
            {
                WarningCal.Text = "Complete information about your profile";
            }
            this.ItemsList.ItemsSource = items;
        }

        private void UpdateDay()
        {
            choosenDay = dayRepository.FindDayByDate(Globals.Date.Value.DateTime);
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

        private void SaveAsProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            Frame.Navigate(typeof(AddNewProduct), thisMeal);
        }

        private void SaveAsCsv_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            CsvExport mealToCsv = new CsvExport(thisMeal);
            mealToCsv.ExportMealToCsvFile();
        }

        private void CreateNewFromThisMeal_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            KeyValuePair<bool, Meal> parameters = new KeyValuePair<bool, Meal>(true, thisMeal);
            Frame.Navigate(typeof(AddMeal), parameters);
        }

        private void EditMeal_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;       
            KeyValuePair<bool, Meal> parameters = new KeyValuePair<bool, Meal>(false, thisMeal);
            Frame.Navigate(typeof(AddMeal), parameters);
        }

        private void DeleteMeal_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            Repository<Meal> mealRepository = new Repository<Meal>();
            if (mealRepository.Delete(thisMeal) > -1)
            {
                items.Remove(thisMeal);
            }
            UpdateDay();
            if (choosenDay.MealsInDay.Count == 0)
            {
                dayRepository.Delete(choosenDay);
            }
        }
    }
}
