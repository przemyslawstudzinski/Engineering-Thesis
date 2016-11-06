using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using ApplicationToSupportAndControlDiet.Models;
using System.IO;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class RoamingSpaceManager
    {
        private const int LAST_N_PRODUCTS = 80;

        public static int LeaveLastNProducts(SQLiteConnection connectionToDatabase)
        {
            return connectionToDatabase.Execute("delete from Products where id not in (select id from Products order by id desc limit ?)", LAST_N_PRODUCTS);
        }

        public static int ClearMeals(SQLiteConnection connectionToDatabase)
        {
            int deletedDays = 0;
            FileInfo fileInfo = new FileInfo(DatabaseConnection.RoamingDbPath);
            DateTime currentDate = DateTime.Now;
            Repository<Day> repo = new Repository<Day>();
            if (fileInfo.Length > 99000) //Length returns file size in bytes
            {                
                List<Day> list = connectionToDatabase.GetAllWithChildren<Day>(recursive: true)
                     .Where(item => item.Date.Date < currentDate.Date.AddDays(-7) || item.Date.Date > currentDate.Date.AddDays(7)).ToList();
                if (list.Count != 0)
                {

                    foreach (Day day in list)
                    {
                        repo.Delete(day);
                        deletedDays++;
                    }
                }
            }
            connectionToDatabase.Execute("vacuum"); //Force clear on database after deleting
            //Further check if previous deleting didnt delete enough
            while (new FileInfo(DatabaseConnection.RoamingDbPath).Length > 99000) {
                List<Day> list = connectionToDatabase.Query<Day>("select * from Days order by date ASC LIMIT 1");
                if (list.Count != 0)
                {
                    if (list[0].Date.Date < currentDate.Date)
                    {
                        repo.Delete(list[0]);
                        connectionToDatabase.Execute("vacuum");
                        deletedDays++;
                    }
                    else break;
                }
                else break;
            }
            return deletedDays;
        }

    }
}
