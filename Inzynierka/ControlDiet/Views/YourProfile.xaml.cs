using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ApplicationToSupportAndControlDiet
{
    public sealed partial class YourProfile : Page
    {
        private Boolean IsFailMessageSet;
        private Boolean IsSuccessMessageSet = false;
        private const string EMPTYMESSAGE = "Fill all the blank fields.";
        private const string VALUESMESSAGE = "{0} field value must be between {1} and {2}";
        private const string CONFIRMMESSAGE = "Adding user info successful. Daily calories requisition set: {0}";
        private const string KCAL_INFO = "Daily calories requisition set: {0}";
        private Style RedBorderStyle;
        private Style DefaultStyle;
    
        private void TextBoxNumeric_TextChanged(object sender, TextChangedEventArgs e)
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

        private void TextBoxInteger_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearConfirmValidationAndStyles();
            TextBox textBox = sender as TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c))
                {
                    newText += c;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        public T SelectedRadioValue<T>(T defaultValue, params RadioButton[] buttons)
        {
            foreach (RadioButton button in buttons)
            {
                if (button.IsChecked == true)
                {
                    if (button.Tag is string && typeof(T) != typeof(string))
                    {
                        string value = (string)button.Tag;
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                    return (T)button.Tag;
                }
            }

            return defaultValue;
        }

        public YourProfile()
        {
            this.InitializeComponent();
            RedBorderStyle = Application.Current.Resources["TextBoxError"] as Style;
            DefaultStyle = null;
            var repository = new Repository<User>();
            User user = repository.FindFirst();
            if (user != null)
            {
                if (user.Sex == Models.Sex.Male)
                {
                    Male.IsChecked = true;
                }
                else
                {
                    Female.IsChecked = true;
                }
                AgeBox.Text = user.Age.ToString();
                HeightBox.Text = user.Height.ToString();
                weightBox.Text = user.Weight.ToString();
                ActivityBox.SelectedValue = user.DailyActivity.ToString();
                GoalBox.SelectedValue = user.GoalOfWeightToAchieve.ToString();
                AddConfirm.Text = String.Format(KCAL_INFO, user.TotalDailyEnergyExpenditure.ToString());
            }
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxesAndMessages();
            int ageValue=0;
            if (ValidateAndCheckInRange(AgeBox, 5, 120))
            {
                ageValue = int.Parse(AgeBox.Text);
            }

            float heightValue=0;
            if(ValidateAndCheckInRange(HeightBox,40,300))
            {
                heightValue = float.Parse(HeightBox.Text);
            }
            float weightValue=0;
            if (ValidateAndCheckInRange(weightBox, 15, 500))
            {
                weightValue = float.Parse(weightBox.Text);
            }

            string sexValue = SelectedRadioValue<string>("Male", Male, Female);
            string activityValue = (string)ActivityBox.SelectedValue;
            string goalValue = (string)GoalBox.SelectedValue;
            Sex sexChoice;
            Enum.TryParse(sexValue, out sexChoice);
            ActivityLevel activityChoice;
            Enum.TryParse(activityValue, out activityChoice);
            UserGoal userGoalChoice;
            Enum.TryParse(goalValue, out userGoalChoice);
            User user = new User(1,sexChoice, ageValue, heightValue, weightValue, userGoalChoice, activityChoice);
            string caloriesValue = SelectedRadioValue<string>("Automatic", Automatic, Manual);
            int totalDailyEnergyExpenditure = 0;
            if (caloriesValue.Equals("Automatic"))
            {
                if (IsFailMessageSet == true) return;
                totalDailyEnergyExpenditure = CaloriesRequisitionCalculator.CalculateRequisitionOfCalories(user);
            }
            else
            {
                if (ValidateAndCheckInRange(CaloriesBox, 50, 10000))
                {
                    totalDailyEnergyExpenditure = int.Parse(CaloriesBox.Text);
                }        
            }
            if (IsFailMessageSet == true) return;
            user.TotalDailyEnergyExpenditure = totalDailyEnergyExpenditure;

            Repository<User> userRepository = new Repository<User>();
            if (userRepository.SaveOneOrReplace(user) > -1) {
                ClearTextBoxesAndSetConfirmMessage(totalDailyEnergyExpenditure);
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

        private Boolean ValidateAndCheckInRange(TextBox textBox, float min, float max) {
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
            float value = float.Parse(textBox.Text);     
            if (value >= min && value <= max)
            {
                return true;
            }
            else {
                textBox.Style = RedBorderStyle;
                AppendToMessages(String.Format(VALUESMESSAGE,textBox.Tag,min,max));
                if (!IsFailMessageSet)
                {
                    IsFailMessageSet = true;
                }
                return false;
            }
        }

        private void ClearTextBoxesAndMessages()
        {
            IsFailMessageSet = false;
            ValidationMessages.Text = String.Empty;
            ClearStyles();
        }

        private void ClearTextBoxesAndSetConfirmMessage(int dailyCaloriesExpenditure)
        {
            ClearStyles();
            if (!IsSuccessMessageSet)
            {
                AddConfirm.Text = String.Format(CONFIRMMESSAGE, dailyCaloriesExpenditure);
            }
           
        }

        private void ClearTextBoxesAndStyles()
        {
            ClearText();
            ClearStyles();
            IsFailMessageSet = false;
        }

        private void ClearStyles()
        {
            AgeBox.Style = DefaultStyle;
            HeightBox.Style = DefaultStyle;
            weightBox.Style = DefaultStyle;
        }

        private void ClearText() {
            ValidationMessages.Text = String.Empty;
            AddConfirm.Text = String.Empty;
            AgeBox.Text = String.Empty;
            HeightBox.Text = String.Empty;
            weightBox.Text = String.Empty;
        }

        private void AppendToMessages(string message)
        {
            ValidationMessages.Text += (message + Environment.NewLine);
        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            CaloriesBox.Visibility = Visibility.Visible;
        }

        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            CaloriesBox.Visibility = Visibility.Collapsed;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
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
    }
}