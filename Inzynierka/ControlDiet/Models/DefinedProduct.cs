using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using ApplicationToSupportAndControlDiet.ViewModels;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Defined_products")]
    public class DefinedProduct
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { set; get; }
        
        [Column("quantity")]
        public float Quantity { set; get; }

        [Column("measure")]
        public Measure Measure { set; get; }

        [ForeignKey(typeof(Product))]
        public int ProductId { set; get; }

        private Product product;

        [OneToOne]
        public Product Product {
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

        [ManyToMany(typeof(DefinedProductMeal))]
        public List<Meal> Meals { set; get; }

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

        public DefinedProduct()
        { 
            Meals = new List<Meal>();
        }

        public DefinedProduct(Product product, float quantity, Measure measure)
        {
            this.ProductId = product.Id;
            this.Product = product;
            this.Quantity = quantity;
            this.Measure = measure;
            MeasureService measureService = new MeasureService();
            measureService.Calculate(this, product);

            Meals = new List<Meal>();
        }
    }
}
