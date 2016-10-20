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
using SQLiteNetExtensions.Extensions;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using System.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMeal : Page
    {
        ObservableCollection<Product> items;
        ObservableCollection<DefinedProduct> choosenProducts;

        ProductProvider productProvider;
        Meal newMeal;
        Product selectedProduct;
        private Boolean IsFailMessageSet;
        private const string EMPTYMESSAGE = "Fill all the blank fields.";
        private const string CONFIRMMESSAGE = "Adding meal successful.";
        private Style RedBorderStyleTextbox;
        private Style RedBorderStyleDate;
        private Style RedBorderStyleAutoSuggest;
        private Style DefaultStyle;

        Repository<Product> productRepository = new Repository<Product>();

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

        public AddMeal()
        {
            this.InitializeComponent();

            productProvider = new ProductProvider();
            items = new ObservableCollection<Product>();
            choosenProducts = new ObservableCollection<DefinedProduct>();
            this.SuggestProductsBox.ItemsSource = items;
            this.ItemsList.ItemsSource = choosenProducts;
            newMeal = new Meal();
            RedBorderStyleTextbox = Application.Current.Resources["TextBoxError"] as Style;
            RedBorderStyleDate = Application.Current.Resources["CalendarError"] as Style;
            RedBorderStyleAutoSuggest = Application.Current.Resources["AutoSuggestError"] as Style;
            DefaultStyle = null;
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
            this.QuantityBox.Text = String.Empty;
            this.SuggestProductsBox.Text = String.Empty;
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            var productToDelete = baseObject.DataContext as DefinedProduct;
            choosenProducts.Remove(productToDelete);
        }

        private void FavouriteProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            var selectedProduct = baseObject.DataContext as DefinedProduct;
            selectedProduct.Product.Favourite = true;
            selectedProduct.Favourite = true;
            RefreshListView();
            productRepository.Update(selectedProduct.Product);
            
        }

        private void UnFavoriteProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            var selectedProduct = baseObject.DataContext as DefinedProduct;
            selectedProduct.Product.Favourite = false;
            selectedProduct.Favourite = false;
            productRepository.Update(selectedProduct.Product);
            RefreshListView();           
        }

        private void DislikeProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            var selectedProduct = baseObject.DataContext as DefinedProduct;
            selectedProduct.Product.DisLike = true;
            selectedProduct.DisLike = true;
            productRepository.Update(selectedProduct.Product);
            RefreshListView();
        }

        private void LikeProduct_Click(object sender, RoutedEventArgs e)
        {
            var baseObject = sender as FrameworkElement;
            var selectedProduct = baseObject.DataContext as DefinedProduct;
            selectedProduct.Product.DisLike = false;
            selectedProduct.DisLike = false;
            productRepository.Update(selectedProduct.Product);
            RefreshListView();
        }

        private void RefreshListView()
        {
            this.ItemsList.ItemsSource = null;
            this.ItemsList.ItemsSource = choosenProducts;
        }

        private void ListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            //ListView listView = (ListView)sender;
            //allContactsMenuFlyout.ShowAt(listView, e.GetPosition(listView));
            //var a = ((FrameworkElement)e.OriginalSource).DataContext;
        }

        private async void SaveMeal_Click(object sender, RoutedEventArgs e)
        {
            Repository<Day> repository = new Repository<Day>();
            ClearTextBoxesStylesAndMessages();
            Meal meal = new Meal();
            if (ValidateEmpty(NameBox))
            {
                meal.Name = NameBox.Text;
            }
            if (ValidateChoosenProducts())
            {
                foreach (DefinedProduct element in choosenProducts)
                {
                    meal.ProductsInMeal.Add(element);
                    element.Meal = meal;
                    element.MealId = meal.Id;
                }
            }
            Day day = null;
            bool newItem = false;

            TimeSpan time = this.TimePicker.Time;
            DateTimeOffset date = this.DataPicker.Date.Value;
            DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
            meal.TimeOfMeal = dateTime;
            day = repository.FindDayByDate(dateTime);
            if (day == null)
            {
                day = new Day();
                day.Date = dateTime;
                newItem = true;
            }
            day.MealsInDay.Add(meal);
            meal.Day = day;
            meal.DayId = day.Id;

            if (IsFailMessageSet) return;
            if (newItem == true)
            {
                if (repository.Save(day) > -1)
                {
                    ClearTextBoxesAndSetConfirmMessage();
                }
            }
            else
            {
                if (repository.Update(day) > -1)
                {
                    ClearTextBoxesAndSetConfirmMessage();
                }
            }

            CsvExport daycsv = new CsvExport(meal);
            daycsv.ExportMealToCsvFile();
        }

        private Boolean ValidateEmpty(TextBox textBox)
        {
            if (textBox.Text.Length == 0)
            {
                if (!IsFailMessageSet)
                {
                    IsFailMessageSet = true;
                    AppendToMessages(EMPTYMESSAGE);
                }
                textBox.Style = RedBorderStyleTextbox;
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateChoosenProducts()
        {
            if (choosenProducts.Count == 0)
            {
                if (!IsFailMessageSet)
                {
                    IsFailMessageSet = true;
                    AppendToMessages(EMPTYMESSAGE);
                }
                SuggestProductsBox.Style = RedBorderStyleAutoSuggest;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void TextBoxNumeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void ClearTextBoxesStylesAndMessages()
        {
            ClearStyles();
            IsFailMessageSet = false;
            AddConfirm.Text = String.Empty;
            ValidationMessages.Text = String.Empty;
        }

        private void ClearTextBoxesAndSetConfirmMessage()
        {
            ClearText();
            ClearList();
            ClearStyles();
            AddConfirm.Text = CONFIRMMESSAGE;
        }

        private void ClearTextBoxesAndStyles()
        {
            ClearText();
            ClearList();
            ClearStyles();
            IsFailMessageSet = false;
        }

        private void ClearStyles()
        {
            NameBox.Style = DefaultStyle;
            SuggestProductsBox.Style = DefaultStyle;
        }

        private void ClearText()
        {
            AddConfirm.Text = String.Empty;
            ValidationMessages.Text = String.Empty;
            NameBox.Text = String.Empty;
            this.QuantityBox.Text = String.Empty;
            this.SuggestProductsBox.Text = String.Empty;
        }

        private void ClearList()
        {
            choosenProducts = new ObservableCollection<DefinedProduct>();
            this.ItemsList.ItemsSource = choosenProducts;
        }

        private void ClearMeal_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxesAndStyles();
        }

        private void AppendToMessages(string message)
        {
            ValidationMessages.Text += (message + Environment.NewLine);
        }

    }
}
