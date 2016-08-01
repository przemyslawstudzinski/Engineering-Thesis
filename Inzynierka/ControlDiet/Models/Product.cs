using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;

namespace Inzynierka.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey][AutoIncrement]
        public long Id { set; get; }

        public string name { set; get; }
    }
}
