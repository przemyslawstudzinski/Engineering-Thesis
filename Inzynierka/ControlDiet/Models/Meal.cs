using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Meal")]
    public class Meal
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("MealId")]
        public int MealId { set; get; }

        [Column("MealName")]
        public string MealName { set; get; }

        [Column("TimeOfMeal")]
        public DateTime DateTimeOfMeal { set; get; }

        public String Time
        {
            get
            {
                return DateTimeOfMeal.ToLocalTime().TimeOfDay.ToString(@"hh\:mm");
            }
        }

        [Column("DayId")]
        [ForeignKey(typeof(Day))]
        public int DayId { set; get; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Day Day { set; get; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Ingridient> IngridientsInMeal { get; set; }

        private double energy;

        public double Energy
        {
            get
            {
                energy = 0;
                foreach (Ingridient element in IngridientsInMeal)
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
                foreach (Ingridient element in IngridientsInMeal)
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
                foreach (Ingridient element in IngridientsInMeal)
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
                foreach (Ingridient element in IngridientsInMeal)
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
                foreach (Ingridient element in IngridientsInMeal)
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
                foreach (Ingridient element in IngridientsInMeal)
                {
                    fiber += element.Fiber;
                }
                return fiber;
            }
        }

        public Meal()
        {
            IngridientsInMeal = new List<Ingridient>();
        }
    }
}