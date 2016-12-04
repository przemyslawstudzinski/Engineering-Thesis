using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Day")]
    public class Day
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { set; get; }

        [Column("Date")]
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