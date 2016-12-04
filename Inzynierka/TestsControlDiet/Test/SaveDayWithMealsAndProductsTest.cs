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
            Ingridient definiedProductOne = new Ingridient(productOne, 5, Measure.Gram);

            Product productTwo = new Product();
            productTwo.Name = "name 2";
            Ingridient definiedProductTwo = new Ingridient(productTwo, 5, Measure.Spoon);

            Meal mealOne = new Meal();
            mealOne.Name = "mealname 1";
            definiedProductOne.Meal = mealOne;
            mealOne.IngridientsInMeal.Add(definiedProductOne);

            Meal mealTwo = new Meal();
            mealTwo.Name = "mealname 2";
            definiedProductTwo.Meal = mealTwo;
            mealTwo.IngridientsInMeal.Add(definiedProductTwo);

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
        public void ShouldGetDayWithMealsAndProductsTest()
        {           
            Assert.AreEqual(dayAfterSave.MealsInDay.Count, 2);
        }

        [TestMethod]
        public void ShouldGetMealWithProductsTest()
        {
            Assert.AreEqual(dayAfterSave.MealsInDay.ElementAt(0).IngridientsInMeal.Count, 1);
        }
    }
}