using ApplicationToSupportAndControlDiet.Models;
using System.IO;
using Windows.Storage;
using SQLite.Net;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DatabaseConnection
    {
        private const string NAME_OF_DATABASE_FILE = "localDb.sqlite";
        private const string NAME_OF_DATABASE_ROAMING_FILE = "roamingDb.sqlite";
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

            connectionToDatabase.Execute("ATTACH ? as dba", roamingDatabaseFilePath);
            connectionToDatabase.Execute("BEGIN");
            connectionToDatabase.Execute("INSERT OR REPLACE INTO Products select * from dba.Products");
            connectionToDatabase.Execute("INSERT OR REPLACE INTO Days select * from dba.Days");
            connectionToDatabase.Execute("INSERT OR REPLACE INTO Meals select * from dba.Meals");
            connectionToDatabase.Execute("INSERT OR REPLACE INTO Defined_products select * from dba.Defined_products");
            connectionToDatabase.Execute("INSERT OR REPLACE INTO Defined_product_meals select * from dba.Defined_product_meals");
            connectionToDatabase.Execute("INSERT OR REPLACE INTO Users select * from dba.Users");
            connectionToDatabase.Execute("COMMIT");
            connectionToDatabase.Execute("DETACH dba");
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
                Repository<Product> repo = new Repository<Product>();
                int count = repo.CountAllLocal();
                string query = "INSERT INTO SQLITE_SEQUENCE(name,seq) VALUES (\"Products\"," + count +")";
                int modified = connectionToDatabase.Execute(query);
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
            if (!TableExists("Defined_products", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProduct>();
            }
            if (!TableExists("Defined_product_meals", connectionToDatabase))
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
