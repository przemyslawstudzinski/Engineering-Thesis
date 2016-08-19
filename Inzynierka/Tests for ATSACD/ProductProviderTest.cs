using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ApplicationToSupportAndControlDiet.ViewModels;
using ApplicationToSupportAndControlDiet.Models;
using System.Collections.Generic;

namespace Tests_for_ATSACD
{
    [TestClass]
    public class ProductProviderTest
    {
        [TestMethod]
        public void getAllProductsTest()
        {
            var productProvider = new ProductProvider();
            List<Product> allProducts = productProvider.getAllProducts();
            Assert.IsTrue(allProducts.Count == 960);
        }
    }
}
