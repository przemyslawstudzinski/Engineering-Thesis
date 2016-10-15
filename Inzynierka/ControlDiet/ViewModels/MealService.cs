using System;
using System.Collections.Generic;
using SQLite.Net;
using ApplicationToSupportAndControlDiet.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    class MealService
    {

        private static SQLiteConnection connectionToDatabase { set; get; }

        public MealService() {
            connectionToDatabase = DatabaseConnection.GetConnection();
        }

        public List<Meal> GetAllMeals()
        {
            string query = @"SELECT * FROM Meals";
            List<Meal> allMeals = connectionToDatabase.Query<Meal>(query);
            return allMeals;
        }

        public int SaveMeal(Meal meal)
        {
            return connectionToDatabase.InsertOrReplace(meal);
        }
    }
}
