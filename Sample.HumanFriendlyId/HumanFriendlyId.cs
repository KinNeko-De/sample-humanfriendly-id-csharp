using System.Security.Cryptography;

namespace Sample.HumanFriendlyId;

public class HumanFriendlyId
{
    const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";

    public HumanFriendlyId(int length) : this(RandomNumberGenerator.GetItems<char>(chars, length))
    {
    }

    protected HumanFriendlyId(char[] humanFriendlyId)
    {
        Id = new string(humanFriendlyId);
        DisplayId = new string(GroupBySpaces(humanFriendlyId).ToArray());
    }

    public string Id { get; private set; }

    public string DisplayId { get; private set; }

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
