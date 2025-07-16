namespace ComarchCwiczenia.Services.Calculators;

public class DateCalculator
{
    public DateTime GetNextBusinessDay(DateTime date)
    {
        date = date.AddDays(1);
        while (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            date = date.AddDays(1);
        }
        return date;
    }
}
