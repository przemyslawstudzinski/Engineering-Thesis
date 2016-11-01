using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using ApplicationToSupportAndControlDiet.ViewModels;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Defined_products")]
    public class DefinedProduct : Product
    {
        [Column("quantity")]
        public float Quantity { set; get; }

        [Column("measure")]
        public Measure Measure { set; get; }

        [ForeignKey(typeof(Product))]
        public int ProductId { set; get; }

        [OneToOne]
        public Product Product { set; get; }

        [ManyToMany(typeof(DefinedProductMeal))]
        public List<Meal> Meals { set; get; }

        public DefinedProduct()
        { 
            Meals = new List<Meal>();
        }

        public DefinedProduct(Product product, float quantity, Measure measure)
        {
            this.Name = product.Name;
            this.Code = product.Code;
            this.Category = product.Category;
            this.Favourite = product.Favourite;
            this.DisLike = product.DisLike;

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
