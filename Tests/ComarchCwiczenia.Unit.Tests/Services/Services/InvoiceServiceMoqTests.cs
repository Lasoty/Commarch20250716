using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Services.Services;
using FluentAssertions;
using Moq;

namespace ComarchCwiczenia.Unit.Tests.Services.Services;

[TestFixture]
internal class InvoiceServiceMoqTests
{
    private Mock<IDiscountService> _discountService;
    private Mock<ITaxService> _taxServiceMock;
    private InvoiceService _sut;

    [SetUp]
    public void Setup()
    {
        _taxServiceMock = new Mock<ITaxService>();
        _discountService = new Mock<IDiscountService>();
        _sut = new InvoiceService(_taxServiceMock.Object, _discountService.Object);
    }

    [Test]
    public void CalculateTotal_WhenCalled_ReturnsExpectedTotal()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";

        _discountService.Setup(ds => 
            ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(10m);

        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>())).Returns(5m);

        // Act
        var total = _sut.CalculateTotal(amount, customerType);

        //Assert
        total.Should().Be(85m);
    }

    [Test]
    public void CalculateTotal_WhenCalled_VerifiesTaxServiceIsCalled()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";

        _discountService.Setup(ds =>
                ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(10m);
        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>())).Returns(5m).Verifiable();

        // Act
        _sut.CalculateTotal(amount, customerType);

        // Assert
        _taxServiceMock.Verify(x => x.GetTax(It.IsAny<decimal>()), Times.Once);
    }

    [Test]
    public void CalculateTotal_ThrowsWhenTaxServiceFails()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";

        _discountService.Setup(ds =>
                ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(10m);
        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>()))
            .Throws(new Exception("Testowy błąd"));

        // Act
        _sut.Invoking(cs => cs.CalculateTotal(amount, customerType)).Should()
            .Throw()
            .WithMessage("Nie udało się obliczyć podatku.")
            .WithInnerException<Exception>()
            .WithMessage("Testowy błąd");
    }
}
