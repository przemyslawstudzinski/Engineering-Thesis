using System.Linq;
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestsControlDiet
{
    [TestClass]
    public class SaveDayWithMealsAndProductsTest
    {
        private Day dayAfterSave;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            DatabaseConnection.CreateSqliteDatabases();
        }

        [TestInitialize]
        public void SetUp()
        {
            Repository<Day> repository = new Repository<Day>();

            Product productOne = new Product();
            productOne.Name = "name 1";
            DefinedProduct definiedProductOne = new DefinedProduct(productOne, 5, Measure.Gram);

            Product productTwo = new Product();
            productTwo.Name = "name 2";
            DefinedProduct definiedProductTwo = new DefinedProduct(productTwo, 5, Measure.Spoon);

            Meal mealOne = new Meal();
            mealOne.Name = "mealname 1";
            definiedProductOne.Meals.Add(mealOne);
            mealOne.ProductsInMeal.Add(definiedProductOne);

            Meal mealTwo = new Meal();
            mealTwo.Name = "mealname 2";
            definiedProductTwo.Meals.Add(mealTwo);
            mealTwo.ProductsInMeal.Add(definiedProductTwo);

            Day day = new Day();
            mealOne.DayId = day.Id;
            mealOne.Day = day;
            mealTwo.DayId = day.Id;
            mealTwo.Day = day;
            day.MealsInDay.Add(mealOne);
            day.MealsInDay.Add(mealTwo);

            repository.SaveOneOrReplaceWithChildren(day);
            dayAfterSave = repository.FindById(day.Id);
        }

        [TestMethod]
        public void GetDayWithMealsAndProductsTest()
        {           
            Assert.AreEqual(dayAfterSave.MealsInDay.Count, 2);
        }

        [TestMethod]
        public void GetMealWithProductsTest()
        {
            Assert.AreEqual(dayAfterSave.MealsInDay.ElementAt(0).ProductsInMeal.Count, 1);
        }
    }
}