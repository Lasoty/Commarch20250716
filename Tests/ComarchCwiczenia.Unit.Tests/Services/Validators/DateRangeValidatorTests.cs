using System.Runtime.InteropServices.JavaScript;
using ComarchCwiczenia.Services.Model;
using ComarchCwiczenia.Services.Validators;

namespace ComarchCwiczenia.Unit.Tests.Services.Validators;

[TestFixture]
internal class DateRangeValidatorTests
{
    private DateRangeValidator _cut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {

    }

    [SetUp]
    public void SetUp()
    {
        _cut = new DateRangeValidator();
    }

    [Test]
    public void AreDatesWithinRange_AllDatesWithinRange_ReturnTrue()
    {
        // Arrange 
        var range = new DateRange(new DateTime(2020, 1, 1), new DateTime(2020, 12, 31));
        List<DateTime> dates = [new(2020, 3, 1), new(2020, 6, 15), new(2020, 12, 1)];
        
        // Act
        var actual = _cut.AreDatesWithinRange(dates, range);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void AreDatesWithinRange_AtleastOneDateOutsideRange_ReturnsFalse()
    {
        // Arrange 
        var range = new DateRange(new DateTime(2020, 1, 1),
            new DateTime(2020, 12, 31));
        List<DateTime> dates = [
            new(2020, 3, 1), 
            new(2020, 6, 15), 
            new(2021, 1, 1)];

        // Act
        var actual = _cut.AreDatesWithinRange(dates, range);

        // Assert
        Assert.That(actual, Is.False);
    }

    [TearDown]
    public void TearDown()
    {
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
    }
}
