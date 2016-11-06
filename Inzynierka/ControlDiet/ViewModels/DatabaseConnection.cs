using ApplicationToSupportAndControlDiet.Models;
using System.IO;
using Windows.Storage;
using SQLite.Net;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DatabaseConnection
    {
        private const string NAME_OF_DATABASE_FILE = "localDb.sqlite";
        private const string NAME_OF_DIRECTORY = "Resources";

        public static string LocalDatabaseFilePath
        {
            get
            {
                return Path.Combine(ApplicationData.
                    Current.LocalFolder.Path, NAME_OF_DATABASE_FILE);
            }
        }

        public static SQLiteConnection ConnectionToLocalDatabase
        {
            get
            {
                return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), 
                    LocalDatabaseFilePath);
            }
        }

        public static void CreateSqliteDatabases()
        {
            if (CreateTables(ConnectionToLocalDatabase))
            {
                InsertProducts();
            }

            RoamingService.InitializeRoamingDb();
            RoamingService.MergeDb(ConnectionToLocalDatabase);
        }

        public static bool CreateTables(SQLiteConnection connectionToDatabase)
        {
            bool productsTableCreated = false;
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
                productsTableCreated = true;
            }
            if (!TableExists("Users", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<User>();
            }
            if (!TableExists("Defined_products", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProduct>();
            }
            if (!TableExists("Defined_product_meals", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProductMeal>();
            }
            return productsTableCreated;
        }

        private static void InsertProducts()
        {
            string insertproducts = File.ReadAllText(Path.Combine(
            Directory.GetCurrentDirectory(), NAME_OF_DIRECTORY + "\\inserts.sql"));
            ConnectionToLocalDatabase.Execute(insertproducts);
        }

        private static bool TableExists(string tableName, SQLiteConnection connetionToDatabase)
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