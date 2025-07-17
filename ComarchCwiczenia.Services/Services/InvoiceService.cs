namespace ComarchCwiczenia.Services.Services;

public class InvoiceService
{
    public string GetInvoiceNumber()
    {
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        var randomPart = new Random().Next(100, 999).ToString();
        return $"INV-{datePart}-{randomPart}";
    }
}
