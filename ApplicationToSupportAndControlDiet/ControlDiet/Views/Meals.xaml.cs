using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ApplicationToSupportAndControlDiet.ViewModels;
using ApplicationToSupportAndControlDiet.Models;
using System.Collections.ObjectModel;
using ApplicationToSupportAndControlDiet.Views;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationToSupportAndControlDiet
{
    public sealed partial class MealsPage : Page
    {
        private ObservableCollection<Meal> items;
        private DayService dayService;
        private Repository<Meal> mealRepository;
        private Repository<Day> dayRepository;
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
            dayService = new DayService();
            mealRepository = new Repository<Meal>();
            dayRepository = new Repository<Day>();
            Repository<User> repo2 = new Repository<User>();
            User user = repo2.FindFirst();
            if (user == null)
            {
                WarningCal.Text = "Complete information about your profile";
            }
            items = new ObservableCollection<Meal>();
            InitializeMeals();
        }

        private void InitializeMeals()
        {
            UpdateDay();
            if (choosenDay != null)
            {
                items = new ObservableCollection<Meal>(choosenDay.MealsInDay.OrderBy(x => x.DateTimeOfMeal));
                if (items != null)
                {
                    this.ItemsList.ItemsSource = items;
                }
            }
            else
            {
                RefreshListWithMeals();
            }
        }

        private void RefreshListWithMeals()
        {
            this.ItemsList.ItemsSource = null;
            if (items != null)
            {
                items.Clear();
            }         
            this.ItemsList.ItemsSource = items;
        }

        private void UpdateDay()
        {
            choosenDay = dayService.FindDayByDate(Globals.Date.Value.DateTime);
        }

        private void NextDayClick(object sender, RoutedEventArgs e)
        {
            Date = Date.Value.AddDays(1);
            Globals.MainPage.NavigateTo(this);
        }

        private void PreviousDayClick(object sender, RoutedEventArgs e)
        {
            Date = Date.Value.AddDays(-1);
            Globals.MainPage.NavigateTo(this);
        }

        private void SaveAsProductClick(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            Frame.Navigate(typeof(AddNewProduct), thisMeal);
        }

        private void SaveAsCsvClick(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            CsvExport mealToCsv = new CsvExport(thisMeal);
            mealToCsv.ExportMealToCsvFile();
        }

        private void CreateNewFromThisMealClick(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            KeyValuePair<bool, Meal> parameters = new KeyValuePair<bool, Meal>(true, thisMeal);
            Frame.Navigate(typeof(AddMeal), parameters);
        }

        private void EditMealClick(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;       
            KeyValuePair<bool, Meal> parameters = new KeyValuePair<bool, Meal>(false, thisMeal);
            Frame.Navigate(typeof(AddMeal), parameters);
        }

        private void DeleteMealClick(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            Meal thisMeal = baseObject.DataContext as Meal;
            if (mealRepository.Delete(thisMeal) > -1)
            {
                items.Remove(thisMeal);
            }
            UpdateDay();
            if (choosenDay != null)
            {
                if (choosenDay.MealsInDay.Count == 0)
                {
                    dayRepository.Delete(choosenDay);
                }
            }
        }

        private void DataPickerDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (!sender.Date.Equals(Globals.Date) && sender.Date != null)
            {
                Date = sender.Date;
                InitializeMeals();
            }
        }
    }
}