using ApplicationToSupportAndControlDiet.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsControlDiet.Test
{
    [TestClass]
    public class MeasureServiceTest
    {
        Product product;

        [TestInitialize]
        public void SetUp()
        {
            product = new Product();
            product.Energy = 1000;
            product.Fat = 10;
            product.Fiber = 10;
            product.Protein = 10;
            product.Sugar = 10;
            product.Carbohydrate = 10;
            product.Name = "product1";
            product.WeightInTeaspoon = 5;
        }

        [TestMethod]
        public void CalculateValuesWithMeasureGramTest()
        {
            DefinedProduct definiedProduct = new DefinedProduct(product, 200, Measure.Gram);
            Assert.AreEqual(definiedProduct.Energy, 2 * product.Energy);
            Assert.AreEqual(definiedProduct.Protein, 2 * product.Protein);
            Assert.AreEqual(definiedProduct.Fat, 2 * product.Fat);
            Assert.AreEqual(definiedProduct.Sugar, 2 * product.Sugar);
            Assert.AreEqual(definiedProduct.Carbohydrate, 2 * product.Carbohydrate);
            Assert.AreEqual(definiedProduct.Fiber, 2 * product.Fiber);
        }

        [TestMethod]
        public void CalculateValuesWithMeasureTeaspoonTest()
        {
            DefinedProduct definiedProduct = new DefinedProduct(product, 2, Measure.Teaspoon);
            Assert.AreEqual(definiedProduct.Energy, (product.Energy/100) * 2 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Protein, (product.Protein / 100) * 2 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Fiber, (product.Fiber / 100) * 2 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Fat, (product.Fat / 100) * 2 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Carbohydrate, (product.Carbohydrate / 100) * 2 * product.WeightInTeaspoon);       
            Assert.AreEqual(definiedProduct.Sugar, (product.Sugar / 100) * 2 * product.WeightInTeaspoon);
        }

        [TestMethod]
        public void CalculateValuesWithMeasureSpoonTest()
        {
            DefinedProduct definiedProduct = new DefinedProduct(product, 2, Measure.Spoon);
            Assert.AreEqual(definiedProduct.Energy, (product.Energy / 100) * 6 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Protein, (product.Protein / 100) * 6 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Fat, (product.Fat / 100) * 6 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Carbohydrate, (product.Carbohydrate / 100) * 6 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Sugar, (product.Sugar / 100) * 6 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Fiber, (product.Fiber / 100) * 6 * product.WeightInTeaspoon);
        }

        [TestMethod]
        public void CalculateValuesWithMeasureGlassTest()
        {
            DefinedProduct definiedProduct = new DefinedProduct(product, 1, Measure.Glass);
            Assert.AreEqual(definiedProduct.Energy, (product.Energy / 100) * 16 * 3 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Protein, (product.Protein / 100) * 16 * 3 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Sugar, (product.Sugar / 100) * 16 * 3 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Fiber, (product.Fiber / 100) * 16 * 3 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Fat, (product.Fat / 100) * 16 * 3 * product.WeightInTeaspoon);
            Assert.AreEqual(definiedProduct.Carbohydrate, (product.Carbohydrate / 100) * 16 * 3 * product.WeightInTeaspoon);
        }
    }
}
