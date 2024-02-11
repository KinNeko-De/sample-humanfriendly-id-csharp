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

        Assert.That(actualId, Has.Length.EqualTo(length));
        Assert.That(actualId, Does.Match($"^[{allowedChars}]+$"));
    }

    [TestCase(1, 1)]
    [TestCase(4, 4)]
    [TestCase(5, 6)]
    [TestCase(8, 9)]
    [TestCase(9, 11)]
    [TestCase(12, 14)]
    [TestCase(13, 16)]
    public void DisplayId_IsGroupedBySpaces(int length, int expectedLength)
    {
        var sut = new HumanFriendlyId(length);
        var actualDisplayId = sut.DisplayId;

        string regex = CreateRegex(length);

        Assert.That(actualDisplayId, Has.Length.EqualTo(expectedLength));
        Assert.That(actualDisplayId, Does.Match(regex));
    }

    private static string CreateRegex(int length)
    {
        var rest = length % 4;
        var expectedBlocks = length / 4;

        const string regexTemplate = $"[{allowedChars}]{{4}}";
        string lastRegex = rest > 0 ? $"[{allowedChars}]{{{rest}}}" : string.Empty;
        string regex = $"^";

        for (int i = 0; i < expectedBlocks; i++)
        {
            if (i != 0)
            {
                regex += " ";
            }
            regex += regexTemplate;
        }

        if (expectedBlocks > 0 && rest > 0)
        {
            regex += " ";
        }
        regex += lastRegex + "$";
        return regex;
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