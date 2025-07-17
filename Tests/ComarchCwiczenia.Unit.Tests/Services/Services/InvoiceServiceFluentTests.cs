using ComarchCwiczenia.Services.Services;
using FluentAssertions;

namespace ComarchCwiczenia.Unit.Tests.Services.Services;

public class InvoiceServiceFluentTests
{
    private InvoiceService _cut;

    [SetUp]
    public void Setup()
    {
        _cut = new InvoiceService();
    }

    #region Testy string
    [Test]
    public void GetInvoiceNumber_ShouldStartWith_INV()
    {
        var actual = _cut.GetInvoiceNumber();
        actual.Should().StartWith("INV-");
    }

    [Test]
    public void GetInvoiceNumber_ShouldEndWith_NumberSuffix()
    {
        var actual = _cut.GetInvoiceNumber();
        actual.Should().MatchRegex(@"INV-\d{8}-\d{3}$");
    }

    [Test]
    public void GetInvoiceNumber_ShouldContain_CurrentDate()
    {
        var currentDate = DateTime.Now.ToString("yyyyMMdd");

        var actual = _cut.GetInvoiceNumber();

        actual.Should().Contain(currentDate);
    }

    [Test]
    public void GetInvoiceNumber_ShouldMatch_ExpectedFormat()
    {
        var actual = _cut.GetInvoiceNumber();

        actual.Should().Match("INV-????????-???").And.BeOfType<string>();
    }
    #endregion

    #region Testy kolekcji
    [Test]
    public void GenerateInvoiceItems_ShouldReturn_NotEmptyCollection()
    {
        var actual = _cut.GenerateInvoiceItems();
        actual.Should().NotBeEmpty();
    }

    [TestCase("Laptop")]
    [TestCase("Smartphone")]
    [TestCase("Tablet")]
    public void GenerateInvoiceItems_ShouldContain_ItemWithSpecificName(string productName)
    {
        var actual = _cut.GenerateInvoiceItems();
        actual.Should().Contain(item => item.ProductName == productName);
        actual.Should().Contain(item => item.ProductName.Equals(productName, StringComparison.InvariantCultureIgnoreCase));
    }

    [Test]
    public void GenerateInvoiceItems_AllItems_ShouldHavePositiveQuantity()
    {
        var actual = _cut.GenerateInvoiceItems();
        actual.Should().OnlyContain(item => item.Quantity > 0);
    }

    [Test]
    public void GenerateInvoiceItems_ShouldHave_ItemsInAscendingOrderByQuantity()
    {
        var actual = _cut.GenerateInvoiceItems();
        actual.Should().BeInAscendingOrder(item => item.Quantity);
    } 
    #endregion

    [Test]
    public async Task FakedErrorMethod_Should_ThrowsException()
    {
        (await _cut.Invoking(async service => await service.FakedErrorMethod()).Should()
            .ThrowAsync<FormatException>()).WithMessage("*Podany format jest nieprawidłowy*");

        Func<Task> act = async () => await _cut.FakedErrorMethod();
        await act.Should().ThrowAsync<FormatException>()
            .WithMessage("*Podany format jest nieprawidłowy*");
    }

    [Test]
    public void GenerateInvoiceItems_ShouldRaise_InvoiceItemsGeneratedEvent()
    {
        using var monitoredSubject = _cut.Monitor();
        var items = _cut.GenerateInvoiceItems();
        monitoredSubject.Should().Raise(nameof(InvoiceService.InvoiceItemsGenerated));
    }


}
