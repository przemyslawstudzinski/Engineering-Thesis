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
        public AddNewProduct()
        {
            this.InitializeComponent();
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
            string userName = NameBox.Text;
            float kcalValue;
            float.TryParse(KcalBox.Text, out kcalValue);
            float proteinValue;
            float.TryParse(ProteinBox.Text, out proteinValue);
            float carbohydrateValue;
            float.TryParse(CarbohydrateBox.Text, out carbohydrateValue);
            float fatValue;
            float.TryParse(FatBox.Text, out fatValue);
            float fiberValue;
            float.TryParse(FiberBox.Text, out fiberValue);
            float sugarvalue;
            float.TryParse(SugarBox.Text, out sugarvalue);
            Product product = new Product(userName, kcalValue,proteinValue,carbohydrateValue,fatValue,fiberValue,sugarvalue);
            ProductCreator productCreator = new ProductCreator();
            productCreator.SaveProduct(product);
        }
         
    }
}
