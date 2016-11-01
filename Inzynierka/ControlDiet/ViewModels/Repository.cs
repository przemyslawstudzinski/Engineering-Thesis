using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            connectionToDatabase.InsertWithChildren(item, recursive: true);
            return 1;
        }

        public int SaveOneOrReplace(T item)
        {
            connectionToRoamingDatabase.InsertOrReplace(item);
            return connectionToDatabase.InsertOrReplace(item);
        }

        public int Delete(T item)
        {
            connectionToDatabase.Delete(item, recursive: true);
            connectionToRoamingDatabase.Delete(item, recursive: true);
            return 1;
        }

        public int Update(T item)
        {
            connectionToRoamingDatabase.InsertOrReplaceWithChildren(item, recursive: true);
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
