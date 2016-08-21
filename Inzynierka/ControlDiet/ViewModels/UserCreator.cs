using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationToSupportAndControlDiet.ViewModels;
using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class UserCreator
    {
        private static SQLiteConnection connectionToDatabase { set; get; }

        public UserCreator()
        {
            connectionToDatabase = DatabaseConnection.GetConnection();
        }

        //Returns inserted user id.
        public int SaveUser(User user) {
           return connectionToDatabase.InsertOrReplace(user);
        }
    }
}
