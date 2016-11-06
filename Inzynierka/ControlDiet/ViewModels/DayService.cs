using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DayService
    {
        private static SQLiteConnection connectionToLocalDatabase;

        public DayService()
        {
            connectionToLocalDatabase = DatabaseConnection.ConnectionToLocalDatabase;
        }

        public Day FindDayByDate(DateTime dateTime)
        {
            List<Day> list = connectionToLocalDatabase.GetAllWithChildren<Day>(recursive: true)
                .Where(item => item.Date.Date == dateTime.Date).ToList();
            if (list.Count != 0)
            {
                Repository<Product> repo = new Repository<Product>();
                foreach (Meal meal in list[0].MealsInDay)
                {
                    foreach (DefinedProduct element in meal.ProductsInMeal)
                    {

                        element.Product = repo.FindById(element.ProductId);
                    }
                }

                return list[0];
            }
            return null;
        }

    }
}