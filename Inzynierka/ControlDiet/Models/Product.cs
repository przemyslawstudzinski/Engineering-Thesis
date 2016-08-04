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

        [Unique]
        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { set; get; }

        [Column("energy")]
        public float Energy { set; get; }

        [Column("protein")]
        public float Protein { set; get; }

        [Column("fat")]
        public float Fat { set; get; }

        [Column("carbohydrate")]
        public float Carbohydrate { set; get; }

        [Column("fiber")]
        public float Fiber { set; get; }

        [Column("sugar")]
        public float Sugar { set; get; }

        [Column("category")]
        public ProductCategory Category { set; get; }
    }
}
