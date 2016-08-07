using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

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

        [ForeignKey(typeof(Day))]  
        public long DayId { set; get; }

        [ManyToOne]
        public Day Day { set; get; }

        [OneToMany]
        public List<Product> ProductsInMeal { set; get; }
    }
}
