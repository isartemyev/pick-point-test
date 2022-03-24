namespace PickPoint.Lib.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfDayUtc(this DateTime date)
        {
            var utcDate = date.ToUniversalTime();
            var result  = new DateTime(utcDate.Year, utcDate.Month, utcDate.Day, 0, 0, 0);

            return result;
        }

        public static DateTime StartOfWeekUtc(this DateTime date, DayOfWeek startOfWeek)
        {
            var utcDate = date.ToUniversalTime();
            var diff    = (7 + (utcDate.DayOfWeek - startOfWeek)) % 7;
            var result  = utcDate.AddDays(-1 * diff).Date;

            return result;
        }

        public static DateTime StartOfMonthUtc(this DateTime date)
        {
            var utcDate = date.ToUniversalTime();
            var result  = new DateTime(utcDate.Year, utcDate.Month, 1, 0, 0, 0);

            return result;
        }

        public static DateTime EndOfMonthUtc(this DateTime date)
        {
            var utcDate = date.ToUniversalTime();
            var result  = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 59999);

            return result;
        }

        public static long ToUnixTimeSeconds(this DateTime date)
        {
            if (date == DateTime.MinValue)
                return 0;

            var diff = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return (long) Math.Round(diff.TotalSeconds);
        }

        public static long ToUnixTimeMilliseconds(this DateTime date)
        {
            if (date == DateTime.MinValue)
                return 0;

            var diff = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return (long) Math.Round(diff.TotalMilliseconds);
        }

        public static DateTime ToUnixDateTime(this ulong seconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        }

        public static DateTime ToUnixDateTime(this long seconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        }

        public static DateTime ToUnixStartDateTime(this DateTime date)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        }

        public static bool IsGreaterOrEqual(this DateTime dateTime, DateTime other) => dateTime >= other;

        public static bool InRange(this DateTime dateTime, DateTime start, DateTime end) => dateTime >= start && dateTime <= end;

        public static DateTime StartOfDay(this DateTime dateTime) => dateTime.Date;

        public static DateTime EndOfDay(this DateTime dateTime) => dateTime.Date.AddDays(1).AddMilliseconds(-1);
    }
}