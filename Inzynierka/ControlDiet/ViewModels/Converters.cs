using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class Converters
    {
        public static DateTimeOffset ConvertDateTimeToDateTimeOffset(DateTime dateTime) {
            return new DateTimeOffset(dateTime);
        }

        public static DateTime ConvertDateTimeOffsetToDateTime(DateTimeOffset dateTimeOffset) {
            return dateTimeOffset.DateTime;
        }
    }
}
