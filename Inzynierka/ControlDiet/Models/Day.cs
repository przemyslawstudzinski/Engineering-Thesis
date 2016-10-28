using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Days")]
    public class Day
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { set; get; }

        [Column("date")]
        public DateTime Date { set; get; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Meal> MealsInDay { set; get; }

        private float energy;

        public float Energy
        {
            get
            {
                energy = 0;
                foreach (Meal element in MealsInDay)
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
                foreach (Meal element in MealsInDay)
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
                foreach (Meal element in MealsInDay)
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
                foreach (Meal element in MealsInDay)
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
                foreach (Meal element in MealsInDay)
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
                foreach (Meal element in MealsInDay)
                {
                    fiber += element.Fiber;
                }
                return fiber;
            }
        }

        public Day()
        {
            MealsInDay = new List<Meal>();
        }
    }
}
