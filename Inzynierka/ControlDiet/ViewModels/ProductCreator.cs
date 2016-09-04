using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationToSupportAndControlDiet.Models;
using SQLite.Net;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    class ProductCreator
    {

        private static SQLiteConnection connectionToDatabase { set; get; }

        public ProductCreator()
        {
            connectionToDatabase = DatabaseConnection.GetConnection();
        }

        //returns inserted id
        public int SaveProduct(Product product)
        {
            return connectionToDatabase.InsertOrReplace(product);
        }
    }
}
