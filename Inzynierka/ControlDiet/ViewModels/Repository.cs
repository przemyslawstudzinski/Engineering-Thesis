using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class Repository<T> where T : class
    {
        private static SQLiteConnection connectionToLocalDatabase;
        private static SQLiteConnection connectionToRoamingDatabase;
        private static SQLiteAsyncConnection connectionToLocalDatabaseAsync;
        private static SQLiteAsyncConnection connectionToRoamingDatabaseAsync;        
        private RoamingService roaming;

        public Repository()
        {
            connectionToLocalDatabase = DatabaseConnection.ConnectionToLocalDatabase;
            connectionToRoamingDatabase = RoamingService.ConnectionToRoamingDatabase;
            connectionToLocalDatabaseAsync = DatabaseConnection.AsyncConnectionToLocalDatabase;
            connectionToRoamingDatabaseAsync = RoamingService.AsyncConnectionToRoamingDatabase;
            roaming = new RoamingService();
        }

        public int Save(T item)
        {
            connectionToLocalDatabaseAsync.InsertWithChildrenAsync(item, recursive: true);
            roaming.Save(item, false);
            return 1;
        }

        public int SaveOneOrReplace(T item)
        {
            connectionToRoamingDatabaseAsync.InsertOrReplaceAsync(item);
            connectionToLocalDatabaseAsync.InsertOrReplaceAsync(item);
            return 1;
        }

        public int Update(T item)
        {
            connectionToLocalDatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);
            roaming.Save(item, true);
            return 1;
        }

        public int Delete(T item)
        {
            connectionToLocalDatabaseAsync.DeleteAsync(item, recursive: true);
            connectionToRoamingDatabaseAsync.DeleteAsync(item, recursive: true);
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

        public T FindFirst() {
            T returnedObject = connectionToLocalDatabase.Table<T>().FirstOrDefault();
            return returnedObject;
        }

        public T FindById(int id) {
           T returnedObject = connectionToLocalDatabase.Find<T>(id);
           return returnedObject;
        }
    }
}