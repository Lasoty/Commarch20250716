using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Services.Validators;

namespace ComarchCwiczenia.Unit.Tests.Services.Validators;
internal class IntegerValidatorTests
{
    [TestCase(1, true)]
    [TestCase(0, false)]
    [TestCase(-1, false)]
    public void NumberIsPositive_BorderValues_ReturnsExpectedResult(int x, bool expected)
    {
        //Arrange
        var cut = new IntegerValidator();

        // Act
        var actual = cut.NumberIsPositive(x);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void NumberIsPositive_AllPositiveRandomNumbers_ReturnsTrue([Random(1, int.MaxValue, 1)] int x)
    {
        //Arrange
        var cut = new IntegerValidator();

        // Act
        var actual = cut.NumberIsPositive(x);

        // Assert
        Assert.That(actual, Is.True);
    }
}
