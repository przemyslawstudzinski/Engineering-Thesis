using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public long Id { set; get; }

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

        [ForeignKey(typeof(Meal))]
        public long DayId { set; get; }

        [ManyToOne]
        public Meal Meal { set; get; }

        [Column("category")]
        public ProductCategory Category { set; get; }

        public Product() { }
        public Product(string name, float kcalValue, float proteinValue, float carbohydrateValue,
            float fatValue, float fiberValue, float sugarValue, ProductCategory categoryValue)
        {
            this.Name = name;
            this.Energy = kcalValue;
            this.Protein = proteinValue;
            this.Carbohydrate = carbohydrateValue;
            this.Fat = fatValue;
            this.Fiber = fiberValue;
            this.Sugar = sugarValue;
            this.Category = categoryValue;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
