namespace PickPoint.Lib.Extensions;

public static class DateTimeExtensions
{
    public static long ToUnixTimeMilliseconds(this DateTime date)
    {
        if (date == DateTime.MinValue)
            return 0;

        var diff = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);

        return (long) Math.Round(diff.TotalMilliseconds);
    }
}