using System;

public static class ConvertUtil
{
    public static int ConvertToInt(object o)
    {
        return Convert.ToInt32(o);
    }

    public static string ConvertToString(object o)
    {
        return o.ToString();
    }

    public static T ConvertToEnum<T>(int o)
    {
        return (T)(Enum.ToObject(typeof(T), o));
    }

    public static T ConvertToEnum<T>(string o)
    {
        T ret = default(T);
        try
        {
            ret = (T)(Enum.Parse(typeof(T), o));
        }
        catch
        {
            ret = (T)(Enum.Parse(typeof(T), "Default"));
        }
        return ret;
    }

    public static long ConvertToLong(object o)
    {
        return Convert.ToInt64(o);
    }

    public static long ConvertToTimeStamp(DateTime value)
    {
        return (long)((value - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds);
    }

    public static string ConvertTimestampToDateReadable(string timeStamp)
    {
        DateTime dateTime = GetTime(timeStamp);
        return string.Format("{0}/{1}/{2}", dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public static long ConvertDataTimeLong(DateTime dt)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        TimeSpan toNow = dt.Subtract(dtStart);
        long timeStamp = toNow.Ticks;
        timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
        return timeStamp;
    }

    public static DateTime GetTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //time stamp is 13 bits, need 17bits totally 
        long lTime = long.Parse(timeStamp + "0000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    public static float GetFloatWithMost2Digits(float val)
    {
        return (int)(val * 100) / 100.0f;
    }
}