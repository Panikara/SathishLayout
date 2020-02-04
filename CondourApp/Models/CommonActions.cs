using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondourApp.Models
{
    public class CommonActions
    {
        public static DateTime GetCurrentISTTime()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            return indianTime;
        }
    }
}