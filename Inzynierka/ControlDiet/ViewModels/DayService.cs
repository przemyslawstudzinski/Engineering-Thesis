using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DayService
    {
        private static SQLiteConnection connectionToDatabase { set; get; }

        public DayService()
        {
            connectionToDatabase = DatabaseConnection.GetConnection();
        }

        public int SaveDay(Day day)
        {
            return connectionToDatabase.InsertOrReplace(day);
        }

        public Day FindDay(DateTime dateTime)
        {
//            String query = "SELECT date
//FROM cars_sales_tbl
//WHERE date = "Hybrid"
            List<Day> list = connectionToDatabase.Table<Day>().Where(item => item.Date == dateTime).ToList();
            return list[0];
            //return connectionToDatabase.Find<Day>();
        }
    }
}
