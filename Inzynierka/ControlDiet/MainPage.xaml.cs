using ApplicationToSupportAndControlDiet.Views;
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
using ApplicationToSupportAndControlDiet.ViewModels;
using Windows.System.Profile;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ApplicationToSupportAndControlDiet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        

        public MainPage()
        {
            this.InitializeComponent();
            DatabaseConnection.CreateSqliteDatabase();
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
        private void SearchPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "SEARCH";
        }
        private void ProfilePaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "YOUR PROFILE";
            WorkSpace.Navigate(typeof(YourProfile));
        }
        private void AddNewProductPaneItem_Click(object sender, RoutedEventArgs e)
        {
            ApplicationName.Text = "ADD NEW PRODUCT";
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
        public void SelectedDay_Click(object sender, RoutedEventArgs e)
        {
            this.WorkSpace.Navigate(typeof(MealsPage));
        }

        public void NavigateTo<T>(T sourcePage) {
            this.WorkSpace.Navigate(typeof(T));
        }
    }
}
