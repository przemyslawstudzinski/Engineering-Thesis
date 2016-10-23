using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public static class Globals
    {
        public static Nullable<DateTimeOffset> Date
        {
            get;
            set;
        }

        public static MainPage MainPage {
            get; 
            set;
        }
    }
}
