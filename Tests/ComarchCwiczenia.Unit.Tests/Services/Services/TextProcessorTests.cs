using ComarchCwiczenia.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia.Unit.Tests.Services.Services;

[TestFixture]
internal class TextProcessorTests
{
    private TextProcessor _cut;

    [SetUp]
    public void Setup()
    {
        _cut = new TextProcessor();
    }

    [Test]
    public void Normalize_ReturnsLowercaseTrimmedString()
    {
        var expected = "hello";
        var actual = _cut.Normalize("  \nHello   ");
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Normalize_WhenNull_ReturnsNull()
    {
        var actual = _cut.Normalize(null!);
        Assert.That(actual, Is.Null);
    }

    [Test]
    public void Normalize_WhenWhitespaceOnly_ReturnsEmptyString()
    {
        var actual = _cut.Normalize("   \t\n\r  ");
        Assert.That(actual, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Greeting_IsCaseInsensitive()
    {
        var actual = _cut.GetGreeting("John");
        Assert.That(actual, Is.EqualTo("hello, john!").IgnoreCase);
    }

    [Test]
    public void Greeting_StartsWithHello()
    {
        var actual = _cut.GetGreeting("John");
        Assert.That(actual, Does.StartWith("Hello")
            .And.EndWith("!")
            .And.Contain(","));
    }

    [Test]
    public void Normalize_WithNonEmptyString_ReturnsNonEmpty()
    {
        var actual = _cut.Normalize("   hi   ");
        Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void Repeat_ReturnsExpectedLength()
    {
        var actual = _cut.Repeat("ab", 3);
        Assert.That(actual.Length, Is.EqualTo(6));
    }

    [Test]
    public void Greeting_ShouldHaveExpectedStructure()
    {
        var actual = _cut.GetGreeting("John");

        Assert.Multiple(() =>
        {
            Assert.That(actual, Does.StartWith("Hello"));
            Assert.That(actual, Does.Contain(","));
            Assert.That(actual, Does.EndWith("!"));
        });
    }

}
