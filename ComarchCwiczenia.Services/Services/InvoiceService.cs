using ComarchCwiczenia.Services.Model;

namespace ComarchCwiczenia.Services.Services;

public class InvoiceService
{
    public event EventHandler? InvoiceItemsGenerated;

    public string GetInvoiceNumber()
    {
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        var randomPart = new Random().Next(100, 999).ToString();
        return $"INV-{datePart}-{randomPart}";
    }

    public List<InvoiceItem> GenerateInvoiceItems()
    {
        InvoiceItemsGenerated?.Invoke(this, EventArgs.Empty);

        return
        [
            new() { ProductName = "Laptop", Quantity = 1, UnitPrice = 1000m },
            new() { ProductName = "Smartphone", Quantity = 2, UnitPrice = 500m },
            new() { ProductName = "Tablet", Quantity = 3, UnitPrice = 300m }
        ];
    }

    public async Task FakedErrorMethod()
    {
        throw new FormatException("Podany format jest nieprawidłowy");
    }
}
