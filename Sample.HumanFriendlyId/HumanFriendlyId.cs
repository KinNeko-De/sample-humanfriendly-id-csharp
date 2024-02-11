using System.Security.Cryptography;

namespace Sample.HumanFriendlyId;

public readonly struct HumanFriendlyId
{
    const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";

    public HumanFriendlyId(int length) : this(RandomNumberGenerator.GetItems<char>(chars, length))
    {
    }

    private HumanFriendlyId(char[] humanFriendlyId)
    {
        Id = new string(humanFriendlyId);
        DisplayId = new string(GroupBySpaces(humanFriendlyId).ToArray());
    }

    public readonly string Id { get; private init; }

    public readonly string DisplayId { get; private init; }

    public static HumanFriendlyId Parse(string userInput)
    {
        return new HumanFriendlyId(userInput.ToUpper()
            .Replace("I", "1")
            .Replace(" ", string.Empty).ToCharArray());
    }

    private static IEnumerable<char> GroupBySpaces(char[] humanFriendlyId)
    {
        for(int i = 0; i < humanFriendlyId.Length; i++)
        {
            if(i != 0 && i % 4 == 0)
            {
                yield return ' ';
            }
            yield return humanFriendlyId[i];
        }
    }
}
