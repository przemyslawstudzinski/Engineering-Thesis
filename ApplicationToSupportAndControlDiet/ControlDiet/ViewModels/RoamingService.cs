using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class RoamingService
    {
        public const int MAX_SIZE_OF_ROAMING_DB = 92160;
        public const int INITIAL_PRODUCT_QUANTITY_IN_DB = 934;
        public const int DAYS_TO_SAVE = 7;

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
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO User select * from dba.User");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Product select * from dba.Product");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Day select * from dba.Day");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Meal select * from dba.Meal");
            connectionToLocalDatabase.Execute("INSERT OR REPLACE INTO Ingridient select * from dba.Ingridient");
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
                RemoveOldData();
                if (RoamingDbFile.Length < MAX_SIZE_OF_ROAMING_DB)
                {
                    if (update) AsyncConnectionToRoamingDatabase.InsertOrReplaceWithChildrenAsync(item, recursive: true);
                    else AsyncConnectionToRoamingDatabase.InsertWithChildrenAsync(item, recursive: true);
                }
            }
        }

        private void RemoveOldData()
        {
            Repository<Day> dayRepository = new Repository<Day>();
            Repository<Product> productRepository = new Repository<Product>();
            DateTime actualDate = DateTime.Now;

            List<Day> list = ConnectionToRoamingDatabase.GetAllWithChildren<Day>(recursive: true)
            .Where(item => item.Date.Date < actualDate.Date.AddDays(-DAYS_TO_SAVE)).ToList();
            if (list.Count != 0)
            {
                foreach (Day day in list)
                {
                    foreach (Meal meal in day.MealsInDay)
                    {
                        foreach (Ingridient ingridient in meal.IngridientsInMeal)
                        {
                            if (ingridient.ProductId < INITIAL_PRODUCT_QUANTITY_IN_DB)
                            {
                                productRepository.Delete(ingridient.Product);
                            }
                        }
                    }
                    dayRepository.Delete(day);
                }
            }
            ConnectionToRoamingDatabase.Execute("vacuum"); //Force clear on database after deleting  
            if (RoamingDbFile.Length > MAX_SIZE_OF_ROAMING_DB)
            {
                ConnectionToRoamingDatabase.DeleteAll<Product>();
                ConnectionToRoamingDatabase.DeleteAll<Day>();
                ConnectionToRoamingDatabase.DeleteAll<Meal>();
                ConnectionToRoamingDatabase.DeleteAll<Ingridient>();
                ConnectionToRoamingDatabase.Execute("vacuum");
            }
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