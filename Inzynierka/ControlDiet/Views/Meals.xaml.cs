using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ApplicationToSupportAndControlDiet.ViewModels;
using ApplicationToSupportAndControlDiet.Models;
using System.Collections.ObjectModel;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MealsPage : Page
    {
        private ObservableCollection<Meal> items;
        private Repository<Meal> mealRepository = new Repository<Meal>();

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
            items = new ObservableCollection<Meal>(mealRepository.FindAll());
            this.ItemsList.ItemsSource = items;
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

        }

        private void SaveAsCsv_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateNewFromThisMeal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditMeal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMeal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
