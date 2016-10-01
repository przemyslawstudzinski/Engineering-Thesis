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
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewProduct : Page
    {

        private Boolean IsEmptyMessageSet;
        private const string EMPTYMESSAGE = "Fill all the blank fields.";
        private Style RedBorderStyle;
        private Style DefaultStyle;

        public AddNewProduct()
        {
            this.InitializeComponent();
            RedBorderStyle = Application.Current.Resources["TextBoxError"] as Style;
            DefaultStyle = NameBox.Style;
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

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxesAndMessages();
            string userName="";
            if (ValidateEmpty(NameBox))
            {
                userName = NameBox.Text;
            }
            float kcalValue = 0;
            if (ValidateEmpty(KcalBox))
            {
                float.TryParse(KcalBox.Text, out kcalValue);
            }
            float proteinValue=0;
            if (ValidateEmpty(ProteinBox))
            {
                float.TryParse(ProteinBox.Text, out proteinValue);
            }
            float carbohydrateValue=0;
            if (ValidateEmpty(CarbohydrateBox))
            {
                float.TryParse(CarbohydrateBox.Text, out carbohydrateValue);
            }
            float fatValue=0;
            if (ValidateEmpty(FatBox))
            {
                float.TryParse(FatBox.Text, out fatValue);
            }
            float fiberValue=0;
            if (ValidateEmpty(FiberBox))
            {
                float.TryParse(FiberBox.Text, out fiberValue);
            }
            float sugarvalue=0;
            if (ValidateEmpty(SugarBox))
            {
                float.TryParse(SugarBox.Text, out sugarvalue);
            }
            if (IsEmptyMessageSet) return;
            Product product = new Product(userName, kcalValue, proteinValue, carbohydrateValue, fatValue,
                fiberValue, sugarvalue, ProductCategory.UserProducts);
            ProductCreator productCreator = new ProductCreator();
            productCreator.SaveProduct(product);
        }

        private Boolean ValidateEmpty(TextBox textBox)
        {
            if (textBox.Text.Length == 0)
            {
                if (!IsEmptyMessageSet)
                {
                    IsEmptyMessageSet = true;
                    AppendToMessages(EMPTYMESSAGE);
                }
                textBox.Style = RedBorderStyle;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ClearTextBoxesAndMessages() {
            IsEmptyMessageSet = false;
            ValidationMessages.Text = "";
            NameBox.Style = DefaultStyle;
            KcalBox.Style = DefaultStyle;
            ProteinBox.Style = DefaultStyle;
            CarbohydrateBox.Style = DefaultStyle;
            FatBox.Style = DefaultStyle;
            FiberBox.Style = DefaultStyle;
            SugarBox.Style = DefaultStyle;
        }

        private void AppendToMessages(string message)
        {
            ValidationMessages.Text += (message + Environment.NewLine);
        }

    }
}
