namespace Sample.HumanFriendlyId.Test;

[TestFixture]
public class HumanFriendlyIdTests
{
    private const string allowedChars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";

    [Test]
    public void Id_IsGenerated()
    {
        var length = 16;

        var sut = new HumanFriendlyId(length);
        var actualId = sut.Id;

        Assert.That(actualId.Length, Is.EqualTo(16));
        Assert.That(actualId, Does.Match($"^[{allowedChars}]+$"));
    }

    [Test]
    public void DisplayId_IsGroupedBySpaces()
    {
        var length = 16;
        var expectedLength = 19;

        var sut = new HumanFriendlyId(length);
        var actualDisplayId = sut.DisplayId;

        Assert.That(actualDisplayId.Length, Is.EqualTo(expectedLength));
        Assert.That(actualDisplayId, Does.Match($"^[{allowedChars}]{{4}} [{allowedChars}]{{4}} [{allowedChars}]{{4}} [{allowedChars}]{{4}}$"));
    }

    [TestCase("ABCDEFGHJKLM", "ABCDEFGHJKLM", TestName = nameof(Parse) + "_InputIsUpperCase")]
    [TestCase("abcdefghjklm", "ABCDEFGHJKLM", TestName = nameof(Parse) + "_InputIsLowerCase")]
    [TestCase("I", "1", TestName = nameof(Parse) + "_InputBigIIsReplacedWith1")]
    [TestCase("i", "1", TestName = nameof(Parse) + "_InputSnallIIsReplacedWith1")]
    [TestCase("ABCD EFGH JKLM", "ABCDEFGHJKLM", TestName = nameof(Parse) + "_InputSpacesAreReplaced")]
    public void Parse(string input, string expectedId)
    {
        var sut = HumanFriendlyId.Parse(input);
        var actualId = sut.Id;

        Assert.That(actualId, Is.EqualTo(expectedId));
    }
}