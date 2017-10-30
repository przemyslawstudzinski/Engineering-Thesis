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

        public int SaveOneOrReplaceWithChildren(T item)
        {
            connectionToRoamingDatabase.InsertOrReplaceWithChildren(item, recursive: true);
            connectionToLocalDatabase.InsertOrReplaceWithChildren(item, recursive: true);
            return 1;
        }

        public int SaveOneOrReplace(T item)
        {
            connectionToRoamingDatabase.InsertOrReplace(item);
            connectionToLocalDatabase.InsertOrReplace(item);
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
            List<T> items = connectionToLocalDatabase.GetAllWithChildren<T>(recursive: true).ToList();
            return items;
        }

        public List<T> FindAllRoaming()
        {
            List<T> items = connectionToRoamingDatabase.GetAllWithChildren<T>(recursive: true).ToList();
            return items;
        }

        public int CountAllLocal()
        {
            return connectionToLocalDatabase.Table<T>().Count<T>();
        }

        public int CountAllRoaming()
        {
            return connectionToRoamingDatabase.Table<T>().Count<T>();
        }

        public T FindFirst()
        {
            return connectionToLocalDatabase.Table<T>().FirstOrDefault<T>();
        }

        public T FindById(int id)
        {
           return connectionToLocalDatabase.FindWithChildren<T>(id, recursive: true);          
        }
    }
}