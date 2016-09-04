using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ApplicationToSupportAndControlDiet.Views;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

//namespace Inzynierka.Views
namespace ApplicationToSupportAndControlDiet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
        }

        private void ShowCalendar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddMealPaneItem_Click(object sender, RoutedEventArgs e)
        {
           //.WorkSpace.Navigate(typeof(AddMeal));
        }
        public void AddMeal2(object sender, RoutedEventArgs e)
        {
           // Border Tile2 = new Border();
            //Tile2.BorderThickness = new Thickness(2, 2, 2, 2);
            //Tile2.Width = 600;
            //Tile2.Height = 100;
           // Tile2.BorderBrush = new SolidColorBrush(Colors.Red);
            //Test.Children.Add(Tile2);
            RelativePanel Meal = new RelativePanel();
            Test.Children.Add(Meal);

            RelativePanel PanelTop = new RelativePanel();
            Meal.Children.Add(PanelTop);

            TextBlock MealName = new TextBlock();
            MealName.Text = "Nazwa";
            MealName.FontSize = 30;
            PanelTop.Children.Add(MealName);

            TextBlock MealHour = new TextBlock();
            MealHour.Text = "Godzina";
            MealHour.FontSize = 30;
            MealHour.HorizontalAlignment=HorizontalAlignment.Center;
            MealHour.Margin =new Thickness (300,0,0,0);
            MealHour.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            PanelTop.Children.Add(MealHour);

            RelativePanel PanelBottom = new RelativePanel();
            PanelBottom.SetValue(RelativePanel.BelowProperty, PanelTop);
    
            Meal.Children.Add(PanelBottom);

            TextBlock Total = new TextBlock();
            Total.Text = "Total of ";
            Total.FontSize = 20;
            PanelBottom.Children.Add(Total);

            TextBlock Kcal = new TextBlock();
            Kcal.Text = "Kcal : xxxxx";
            Kcal.FontSize = 20;
            Kcal.Margin = new Thickness(10, 0, 0, 0);
            Kcal.SetValue(RelativePanel.RightOfProperty, Total);
            PanelBottom.Children.Add(Kcal);

            TextBlock Carb = new TextBlock();
            Carb.Text = "Carbohydrate: xxxxx";
            Carb.FontSize = 20;
            Carb.Margin = new Thickness(10, 0, 0, 0);
            Carb.SetValue(RelativePanel.RightOfProperty, Kcal);
            PanelBottom.Children.Add(Carb);

            TextBlock Protein = new TextBlock();
            Protein.Text = "Protein: xxxxx";
            Protein.FontSize = 20;
            Protein.Margin = new Thickness(10, 0, 0, 0);
            Protein.SetValue(RelativePanel.RightOfProperty, Carb);
            PanelBottom.Children.Add(Protein);

            TextBlock Fat = new TextBlock();
            Fat.Text = "Fat: xxxxx";
            Fat.FontSize = 20;
            Fat.Margin = new Thickness(10, 0, 0, 0);
            Fat.SetValue(RelativePanel.RightOfProperty, Protein);
            PanelBottom.Children.Add(Fat);


        }
    }
}
