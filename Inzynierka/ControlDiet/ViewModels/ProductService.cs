using SQLite.Net;
using ApplicationToSupportAndControlDiet.Models;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class ProductService
    {
        private static SQLiteConnection connectionToLocalDatabase { set; get; }

        public ProductService() {
            connectionToLocalDatabase = DatabaseConnection.ConnectionToLocalDatabase;
        }

        public List<Product> GetAllProducts() {
            string query = @"SELECT * FROM Products";
            List<Product> allProducts = connectionToLocalDatabase.Query<Product>(query);
            return allProducts;
        }

        public List<Product> GetProductsLike(string input) {
            string queryWithName = @"SELECT * FROM Products product where product.name LIKE '%" + input + "%'" ;
            List<Product> allProducts = connectionToLocalDatabase.Query<Product>(queryWithName);
            var query = from x in allProducts
                         orderby x.Favourite descending, x.DisLike ascending
                         select x;            
            return query.ToList();
        }
    }
}