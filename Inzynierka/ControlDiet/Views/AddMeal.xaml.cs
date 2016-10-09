using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class AddMeal : Page
    {
        ObservableCollection<Product> items;
        ObservableCollection<DefinedProduct> choosenProducts;

        ProductProvider productProvider;
        Meal newMeal;
        Product selectedProduct;

        public AddMeal()
        {
            this.InitializeComponent();

            productProvider = new ProductProvider();
            items = new ObservableCollection<Product>();
            choosenProducts = new ObservableCollection<DefinedProduct>();
            this.SuggestProductsBox.ItemsSource = items;
            this.ItemsList.ItemsSource = choosenProducts;
            newMeal = new Meal();
        }

        private void SuggestProducts_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            selectedProduct = args.SelectedItem as Product;
        }

        private void SuggestProducts_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            items.Clear();
            List<Product> result = productProvider.GetProductsLike(sender.Text);
            result.ForEach(x => items.Add(x));
        }

        private void AddDefinedProduct_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            Int32.TryParse(this.QuantityBox.Text, out quantity);
            DefinedProduct definedProduct = new DefinedProduct(selectedProduct, quantity);
            choosenProducts.Add(definedProduct);
            newMeal.ProductsInMeal.Add(definedProduct);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            var productToDelete = baseObject.DataContext as DefinedProduct;
            choosenProducts.Remove(productToDelete);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyTimeIsAvailable(TimePicker.Time) == true)
            {
                Control1Output.Text = string.Format("Thank you. Your appointment is set for {0}.",
                   TimePicker.Time.ToString());
            }
            else
            {
                Control1Output.Text = "Sorry, we're only open from 8AM to 5PM.";
            }
        }
        private bool VerifyTimeIsAvailable(TimeSpan time)
        {

            // Set open (8AM) and close (5PM) times. 
            TimeSpan openTime = new TimeSpan(8, 0, 0);
            TimeSpan closeTime = new TimeSpan(17, 0, 0);

            if (time >= openTime && time < closeTime)
            {
                return true; // Open 
            }
            return false; // Closed 
        }

        private void SaveMeal_Click(object sender, RoutedEventArgs e)
        {
            Meal p = newMeal;
        }

        private void CancelMeal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
