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

            Product productOne = new Product();
            productOne.Name = "name 1";
            DefinedProduct definiedProductOne = new DefinedProduct(productOne, 5);

            Product productTwo = new Product();
            productTwo.Name = "name 2";
            DefinedProduct definiedProductTwo = new DefinedProduct(productTwo, 5);

            Meal mealOne = new Meal();
            mealOne.Name = "mealname 1";
            definiedProductOne.Meal = mealOne;
            definiedProductOne.MealId = mealOne.Id;
            mealOne.ProductsInMeal.Add(definiedProductOne);

            Meal mealTwo = new Meal();
            mealTwo.Name = "mealname 2";
            definiedProductTwo.Meal = mealTwo;
            definiedProductTwo.MealId = mealTwo.Id;
            mealTwo.ProductsInMeal.Add(definiedProductTwo);

            Day day = new Day();
            mealOne.DayId = day.Id;
            mealOne.Day = day;
            mealTwo.DayId = day.Id;
            mealTwo.Day = day;
            day.MealsInDay.Add(mealOne);
            day.MealsInDay.Add(mealTwo);
            repository.Save(day);

            Day dayAfterSave = repository.FindDay(day);
            Assert.AreEqual(dayAfterSave.MealsInDay.Count, 2, "There should be 2 meals binded to the day");
            Assert.AreEqual(dayAfterSave.MealsInDay.ElementAt(0).ProductsInMeal.Count, 1, "There should be 1 definied product in meal");
        }
    }
}
