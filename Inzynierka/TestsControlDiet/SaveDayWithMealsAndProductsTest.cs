using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestsControlDiet
{
    [TestClass]
    public class SaveDayWithMealsAndProductsTest
    {
        [TestMethod]
        public void getDayWithMealsAndProductsTest()
        {
            Repository<Day> repository = new Repository<Day>();

            Product product = new Product();
            product.Name = "name";
            DefinedProduct definiedProduct = new DefinedProduct(product, 5);

            Meal meal = new Meal();
            meal.Name = "mealname";
            definiedProduct.Meal = meal;
            definiedProduct.MealId = meal.Id;
            meal.ProductsInMeal.Add(definiedProduct);

            Day day = new Day();
            meal.DayId = day.Id;
            meal.Day = day;
            day.MealsInDay.Add(meal);
            Repository<Day> sv = new Repository<Day>();
            sv.Save(day);

            Day dayAfterSave = repository.FindDay(day);
            Assert.AreEqual(dayAfterSave.MealsInDay.Count, 1, "There should be 1 meal in database");
            Assert.AreEqual(dayAfterSave.MealsInDay.ElementAt(0).ProductsInMeal.Count, 1, "There should be 1 definied product in database");
        }
    }
}
