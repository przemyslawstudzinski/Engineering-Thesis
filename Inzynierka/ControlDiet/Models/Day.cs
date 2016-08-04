using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Days")]
    public class Day
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public long Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("date")]
        public DateTime Date { set; get; }

        public List<Meal> MealsInDay { set; get; }
    }
}
