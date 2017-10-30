using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Linq;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class DayService
    {
        private static SQLiteConnection connectionToLocalDatabase;

        public DayService()
        {
            connectionToLocalDatabase = DatabaseConnection.ConnectionToLocalDatabase;
        }

        public Day FindDayByDate(DateTime dateTime)
        {
            Day day = connectionToLocalDatabase.GetAllWithChildren<Day>(recursive: true)
                .Where(item => item.Date.Date == dateTime.Date).FirstOrDefault<Day>();
            return day;
        }

    }
}