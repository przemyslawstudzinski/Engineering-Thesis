using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationToSupportAndControlDiet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YourProfile : Page
    {
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

        private void TextBoxInteger_TextChanged(object sender, TextChangedEventArgs e)
        {
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
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            string userName = NameBox.Text;
            int ageValue = Int32.Parse(AgeBox.Text);
            float heightValue = float.Parse(HeightBox.Text);
            float weightValue = float.Parse(weightBox.Text);
            string sexValue = SelectedRadioValue<string>("Male", Male, Female);
            string activityValue = ActivityBox.Text;
            string goalValue = GoalBox.Text;
            Sex sexChoice;
            Enum.TryParse(sexValue, out sexChoice);
            //TODO
            //Slider doesn't work so for now I assume that user inputs correct enum type. 
            ActivityLevel activityChoice;
            Enum.TryParse(activityValue, out activityChoice);
            UserGoal userGoalChoice;
            Enum.TryParse(goalValue, out userGoalChoice);
            User user = new User(userName, sexChoice, ageValue, heightValue, weightValue, userGoalChoice, activityChoice);
            string caloriesValue = SelectedRadioValue<string>("Automatic", Automatic, Manual);
            int totalDailyEnergyExpenditure;
            if (caloriesValue.Equals("Automatic"))
            {
                totalDailyEnergyExpenditure = RequisitionOfCalories.CalculateRequisitionOfCalories(user);
            }
            else
            {
                totalDailyEnergyExpenditure = int.Parse(CaloriesBox.Text);                
            }
            user.TotalDailyEnergyExpenditure = totalDailyEnergyExpenditure;

            UserCreator userCreator = new UserCreator();
            userCreator.SaveUser(user);
        }
    }
}
