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
        [TestMethod]
        public void getAllProductsTest()
        {
            var productProvider = new ProductProvider();
            List<Product> allProducts = productProvider.getAllProducts();
            Assert.Equals(allProducts.Count, 960);
        }
    }
}
