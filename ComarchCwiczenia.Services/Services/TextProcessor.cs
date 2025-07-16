namespace ComarchCwiczenia.Services.Services;

public class TextProcessor
{
    public string Normalize(string input)
    {
        return input?.Trim().ToLowerInvariant();
    }

    public string? GetGreeting(string? name)
    {
        return string.IsNullOrWhiteSpace(name) ? null : $"Hello, {name}!";
    }

    public string Repeat(string text, int count)
    {
        return string.Concat(Enumerable.Repeat(text, count));
    }
}
