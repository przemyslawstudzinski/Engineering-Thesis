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
        private Boolean IsFailMessageSet;
        private const string EMPTYMESSAGE = "Fill all the blank fields.";
        private const string CONFIRMMESSAGE = "Adding meal successful.";
        private Style RedBorderStyleTextbox;
        private Style RedBorderStyleDate;
        private Style RedBorderStyleAutoSuggest;
        private Style DefaultStyle;

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
            ClearTextBoxesStylesAndMessages();
            Meal meal = new Meal();
            if (ValidateEmpty(NameBox))
            {
                meal.Name = NameBox.Text;
            }
                TimeSpan time = this.TimePicker.Time;
                DateTimeOffset date = this.DataPicker.Date.Value;
                DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
                meal.TimeOfMeal = dateTime;
                DayService serviceOfDays = new DayService();
                Day d1 = new Day();
                d1.Date = dateTime;
                //Its probably better to save the date if all fields are succesfuly validated
                serviceOfDays.SaveDay(d1);
                Day d2 = serviceOfDays.FindDay(dateTime);
            if (ValidateChoosenProducts())
            {
                meal.ProductsInMeal = new List<DefinedProduct>(choosenProducts);
            }
            if (IsFailMessageSet) return;
            MealService mealService = new MealService();
            if (mealService.SaveMeal(meal) > -1)
            {
                ClearTextBoxesAndSetConfirmMessage();
            }
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
            ClearTextAndList();
            ClearStyles();
            AddConfirm.Text = CONFIRMMESSAGE;
        }

        private void ClearTextBoxesAndStyles()
        {
            ClearTextAndList();
            ClearStyles();
            IsFailMessageSet = false;
        }

        private void ClearStyles()
        {
            NameBox.Style = DefaultStyle;
            SuggestProductsBox.Style = DefaultStyle;
        }

        private void ClearTextAndList()
        {
            AddConfirm.Text = String.Empty;
            ValidationMessages.Text = String.Empty;
            NameBox.Text = String.Empty;
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
