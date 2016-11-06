using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;

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
        public void GetAllProductsTest()
        {
            var productProvider = new ProductService();
            List<Product> allProducts = productProvider.GetAllProducts();
            Assert.AreEqual(allProducts.Count, 934, "There should be 934 products in database");
        }

        [TestMethod]
        public void GetProductsWithPatternTest()
        {
            var productProvider = new ProductService();
            List<Product> allProducts = productProvider.GetProductsLike("bev");
            Assert.AreEqual(allProducts.Count, 49, "There should be only 49 records with substring 'bev' in name");
        }
    }
}
