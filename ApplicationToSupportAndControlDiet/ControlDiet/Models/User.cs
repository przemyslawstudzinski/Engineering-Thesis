using SQLite.Net.Attributes;

namespace ApplicationToSupportAndControlDiet.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("UserId")]
        public int UserId { set; get; }

        [Column("Sex")]
        public Sex Sex { set; get; }

        [Column("Age")]
        public int Age { set; get; }

        [Column("Height")]
        public float Height { set; get; }

        [Column("Weight")]
        public float Weight { set; get; }

        [Column("GoalOfWeightToAchieve")]
        public UserGoal GoalOfWeightToAchieve { set; get; }

        [Column("DailyActivity")]
        public ActivityLevel DailyActivity { set; get; }

        [Column("TotalDailyEnergyExpenditure")]
        public int TotalDailyEnergyExpenditure { set; get; }

        public User() { }

        public User(int id, Sex sex, int age, float height, float weight, 
            UserGoal goal, ActivityLevel activity) {
            this.UserId = id;
            this.Sex = sex;
            this.Age = age;
            this.Height = height;
            this.Weight = weight;
            this.GoalOfWeightToAchieve = goal;
            this.DailyActivity = activity;
        }
    }
}