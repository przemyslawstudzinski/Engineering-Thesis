using ApplicationToSupportAndControlDiet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DatabaseConnection
    {
        private const string NAME_OF_DATABASE_FILE = "db.sqlite";
        private const string NAME_OF_DIRECTORY = "Resources";
        public static async void ConnectToSqliteDatabase()
        {
            //var res = Windows.ApplicationModel;
            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //StorageFolder resources = await appInstalledFolder.GetFolderAsync(NAME_OF_DIRECTORY);
            //var database = await resources.GetFileAsync(NAME_OF_DATABASE_FILE);

            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.
                    Current.LocalFolder.Path, "db.sqlite");
            SQLite.Net.SQLiteConnection connetionToDatabase = new SQLite.Net.SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);

            //connetionToDatabase.CreateTable<Product>();
            Product p = new Product();
            p.Name = "sdfsd";
            p.Protein = 3443;
            connetionToDatabase.Insert(p);
        }
    }
}
