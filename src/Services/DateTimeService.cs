using System;

namespace musichino.Services
{
    public class DateTimeService
    {
        public static DateTime UnixTimeToDateTime(int unixTime)
        {
            var date = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
            return date.AddSeconds(unixTime);
        }
    }
}