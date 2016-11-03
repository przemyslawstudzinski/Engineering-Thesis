using SQLite.Net.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("energy")]
        public double Energy { set; get; }

        [Column("protein")]
        public double Protein { set; get; }

        [Column("fat")]
        public double Fat { set; get; }

        [Column("carbohydrate")]
        public double Carbohydrate { set; get; }

        [Column("fiber")]
        public double Fiber { set; get; }

        [Column("sugar")]
        public double Sugar { set; get; }

        [Column("favourite")]
        public bool Favourite { set; get; }

        [Column("dislike")]
        public bool DisLike { set; get; }

        [Column("weight_in_teaspoon")]
        public float WeightInTeaspoon { set; get; }

        public Product() { }

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
        }

        public override string ToString()
        {
            return Name;
        }
    }
}