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
        public int Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("time")]
        public DateTime DateTimeOfMeal { set; get; }

        public String Time
        {
            get
            {
                return DateTimeOfMeal.ToLocalTime().TimeOfDay.ToString(@"hh\:mm");
            }
        }

        [ForeignKey(typeof(Day))]
        public int DayId { set; get; }

        [ManyToOne]
        public Day Day { set; get; }

        [ManyToMany(typeof(DefinedProductMeal), CascadeOperations = CascadeOperation.All)]
        public List<DefinedProduct> ProductsInMeal { set; get; }

        private double energy;

        public double Energy
        {
            get
            {
                energy = 0;
                foreach (DefinedProduct element in ProductsInMeal)
                {
                    energy += element.Energy;
                }
                return energy;
            }
        }

        private double protein;

        public double Protein
        {
            get
            {
                protein = 0;
                foreach (DefinedProduct element in ProductsInMeal)
                {
                    protein += element.Protein;
                }
                return protein;
            }
        }

        private double carbohydrate;

        public double Carbohydrate
        {
            get
            {
                carbohydrate = 0;
                foreach (DefinedProduct element in ProductsInMeal)
                {
                    carbohydrate += element.Carbohydrate;
                }
                return carbohydrate;
            }
        }

        private double fat;

        public double Fat
        {
            get
            {
                fat = 0;
                foreach (DefinedProduct element in ProductsInMeal)
                {
                    fat += element.Fat;
                }
                return fat;
            }
        }

        private double sugar;

        public double Sugar
        {
            get
            {
                sugar = 0;
                foreach (DefinedProduct element in ProductsInMeal)
                {
                    sugar += element.Sugar;
                }
                return sugar;
            }
        }

        private double fiber;

        public double Fiber
        {
            get
            {
                fiber = 0;
                foreach (DefinedProduct element in ProductsInMeal)
                {
                    fiber += element.Fiber;
                }
                return fiber;
            }
        }

        public Meal()
        {
            ProductsInMeal = new List<DefinedProduct>();
        }
    }
}
