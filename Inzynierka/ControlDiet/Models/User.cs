using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { set; get; }

        [Column("sex")]
        public Sex Sex { set; get; }

        [Column("age")]
        public int Age { set; get; }

        [Column("height")]
        public float Height { set; get; }

        [Column("weight")]
        public float Weight { set; get; }

        [Column("goal")]
        public UserGoal Goal { set; get; }

        [Column("activity")]
        public ActivityLevel Activity { set; get; }

        [Column("total_daily_energy_expenditure")]
        public int TotalDailyEnergyExpenditure { set; get; }


        public User() { }
        public User(int id, Sex sex, int age, float height, float weight, UserGoal goal, ActivityLevel activity) {
            this.Id = id;
            this.Sex = sex;
            this.Age = age;
            this.Height = height;
            this.Weight = weight;
            this.Goal = goal;
            this.Activity = activity;
        }

    }
}
