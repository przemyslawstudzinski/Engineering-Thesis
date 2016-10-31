using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsControlDiet
{
    [TestClass]
    public class ProductProviderTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            DatabaseConnection.CreateSqliteDatabases();
        }

        [TestMethod]
        public void getAllProductsTest()
        {
            var productProvider = new ProductProvider();
            List<Product> allProducts = productProvider.GetAllProducts();
            Assert.AreEqual(allProducts.Count, 960, "There should be 960 products in database");
        }

        [TestMethod]
        public void getProductsWithPatternTest()
        {
            var productProvider = new ProductProvider();
            List<Product> allProducts = productProvider.GetProductsLike("bev");
            Assert.AreEqual(allProducts.Count, 49, "There should be only 49 records with substring 'bev' in name");
        }
    }
}
