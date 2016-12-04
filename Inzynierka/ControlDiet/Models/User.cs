using SQLite.Net.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { set; get; }

        [Column("Sex")]
        public Sex Sex { set; get; }

        [Column("Age")]
        public int Age { set; get; }

        [Column("Height")]
        public float Height { set; get; }

        [Column("Weight")]
        public float Weight { set; get; }

        [Column("Goal")]
        public UserGoal Goal { set; get; }

        [Column("Activity")]
        public ActivityLevel Activity { set; get; }

        [Column("TotalDailyEnergyExpenditure")]
        public int TotalDailyEnergyExpenditure { set; get; }

        public User() { }

        public User(int id, Sex sex, int age, float height, float weight, 
            UserGoal goal, ActivityLevel activity) {
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