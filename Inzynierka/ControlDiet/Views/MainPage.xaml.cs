using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ApplicationToSupportAndControlDiet.ViewModels;

namespace ApplicationToSupportAndControlDiet.Views
{
    public sealed partial class MainPage : Page
    {     
        public MainPage()
        {
            this.InitializeComponent();
            DatabaseConnection.CreateSqliteDatabases();
            Globals.MainPage = this;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            this.MySplitView.IsPaneOpen = this.MySplitView.IsPaneOpen ? false : true;
        }
       
        private void MealsPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "CONTROL DIET";
            this.WorkSpace.Navigate(typeof(MealsPage));
        }

        private void AddMealPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ADD MEAL";

           this.WorkSpace.Navigate(typeof(AddMeal));
        }
        private void ProfilePaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "YOUR PROFILE";
            WorkSpace.Navigate(typeof(YourProfile));
        }
        private void AddNewProductPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ADD PRODUCT";
            WorkSpace.Navigate(typeof(AddNewProduct));
        }
        private void StatisticsPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "STATISTICS";
            WorkSpace.Navigate(typeof(Statistics));
        }
        private void HelpPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "HELP";
            WorkSpace.Navigate(typeof(Help));
        }
        private void AboutPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ABOUT";
            WorkSpace.Navigate(typeof(About));
        }

        public void NavigateTo<T>(T sourcePage) {
            this.WorkSpace.Navigate(typeof(T));
        }
    }
}