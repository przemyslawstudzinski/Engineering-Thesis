using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("DefinedProduct_Meal")]
    public class DefinedProductMeal
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        [ForeignKey(typeof(DefinedProduct))]
        public int DefinedProductId { get; set; }

        [Column("meal_id")]
        [ForeignKey(typeof(Meal))]
        public int MealId { get; set; }

        public DefinedProductMeal() { }
    }
}