using ApplicationToSupportAndControlDiet.Models;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class MeasureService
    {
        private const double GRAM_OF_PRODUCT_IN_DB = 100;
        private const double TEASPOON_IN_SPOON = 3;
        private const double TEASPOON_IN_GLASS = 16 * TEASPOON_IN_SPOON;

        public MeasureService() { }

        public void Calculate(Ingridient ingridient, Product product, Measure? otherMeasure = null)
        {
            if (product == null || ingridient == null)
            {
                return;
            }
            Measure measure;
            if (otherMeasure != null)
            {
                measure = (Measure) otherMeasure;
            }
            else
            {
                measure = ingridient.Measure;
            }
           
            switch (measure)
            {
                case Measure.Teaspoon:
                    ingridient.Energy = (ingridient.Quantity * product.WeightInTeaspoon) * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Protein = (ingridient.Quantity * product.WeightInTeaspoon) * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Carbohydrate = (ingridient.Quantity * product.WeightInTeaspoon) * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fat = (ingridient.Quantity * product.WeightInTeaspoon) * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fiber = (ingridient.Quantity * product.WeightInTeaspoon) * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Sugar = (ingridient.Quantity * product.WeightInTeaspoon) * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;

                case Measure.Spoon:
                    ingridient.Energy = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Protein = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Carbohydrate = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fat = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fiber = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Sugar = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_SPOON)) * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;

                case Measure.Gram:
                    ingridient.Energy = ingridient.Quantity * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Protein = ingridient.Quantity * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Carbohydrate = ingridient.Quantity * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fat = ingridient.Quantity * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fiber = ingridient.Quantity * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Sugar = ingridient.Quantity * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;

                case Measure.Glass:
                    ingridient.Energy = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Energy / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Protein = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Protein / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Carbohydrate = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Carbohydrate / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fat = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Fat / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Fiber = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Fiber / GRAM_OF_PRODUCT_IN_DB);
                    ingridient.Sugar = (ingridient.Quantity * (product.WeightInTeaspoon * TEASPOON_IN_GLASS)) * (product.Sugar / GRAM_OF_PRODUCT_IN_DB);
                    break;
            }
        }
    }
}