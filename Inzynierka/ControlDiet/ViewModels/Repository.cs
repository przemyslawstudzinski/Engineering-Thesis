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
        private static SQLiteConnection connectionToLocalDatabase;
        private static SQLiteConnection connectionToRoamingDatabase;
        private RoamingService roaming;

        public Repository()
        {
            connectionToLocalDatabase = DatabaseConnection.ConnectionToLocalDatabase;
            connectionToRoamingDatabase = RoamingService.ConnectionToRoamingDatabase;
            roaming = new RoamingService();
        }

        public int Save(T item)
        {
            connectionToLocalDatabase.InsertWithChildren(item, recursive: true);
            if (RoamingService.RoamingDbFile.Length < RoamingService.MAX_SIZE_OF_ROAMING_DB)
            {
                connectionToRoamingDatabase.InsertWithChildren(item, recursive: true);
            }
            else
            {
                roaming.RemoveOldDays();
                if (RoamingService.RoamingDbFile.Length < RoamingService.MAX_SIZE_OF_ROAMING_DB)
                {
                    connectionToRoamingDatabase.InsertWithChildren(item, recursive: true);
                }
                else
                {
                    roaming.RemoveOldProducts();
                    if (RoamingService.RoamingDbFile.Length < RoamingService.MAX_SIZE_OF_ROAMING_DB)
                    {
                        connectionToRoamingDatabase.InsertWithChildren(item, recursive: true);
                    }
                }
            }
            return 1;
        }

        public int SaveOneOrReplace(T item)
        {
            connectionToRoamingDatabase.InsertOrReplace(item);
            return connectionToLocalDatabase.InsertOrReplace(item);
        }

        public int Delete(T item)
        {
            connectionToLocalDatabase.Delete(item, recursive: true);
            connectionToRoamingDatabase.Delete(item, recursive: true);
            return 1;
        }

        public int Update(T item)
        {
            connectionToRoamingDatabase.InsertOrReplaceWithChildren(item, recursive: true);
            connectionToLocalDatabase.InsertOrReplaceWithChildren(item, recursive: true);
            return 1;
        }

        public List<T> FindAllLocal()
        {
            List<T> items = connectionToLocalDatabase.GetAllWithChildren<T>().ToList();
            return items;
        }

        public List<T> FindAllRoaming()
        {
            List<T> items = connectionToRoamingDatabase.GetAllWithChildren<T>().ToList();
            return items;
        }

        public int CountAllLocal() {
            return connectionToLocalDatabase.Table<T>().Count();
        }

        public int CountAllRoaming()
        {
            return connectionToRoamingDatabase.Table<T>().Count();
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
            List<Day> list = connectionToLocalDatabase.GetAllWithChildren<Day>(recursive: true)
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
            User user = connectionToLocalDatabase.Table<User>().FirstOrDefault();
            return user;
        }

        public T FindById(int id) {
           T returnedObject = connectionToLocalDatabase.Find<T>(id);
           return returnedObject;
        }
    }
}