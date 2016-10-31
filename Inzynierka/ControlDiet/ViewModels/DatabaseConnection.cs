using ApplicationToSupportAndControlDiet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DatabaseConnection
    {
        private const string NAME_OF_DATABASE_FILE = "db.sqlite";
        private const string NAME_OF_DATABASE_ROAMING_FILE = "roamingDB.sqlite";
        private const string NAME_OF_DIRECTORY = "Resources";

        public static void CreateSqliteDatabases()
        {
            string databaseFilePath = Path.Combine(ApplicationData.
                    Current.LocalFolder.Path, NAME_OF_DATABASE_FILE);
            SQLiteConnection connectionToDatabase = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);
            CreateTablesLocal(connectionToDatabase);

            string roamingDatabaseFilePath = Path.Combine(ApplicationData.
            Current.RoamingFolder.Path, NAME_OF_DATABASE_ROAMING_FILE);
            SQLiteConnection connectionToRoamingDatabase = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), roamingDatabaseFilePath);
            CreateTablesRoaming(connectionToRoamingDatabase);
        }

        public static SQLiteConnection GetConnection() {
            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.
        Current.LocalFolder.Path, NAME_OF_DATABASE_FILE);
            SQLiteConnection connectionToDatabase = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);
            return connectionToDatabase;
        }

        public static SQLiteConnection GetRoamingConnection() {
            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.
            Current.RoamingFolder.Path, NAME_OF_DATABASE_ROAMING_FILE);
            SQLiteConnection connectionToDatabase = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);
            return connectionToDatabase;
        }

        public static void CreateTablesRoaming(SQLiteConnection connectionToDatabase)
        {
            if(!TableExists("Days", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<Day>();
            }
            if (!TableExists("Meals", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<Meal>();
            }
            if (!TableExists("Products", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<Product>();
            }
            if (!TableExists("Users", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<User>();
            }
            if (!TableExists("DefinedProduct", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProduct>();
            }
            if (!TableExists("DefinedProductMeal", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProductMeal>();
            }

        }

        public static void CreateTablesLocal(SQLiteConnection connectionToDatabase)
        {
            if (!TableExists("Days", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<Day>();
            }
            if (!TableExists("Meals", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<Meal>();
            }
            if (!TableExists("Products", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<Product>();
                initializeDatabase(connectionToDatabase);
            }
            if (!TableExists("Users", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<User>();
            }
            if (!TableExists("DefinedProduct", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProduct>();
            }
            if (!TableExists("DefinedProductMeal", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProductMeal>();
            }

        }

        public static void initializeDatabase(SQLiteConnection connectionToDatabase) {
            string insertproducts = System.IO.File.ReadAllText(Path.Combine(
Directory.GetCurrentDirectory(), NAME_OF_DIRECTORY + "\\inserts.sql"));
            connectionToDatabase.Execute(insertproducts);
        }

        public static bool TableExists(string tableName, SQLiteConnection connetionToDatabase)
        {
            var tableInfo = connetionToDatabase.GetTableInfo(tableName);
            if(tableInfo.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
