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
        public List<DefinedProduct> ProductsInMeal { set; get; }

        private float energy;

        public float Energy
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

        private float protein;

        public float Protein
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

        private float carbohydrate;

        public float Carbohydrate
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

        private float fat;

        public float Fat
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

        private float sugar;

        public float Sugar
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

        private float fiber;

        public float Fiber
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
