using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class Repository<T> where T : class
    {
        private static SQLiteConnection connectionToDatabase { set; get; }
        private static SQLiteConnection connectionToRoamingDatabase { set; get; }

        public Repository()
        {
            connectionToDatabase = DatabaseConnection.GetConnection();
            connectionToRoamingDatabase = DatabaseConnection.GetRoamingConnection();
        }

        public int Save(T item)
        {
            connectionToRoamingDatabase.InsertWithChildren(item, recursive: true);
            SaveSpaceOnRoamingDB(item);
            connectionToDatabase.InsertWithChildren(item, recursive: true);
            return 1;
        }

        public int SaveOneOrReplace(T item)
        {
            connectionToRoamingDatabase.InsertOrReplace(item);
            SaveSpaceOnRoamingDB(item);
            return connectionToDatabase.InsertOrReplace(item);
        }

        private void SaveSpaceOnRoamingDB(T item) {
            if (item.GetType() == typeof(Product))
            {
                RoamingSpaceManager.LeaveLastNProducts(connectionToRoamingDatabase);
            }
            else if (item.GetType() == typeof(Day)) {
                RoamingSpaceManager.ClearMeals(connectionToRoamingDatabase);
            }
        }

        public int Delete(T item)
        {
            connectionToDatabase.Delete(item, recursive: true);
            connectionToRoamingDatabase.Delete(item, recursive: true);
            if (item.GetType() == typeof(Day)) {
                string query = "delete from Defined_product_meals where meal_id not in (select id from Meals)";
                connectionToDatabase.Execute(query);
                connectionToRoamingDatabase.Execute(query);
            }
            return 1;
        }

        public int Update(T item)
        {
            connectionToRoamingDatabase.InsertOrReplaceWithChildren(item, recursive: true);
            SaveSpaceOnRoamingDB(item);
            connectionToDatabase.InsertOrReplaceWithChildren(item, recursive: true);
            return 1;
        }

        public List<T> FindAllLocal()
        {
            List<T> items = connectionToDatabase.GetAllWithChildren<T>().ToList();
            return items;
        }

        public List<T> FindAllRoaming()
        {
            List<T> items = connectionToRoamingDatabase.GetAllWithChildren<T>().ToList();
            return items;
        }

        public int CountAllLocal() {
            return connectionToDatabase.Table<T>().Count();
        }

        public int CountAllRoaming()
        {
            return connectionToRoamingDatabase.Table<T>().Count();
        }


        public Day FindDayByDate(DateTime dateTime)
        {
            List<Day> list = connectionToDatabase.GetAllWithChildren<Day>(recursive: true)
                .Where(item => item.Date.Date == dateTime.Date).ToList();
            if (list.Count != 0)
            {
                Repository<Product> repo = new Repository<Product>();
                foreach (Meal meal in list[0].MealsInDay)
                {
                    foreach (DefinedProduct element in meal.ProductsInMeal) {

                        element.Product = repo.FindById(element.ProductId);
                    }
                }
                
                return list[0];
            }
            return null;
        }

        public Day FindDay(Day day)
        {
            List<Day> list = connectionToDatabase.GetAllWithChildren<Day>(recursive: true)
                .Where(x => x.Id == day.Id).ToList();
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

        public User FindUser() {
            User user = connectionToDatabase.Table<User>().FirstOrDefault();
            return user;
        }

        public T FindById(int id) {
           T returnedObject = connectionToDatabase.Find<T>(id);
           return returnedObject;
        }
    }
}
