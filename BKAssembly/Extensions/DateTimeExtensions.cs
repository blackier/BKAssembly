// This example demonstrates the DateTime.DayOfWeek property
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
public static class DateTimeExtensions
{
    // https://stackoverflow.com/questions/24245523/getting-the-first-and-last-day-of-a-month-using-a-given-datetime-object
    public static List<DateTime> DaysOfWeek(this DateTime value, DayOfWeek firstDayOfWeek)
    {
        List<DateTime> days = new List<DateTime>();

        int ii = value.DayOfWeek - firstDayOfWeek;
        DateTime firstDay = ii >= 0 ? value.AddDays(-ii) : value.AddDays(-7 - ii);
        days.Add(firstDay);
        days.Add(firstDay.AddDays(1));
        days.Add(firstDay.AddDays(2));
        days.Add(firstDay.AddDays(3));
        days.Add(firstDay.AddDays(4));
        days.Add(firstDay.AddDays(5));
        days.Add(firstDay.AddDays(6));
        return days;
    }

    public static Dictionary<DateTime, DateTime> WeeksOfMonth(this DateTime value, DayOfWeek firstDayOfWeek)
    {
        Dictionary<DateTime, DateTime> weeks = new Dictionary<DateTime, DateTime>();
        for (DateTime day = value.FirstDayOfMonth(); day < value.LastDayOfMonth(); day = day.AddDays(7))
            weeks.Add(day.DaysOfWeek(firstDayOfWeek).First(), day.DaysOfWeek(firstDayOfWeek).Last());
        if (weeks.Last().Value < value.LastDayOfMonth())
            weeks.Add(value.LastDayOfMonth().DaysOfWeek(firstDayOfWeek).First(), value.LastDayOfMonth().DaysOfWeek(firstDayOfWeek).Last());
        return weeks;
    }

    public static Dictionary<DateTime, DateTime> MonthsOfYear(this DateTime value)
    {
        Dictionary<DateTime, DateTime> months = new Dictionary<DateTime, DateTime>();
        var firstMonth = new DateTime(value.Year, 1, 1);
        for (int i = 0; i < 12; i++)
            months.Add(firstMonth.AddMonths(i).FirstDayOfMonth(), firstMonth.AddMonths(i).LastDayOfMonth());
        return months;
    }

    public static DateTime FirstDayOfMonth(this DateTime value)
    {
        return new DateTime(value.Year, value.Month, 1);
    }

    public static DateTime LastDayOfMonth(this DateTime value)
    {
        return value.AddMonths(1).FirstDayOfMonth().AddDays(-1);
    }

    public static DateTime BeforDay(this DateTime value)
    {
        return value.AddDays(-1);
    }
    public static DateTime NextDay(this DateTime value)
    {
        return value.AddDays(1);
    }

    public static DateTime BeforWeek(this DateTime value)
    {
        return value.AddDays(-7);
    }
    public static DateTime NextWeek(this DateTime value)
    {
        return value.AddDays(7);
    }

    public static DateTime BeforMonth(this DateTime value)
    {
        return value.AddMonths(-1);
    }
    public static DateTime NextMonth(this DateTime value)
    {
        return value.AddMonths(1);
    }
    public static DateTime BeforYear(this DateTime value)
    {
        return value.AddYears(-1);
    }
    public static DateTime NextYear(this DateTime value)
    {
        return value.AddYears(1);
    }

}
