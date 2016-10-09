using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Defined_products")]
    public class DefinedProduct : Product
    {
        [Column("quantity")]
        public int Quantity { set; get; }

        public DefinedProduct() { }

        public DefinedProduct(Product product, int quantity)
        {
            this.Name = product.Name;
            this.Energy = product.Energy;
            this.Protein = product.Protein;
            this.Carbohydrate = product.Carbohydrate;
            this.Fat = product.Fat;
            this.Fiber = product.Fiber;
            this.Sugar = product.Sugar;
            this.Category = product.Category;

            this.Quantity = quantity;
        }
    }
}
