using ApplicationToSupportAndControlDiet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite.Net;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DatabaseConnection
    {
        private const string NAME_OF_DATABASE_FILE = "db.sqlite";
        private const string NAME_OF_DIRECTORY = "Resources";

        public static void CreateSqliteDatabase()
        {
           // StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
           // StorageFolder resources = await appInstalledFolder.GetFolderAsync(NAME_OF_DIRECTORY);
           // var database = await resources.GetFileAsync(NAME_OF_DATABASE_FILE);

            //StorageFolder storageFile = ApplicationData.Current.LocalFolder;

            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.
                    Current.LocalFolder.Path, NAME_OF_DATABASE_FILE);
            SQLiteConnection connectionToDatabase = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);

            CreateTables(connectionToDatabase);
        }

        public static SQLiteConnection GetConnection() {
            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.
        Current.LocalFolder.Path, NAME_OF_DATABASE_FILE);
            SQLiteConnection connectionToDatabase = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);
            return connectionToDatabase;
        }

        public static void CreateTables(SQLiteConnection connectionToDatabase)
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
                string insertproducts = System.IO.File.ReadAllText(Path.Combine(
                Directory.GetCurrentDirectory(), NAME_OF_DIRECTORY + "\\inserts.sql"));
                connectionToDatabase.Execute(insertproducts);
            }
            if (!TableExists("Users", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<User>();
            }
            if (!TableExists("DefinedProduct", connectionToDatabase))
            {
                connectionToDatabase.CreateTable<DefinedProduct>();
            }

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
