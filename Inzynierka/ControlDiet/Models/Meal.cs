using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;


namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Meals")]
    public class Meal
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public long Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("time")]
        public DateTime TimeOfMeal { set; get; }

        public List<Product> ProductsInMeal { set; get; }
    }
}
