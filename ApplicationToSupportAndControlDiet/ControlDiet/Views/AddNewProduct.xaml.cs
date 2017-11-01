using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.Views
{
    public sealed partial class AddNewProduct : Page
    {
        private Boolean IsFailMessageSet;
        private Boolean IsSuccessMessageSet = false;
        private const string EMPTYMESSAGE = "Fill all the blank fields.";
        private const string CONFIRMMESSAGE = "Adding product successful.";
        private Style RedBorderStyle;
        private Style DefaultStyle;

        public AddNewProduct()
        {
            this.InitializeComponent();
            RedBorderStyle = Application.Current.Resources["TextBoxError"] as Style;
            DefaultStyle = NameBox.Style;
        }

        private void TextBoxNumericTextChanged(object sender, TextChangedEventArgs e)
        {
            ClearConfirmValidationAndStyles();
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

        private void NameBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            ClearConfirmValidationAndStyles();
        }

        private void SaveProductClick(object sender, RoutedEventArgs e)
        {
            ClearTextBoxesStylesAndMessages();
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
            if (IsFailMessageSet) return;
            Product product = new Product(userName, kcalValue, proteinValue, carbohydrateValue, fatValue, fiberValue, sugarvalue);
            Repository<Product> productCreator = new Repository<Product>();
            if (productCreator.Save(product) > -1) {
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
                textBox.Style = RedBorderStyle;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ClearTextBoxesStylesAndMessages() {
            ClearStyles();
            IsFailMessageSet = false;
            AddConfirm.Text = String.Empty;
            ValidationMessages.Text = String.Empty;
        }

        private async void ClearTextBoxesAndSetConfirmMessage() {
            ClearText();
            ClearStyles();
            IsSuccessMessageSet = true;
            AddConfirm.Text = CONFIRMMESSAGE;
            await Task.Delay(500);
            IsSuccessMessageSet = false;
        }

        private void ClearTextBoxesAndStyles()
        {
            ClearText();
            ClearStyles();
            IsFailMessageSet = false;
        }

        private void ClearStyles() {
            NameBox.Style = DefaultStyle;
            KcalBox.Style = DefaultStyle;
            ProteinBox.Style = DefaultStyle;
            CarbohydrateBox.Style = DefaultStyle;
            FatBox.Style = DefaultStyle;
            FiberBox.Style = DefaultStyle;
            SugarBox.Style = DefaultStyle;
        }

        private void ClearText()
        {
            AddConfirm.Text = String.Empty;
            ValidationMessages.Text = String.Empty;
            NameBox.Text = String.Empty;
            KcalBox.Text = String.Empty;
            ProteinBox.Text = String.Empty;
            CarbohydrateBox.Text = String.Empty;
            FatBox.Text = String.Empty;
            FiberBox.Text = String.Empty;
            SugarBox.Text = String.Empty;
        }

        private void AppendToMessages(string message)
        {
            ValidationMessages.Text += (message + Environment.NewLine);
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ClearTextBoxesAndStyles();
        }

        private void ClearConfirmValidationAndStyles()
        {
            if (!IsSuccessMessageSet)
            {
                AddConfirm.Text = String.Empty;
            }
            ValidationMessages.Text = String.Empty;
            ClearStyles();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                Meal mealFromMealsPage = e.Parameter as Meal;
                this.NameBox.Text = mealFromMealsPage.MealName;
                this.CarbohydrateBox.Text = mealFromMealsPage.Carbohydrate.ToString();
                this.FatBox.Text = mealFromMealsPage.Fat.ToString();
                this.ProteinBox.Text = mealFromMealsPage.Protein.ToString();
                this.KcalBox.Text = mealFromMealsPage.Energy.ToString();
                this.SugarBox.Text = mealFromMealsPage.Sugar.ToString();
                this.FiberBox.Text = mealFromMealsPage.Fiber.ToString();
            }
        }
    }
}