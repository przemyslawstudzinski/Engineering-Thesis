using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Defined_product_meals")]
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