using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("DefinedProductMeal")]
    public class DefinedProductMeal
    {
        [PrimaryKey]
        [AutoIncrement]
        [NotNull]
        [Column("id")]
        public int Id { get; set; }

        [Column("productId")]
        [ForeignKey(typeof(DefinedProduct))]
        public int DefinedProductId { get; set; }

        [Column("mealId")]
        [ForeignKey(typeof(Meal))]
        public int MealId { get; set; }

        public DefinedProductMeal() { }
    }
}
