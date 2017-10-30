using ApplicationToSupportAndControlDiet.ViewModels;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Ingridient")]
    public class Ingridient
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("IngridientId")]
        public int IngridientId { get; set; }

        [Column("MealId")]
        [ForeignKey(typeof(Meal))]
        public int MealId { get; set; }

        [ManyToOne (CascadeOperations = CascadeOperation.All)]
        public Meal Meal { get; set; }

        [Column("ProductId")]
        [ForeignKey(typeof(Product))]
        public int ProductId { set; get; }

        private Product product;

        [ManyToOne (CascadeOperations = CascadeOperation.CascadeRead)]
        public Product Product
        {
            set
            {
                product = value;
                MeasureService measureService = new MeasureService();
                measureService.Calculate(this, product);
            }
            get
            {
                return product;
            }
        }

        [Column("QuantityInMeal")]
        public float QuantityInMeal { set; get; }

        [Column("MeasureOfQuantity")]
        public Measure MeasureOfQuantity { set; get; }

        [Ignore]
        public double Energy { get; set; }

        [Ignore]
        public double Protein { get; set; }

        [Ignore]
        public double Fat { get; set; }

        [Ignore]
        public double Carbohydrate { get; set; }

        [Ignore]
        public double Fiber { get; set; }

        [Ignore]
        public double Sugar { get; set; }

        public Ingridient() { }

        public Ingridient(Product product, float quantity, Measure measure)
        {
            this.ProductId = product.ProductId;
            this.Product = product;
            this.QuantityInMeal = quantity;
            this.MeasureOfQuantity = measure;
            MeasureService measureService = new MeasureService();
            measureService.Calculate(this, product);
        }

        public void Update(Measure? measure = null)
        {
            MeasureService measureService = new MeasureService();
            measureService.Calculate(this, product, measure);
        }
    }
}