using ComarchCwiczenia.Services.Model;

namespace ComarchCwiczenia.Services.Validators;

public class DateRangeValidator
{
    public bool AreDatesWithinRange(List<DateTime> dates, DateRange range)
    {
        return dates != null 
               && dates.Count != 0 
               && dates.All(date => date >= range.From && date <= range.To);
    }
}
