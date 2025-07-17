using ComarchCwiczenia.Services.Model;

namespace ComarchCwiczenia.Services.Services;

public class InvoiceService
{
    private readonly ITaxService _taxService;
    private readonly IDiscountService _discountService;

    public InvoiceService()
    {
        
    }

    public InvoiceService(ITaxService taxService, IDiscountService discountService)
    {
        _taxService = taxService;
        _discountService = discountService;
    }

    public decimal CalculateTotal(decimal amount, string customerType)
    {
        var discount = _discountService.CalculateDiscount(amount, customerType);
        var taxableAmount = amount - discount;
        try
        {
            var tax = _taxService.GetTax(taxableAmount);
            return taxableAmount - tax;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Nie udało się obliczyć podatku.", ex);
        }
    }




    #region Stare metody

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

#endregion
}

public interface IDiscountService
{
    decimal CalculateDiscount(decimal amount, string customerType);
}

public interface ITaxService
{
    decimal GetTax(decimal amount);
}
