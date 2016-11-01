using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage;
using ApplicationToSupportAndControlDiet.Models;
using Windows.Storage.Pickers;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class CsvExport
    {
        private Meal meal;

        public CsvExport(Meal mealToExport)
        {
            this.meal = mealToExport;
        }

        public async void ExportMealToCsvFile()
        {
            FileSavePicker saveWindow = new FileSavePicker();
            saveWindow.SuggestedFileName = "products.csv";
            saveWindow.DefaultFileExtension = ".csv";
            saveWindow.FileTypeChoices.Add(".csv", new List<String> { ".csv" });
            StorageFile file = await saveWindow.PickSaveFileAsync();
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, ExportMeal());
            }          
        }

        private string ExportMeal()
        {
            var sb = new StringBuilder();
            sb.Append("List of products in " + meal.Name + ": ");
            sb.AppendLine();
            sb.AppendLine();
            foreach (DefinedProduct product in meal.ProductsInMeal)
            {
                sb.Append(EditValue(product.Product.Name));
                sb.Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                sb.AppendLine();
            }           
            return sb.ToString();
        }

        private string EditValue(String value)
        {
            if (value == null) return "";
            if (value.Contains("&"))
            {
                value = value.Replace("&", " ");
            }
            if (value.Contains(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator))
            {
                value = value.Replace(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, " -");
            }
            return value;
        }
    }
}
