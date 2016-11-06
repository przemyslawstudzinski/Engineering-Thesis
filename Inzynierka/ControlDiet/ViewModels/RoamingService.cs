using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class RoamingService
    {
        public const int MAX_SIZE_OF_ROAMING_DB = 92160;
        public const string NAME_OF_DATABASE_ROAMING_FILE = "roamingDb.sqlite";

        public static string RoamingDatabaseFilePath
        {
            get
            {
                return Path.Combine(ApplicationData.
                    Current.RoamingFolder.Path, NAME_OF_DATABASE_ROAMING_FILE);
            }
        }

        public static SQLiteConnection ConnectionToRoamingDatabase
        {
            get
            {
                return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(),
                    RoamingDatabaseFilePath);
            }
        }

        public static SQLiteAsyncConnection AsyncConnectionToRoamingDatabase
        {
            get
            {
                var connectionString = new SQLiteConnectionString(RoamingDatabaseFilePath, true);
                var connectionWithLock = new SQLiteConnectionWithLock(
                    new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), connectionString);
                return new SQLiteAsyncConnection(() => connectionWithLock);
            }
        }

        public static FileInfo RoamingDbFile
        {
            get
            {
                return new FileInfo(RoamingDatabaseFilePath);
            }
        }

        public RoamingService() { }

        public static void InitializeRoamingDb()
        {
            if (DatabaseConnection.CreateTables(ConnectionToRoamingDatabase))
            {
                SetSequenceOfProductsTable();
            }
        }

        public static void MergeDb(SQLiteConnection connectionToLocalDatabase)
        {
            connectionToLocalDatabase.Execute("ATTACH ? as dba", RoamingDatabaseFilePath);
            connectionToLocalDatabase.Execute("BEGIN");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Users select * from dba.Users");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Products select * from dba.Products");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Days select * from dba.Days");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Meals select * from dba.Meals");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Defined_products select * from dba.Defined_products");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Defined_product_meals select * from dba.Defined_product_meals");
            connectionToLocalDatabase.Execute("COMMIT");
            connectionToLocalDatabase.Execute("DETACH dba");
        }

        public void Save(object item, bool update)
        {
            if (RoamingDbFile.Length < MAX_SIZE_OF_ROAMING_DB)
            {
                if (update) AsyncConnectionToRoamingDatabase.InsertOrReplaceWithChildrenAsync(item, recursive: true);
                else AsyncConnectionToRoamingDatabase.InsertWithChildrenAsync(item, recursive: true);
            }
            else
            {
                RemoveOldDays();
                if (RoamingDbFile.Length < MAX_SIZE_OF_ROAMING_DB)
                {
                    if (update) AsyncConnectionToRoamingDatabase.InsertOrReplaceWithChildrenAsync(item, recursive: true);
                    else AsyncConnectionToRoamingDatabase.InsertWithChildrenAsync(item, recursive: true);
                }
                else
                {
                    RemoveOldProducts();
                    if (update) AsyncConnectionToRoamingDatabase.InsertOrReplaceWithChildrenAsync(item, recursive: true);
                    else AsyncConnectionToRoamingDatabase.InsertWithChildrenAsync(item, recursive: true);
                }
            }
        }

        private void RemoveOldDays()
        {
            Repository<Day> repository = new Repository<Day>();
            List<Day> lastWeek = GetLastWeek();
            ConnectionToRoamingDatabase.DeleteAll<Day>();
            ConnectionToRoamingDatabase.DeleteAll<Meal>();
            ConnectionToRoamingDatabase.DeleteAll<DefinedProduct>();
            ConnectionToRoamingDatabase.DeleteAll<DefinedProductMeal>();

            foreach (Day day in lastWeek)
            {
                repository.Save(day);
            }
        }

        private void RemoveOldProducts()
        {
            Repository<Product> repository = new Repository<Product>();
            List<Day> lastWeek = GetLastWeek();
            int removeOldestUserProduct = 0;
            ProductService productservice = new ProductService();
            List<Product> allProducts = productservice.GetAllProducts();

            while (RoamingDbFile.Length < MAX_SIZE_OF_ROAMING_DB)
            {
                ConnectionToRoamingDatabase.Delete<Product>(allProducts[removeOldestUserProduct]);
                //TODO compact database file
            }

            foreach (Day day in lastWeek)
            {
                foreach (Meal meal in day.MealsInDay)
                {
                    foreach (DefinedProduct definiedProduct in meal.ProductsInMeal)
                    {
                        repository.SaveOneOrReplace(definiedProduct.Product);
                    }
                }
            }
        }

        private List<Day> GetLastWeek()
        {
            DateTime actualDate = DateTime.Now;
            Repository<Day> repository = new Repository<Day>();
            DayService dayService = new DayService();
            int count = repository.CountAllRoaming();
            List<Day> lastWeek = new List<Day>();
            for (int i = 0; i < 7; i++)
            {
                Day day = dayService.FindDayByDate(actualDate.AddDays(-i));
                lastWeek.Add(day);
            }
            return lastWeek;
        }

        private static void SetSequenceOfProductsTable()
        {
            Repository<Product> repo = new Repository<Product>();
            int count = repo.CountAllLocal();
            string query = "INSERT INTO SQLITE_SEQUENCE(name,seq) VALUES (\"Products\"," + count + ")";
            ConnectionToRoamingDatabase.Execute(query);
        }

    }
}