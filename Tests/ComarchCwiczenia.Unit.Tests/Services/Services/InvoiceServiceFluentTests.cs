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
}
