using ComarchCwiczenia.Services.Calculators;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace ComarchCwiczenia.Unit.Tests.Services.Calculators;

[TestFixture]
internal class DateCalculatorFluentTests
{
    private DateCalculator _cut;

    [SetUp]
    public void Setup()
    {
        _cut = new DateCalculator();
    }

    [Test]
    public void GetNextBusinessDay_ShouldSkipWeekends()
    {
        var actual = _cut.GetNextBusinessDay(18.July(2025));
        actual.Should().Be(21.July(2025));

        _cut.GetNextBusinessDay(19.July(2025)).Should().Be(21.July(2025));
        
        _cut.GetNextBusinessDay(20.July(2025)).Should().Be(21.July(2025));
    }

    [Test]
    public void GetNextBusinessDay_ShouldHandleLeapYear()
    {
        _cut.GetNextBusinessDay(28.February(2024)).Should().Be(29.February(2024));
    }

    [Test]
    public void GetNextBusinessDay_ShouldNotChangeTimePart()
    {
        var time = 17.July(2025).At(10, 33, 11);
        _cut.GetNextBusinessDay(time).TimeOfDay.Should().Be(time.TimeOfDay);
    }
}
