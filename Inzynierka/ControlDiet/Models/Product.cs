using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public long ProductId { set; get; }

        [Column("name")]
        public string Name { set; get; }
    }
}
