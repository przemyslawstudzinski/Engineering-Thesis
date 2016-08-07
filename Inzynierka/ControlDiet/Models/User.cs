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
        public long Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

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
    }
}
