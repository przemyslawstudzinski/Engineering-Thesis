using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ApplicationToSupportAndControlDiet.ViewModels;
using Windows.UI.ViewManagement;
using Windows.System.Profile;

namespace ApplicationToSupportAndControlDiet.Views
{
    public sealed partial class MainPage : Page
    {     
        public MainPage()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                Window.Current.SetTitleBar(null);
            }
            else
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            }
            DatabaseConnection.CreateSqliteDatabases();
            Globals.MainPage = this;
        }

        private void HamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            this.MySplitView.IsPaneOpen = this.MySplitView.IsPaneOpen ? false : true;
        }
       
        private void MealsPaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "CONTROL DIET";
            this.WorkSpace.Navigate(typeof(MealsPage));
        }

        private void AddMealPaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ADD MEAL";

           this.WorkSpace.Navigate(typeof(AddMeal));
        }
        private void ProfilePaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "YOUR PROFILE";
            WorkSpace.Navigate(typeof(YourProfile));
        }
        private void AddNewProductPaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ADD NEW PRODUCT";
            WorkSpace.Navigate(typeof(AddNewProduct));
        }
        private void StatisticsPaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "STATISTICS";
            WorkSpace.Navigate(typeof(Statistics));
        }
        private void HelpPaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "HELP";
            WorkSpace.Navigate(typeof(Help));
        }
        private void AboutPaneItemClick(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ABOUT";
            WorkSpace.Navigate(typeof(About));
        }

        public void NavigateTo<T>(T sourcePage) {
            this.WorkSpace.Navigate(typeof(T));
        }
    }
}