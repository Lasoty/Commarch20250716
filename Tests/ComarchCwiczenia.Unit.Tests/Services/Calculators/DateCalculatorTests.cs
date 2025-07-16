using ComarchCwiczenia.Services.Calculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia.Unit.Tests.Services.Calculators;
internal class DateCalculatorTests
{
    private DateCalculator _calculator;

    [SetUp]
    public void SetUp()
    {
        _calculator = new DateCalculator();
    }

    [TestCase("2025-07-14", "2025-07-15")] // Monday -> Tuesday
    [TestCase("2025-07-15", "2025-07-16")] // Tuesday -> Wednesday
    [TestCase("2025-07-16", "2025-07-17")] // Wednesday -> Thursday
    [TestCase("2025-07-17", "2025-07-18")] // Thursday -> Friday
    [TestCase("2025-07-18", "2025-07-21")] // Friday -> Monday
    [TestCase("2025-07-19", "2025-07-21")] // Saturday -> Monday
    [TestCase("2025-07-20", "2025-07-21")] // Sunday -> Monday
    public void GetNextBusinessDay_ReturnsExpectedDate(string inputDateStr, string expectedDateStr)
    {
        // Arrange
        var inputDate = DateTime.Parse(inputDateStr);
        var expectedDate = DateTime.Parse(expectedDateStr);

        // Act
        var result = _calculator.GetNextBusinessDay(inputDate);

        // Assert
        Assert.That(result, Is.EqualTo(expectedDate));
    }
}
