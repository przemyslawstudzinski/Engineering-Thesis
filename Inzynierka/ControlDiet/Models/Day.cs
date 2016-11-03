using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private double energy;

        public double Energy
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

        private double protein;

        public double Protein
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

        private double carbohydrate;

        public double Carbohydrate
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

        private double fat;

        public double Fat
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

        private double sugar;

        public double Sugar
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

        private double fiber;

        public double Fiber
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