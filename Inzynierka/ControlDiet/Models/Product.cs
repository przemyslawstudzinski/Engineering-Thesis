using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("ProductId")]
        public int ProductId { set; get; }

        [Column("Name")]
        public string Name { set; get; }

        [Column("Energy")]
        public double Energy { set; get; }

        [Column("Protein")]
        public double Protein { set; get; }

        [Column("Fat")]
        public double Fat { set; get; }

        [Column("Carbohydrate")]
        public double Carbohydrate { set; get; }

        [Column("Fiber")]
        public double Fiber { set; get; }

        [Column("Sugar")]
        public double Sugar { set; get; }

        [Column("Favourite")]
        public bool Favourite { set; get; }

        [Column("Dislike")]
        public bool DisLike { set; get; }

        [Column("WeightInTeaspoon")]
        public float WeightInTeaspoon { set; get; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Ingridient> Ingridients { get; set; }

        public Product()
        {
            Ingridients = new List<Ingridient>();
        }

        public Product(string name, float kcalValue, float proteinValue, float carbohydrateValue,
            float fatValue, float fiberValue, float sugarValue)
        {
            this.Name = name;
            this.Energy = kcalValue;
            this.Protein = proteinValue;
            this.Carbohydrate = carbohydrateValue;
            this.Fat = fatValue;
            this.Fiber = fiberValue;
            this.Sugar = sugarValue;
            this.Favourite = false;
            this.DisLike = false;
            this.WeightInTeaspoon = 0;

            Ingridients = new List<Ingridient>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}