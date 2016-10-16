using ApplicationToSupportAndControlDiet.ViewModels;
using SQLite.Net;
using ApplicationToSupportAndControlDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class ProductProvider
    {
        private static SQLiteConnection connectionToDatabase { set; get; }

        public ProductProvider() {
            connectionToDatabase = DatabaseConnection.GetConnection();
        }

        public List<Product> GetAllProducts() {
            string query = @"SELECT * FROM Products";
            List<Product> allProducts = connectionToDatabase.Query<Product>(query);
            return allProducts;
        }

        public List<Product> GetProductsLike(string input) {
            string queryWithName = @"SELECT * FROM Products product where product.name LIKE '%" + input + "%'" ;
            List<Product> allProducts = connectionToDatabase.Query<Product>(queryWithName);
            var query = from d in allProducts
                         orderby d.Favourite descending, d.DisLike ascending
                         select d;            
            return query.ToList();
        }
    }
}
