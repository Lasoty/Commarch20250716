using ComarchCwiczenia.Services.Services;
using Moq;
using FluentAssertions;

namespace ComarchCwiczenia.Unit.Tests.Services.Services;

[TestFixture]
public class OrderServiceMoqTests
{
    [Test]
    public void PlaceOrder_ShouldReturnTrue_WhenProductIsAvailable()
    {
        // Arrange
        var inventoryMock = new Mock<IInventoryService>();
        inventoryMock.Setup(x => x.IsProductAvailable(It.IsAny<string>())).Returns(true);

        var orderService = new OrderService(inventoryMock.Object);

        // Act
        var result = orderService.PlaceOrder("ABC");

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void PlaceOrder_ShouldReturnFalse_WhenProductIsNotAvailable()
    {
        // Arrange
        var inventoryMock = new Mock<IInventoryService>();
        inventoryMock.Setup(x => x.IsProductAvailable(It.IsAny<string>())).Returns(false);

        var orderService = new OrderService(inventoryMock.Object);

        // Act
        var result = orderService.PlaceOrder("XYZ");

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void PlaceOrder_ShouldCallIsProductAvailable_Once()
    {
        // Arrange
        var inventoryMock = new Mock<IInventoryService>();
        inventoryMock.Setup(x => x.IsProductAvailable(It.IsAny<string>())).Returns(true);

        var orderService = new OrderService(inventoryMock.Object);

        // Act
        orderService.PlaceOrder("ABC");

        // Assert
        inventoryMock.Verify(x => x.IsProductAvailable("ABC"), Times.Once);
    }

    [Test]
    public void PlaceOrder_ShouldThrowArgumentNullException_WhenProductIdIsNull()
    {
        // Arrange
        var inventoryMock = new Mock<IInventoryService>();
        var orderService = new OrderService(inventoryMock.Object);

        // Act
        Action act = () => orderService.PlaceOrder(null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("productId");
    }
}
