using ApplicationToSupportAndControlDiet.Models;
using System;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class MeasureService
    {
        public const float GRAM_OF_PRODUCT_IN_DB = 100;
        public const float TEASPOON_IN_SPOON = 3;
        public const float TEASPOON_IN_GLASS = 16 * TEASPOON_IN_SPOON;

        public MeasureService() { }

        public void Calculate(DefinedProduct definedProduct, Product product)
        {
            Measure measure = definedProduct.Measure;
            switch (measure)
            {
                case Measure.Teaspoon:
                    definedProduct.Energy = (definedProduct.Quantity * product.WeightInTeaspoon) * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Protein = (definedProduct.Quantity * product.WeightInTeaspoon) * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Carbohydrate = (definedProduct.Quantity * product.WeightInTeaspoon) * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fat = (definedProduct.Quantity * product.WeightInTeaspoon) * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fiber = (definedProduct.Quantity * product.WeightInTeaspoon) * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Sugar = (definedProduct.Quantity * product.WeightInTeaspoon) * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;
                case Measure.Spoon:
                    definedProduct.Energy = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Protein = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Carbohydrate = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fat = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fiber = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Sugar = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;
                case Measure.Gram:
                    definedProduct.Energy = definedProduct.Quantity * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Protein = definedProduct.Quantity * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Carbohydrate = definedProduct.Quantity * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fat = definedProduct.Quantity * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fiber = definedProduct.Quantity * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Sugar = definedProduct.Quantity * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;
                case Measure.Glass:
                    definedProduct.Energy = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Protein = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Carbohydrate = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fat = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Fiber = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    definedProduct.Sugar = (definedProduct.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;
            }
        }
    }
}
