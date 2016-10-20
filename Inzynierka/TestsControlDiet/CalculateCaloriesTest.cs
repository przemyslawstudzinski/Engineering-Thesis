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
    public class CalculateCaloriesTest
    {
        [TestMethod]
        public void calculateCaloriesFromMealAndDay() {
            Product productOne = new Product();
            productOne.Name = "name One";
            productOne.Energy = 2000;
            DefinedProduct definiedProductOne = new DefinedProduct(productOne, 10);

            Product productTwo = new Product();
            productTwo.Name = "name Two";
            productTwo.Energy = 1000;
            DefinedProduct definiedProductTwo = new DefinedProduct(productTwo, 50);

            Product productThree = new Product();
            productThree.Name = "name Three";
            productThree.Energy = 3000;
            DefinedProduct definedProductThree = new DefinedProduct(productThree, 70);

            Meal mealOne = new Meal();
            mealOne.Name = "mealname 1";
            mealOne.ProductsInMeal.Add(definiedProductOne);
            mealOne.ProductsInMeal.Add(definiedProductTwo);

            Meal mealTwo = new Meal();
            mealTwo.Name = "mealname 2";
            mealTwo.ProductsInMeal.Add(definedProductThree);

            Day day = new Day();
            day.MealsInDay.Add(mealOne);
            day.MealsInDay.Add(mealTwo);

            Assert.AreEqual(3000, mealOne.Energy, "Calculated energy from that meal should be equal to ");
            Assert.AreEqual(6000, day.Energy, "Calculated energy from that day should be equal to ");
        }
    }
}
