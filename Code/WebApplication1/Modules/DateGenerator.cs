using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Modules
{
    public class DateGenerator
    {
        public static DateTime SQLServerMinDateTime = DateTime.ParseExact("1753-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
        public static DateTime SQLServerCastZeroAsDateTime = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
        public static DateTime ZeroUnixTimestamp = DateTime.ParseExact("1970-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
        public static long ToUnixTimeStamp(DateTime dateTime)
        {
            return (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        public static DateTime GetRidOfMilisec(DateTime inputDateTime)
        {
            return inputDateTime.AddTicks(-(inputDateTime.Ticks % 10000000));
        }
    }
}