namespace ComarchCwiczenia.Services.Services;

public class ListProcessor
{
    public IEnumerable<int> FilterEvenNumbers(IEnumerable<int> numbers)
    {
        return numbers.Where(n => n % 2 == 0);
    }

    public List<string> NormalizeStrings(IEnumerable<string?> inputs)
    {
        return inputs
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s!.Trim().ToLowerInvariant())
            .ToList();
    }

    public List<string> SortAlphabetically(IEnumerable<string> inputs)
    {
        return inputs.OrderBy(s => s).ToList();
    }
}

