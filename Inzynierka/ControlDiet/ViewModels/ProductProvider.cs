using ApplicationToSupportAndControlDiet.ViewModels;
using SQLite.Net;
using ApplicationToSupportAndControlDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class ProductProvider
    {
        private SQLiteConnection connectionToDatabase { set; get; }

        public ProductProvider() {
            connectionToDatabase = DatabaseConnection.GetConnection();
        }

        public List<Product> getAllProducts() {
            string query = "SELECT * FROM Products";
            List<Product> allProducts = connectionToDatabase.Query<Product>(query);
            return allProducts;
        }

        public List<Product> getProductsLike(string input) {
            string query = "SELECT * FROM Products product where product.name LIKE '%" + input + "%'" ;
            List<Product> allProducts = connectionToDatabase.Query<Product>(query);
            return allProducts;
        }

    }
}
