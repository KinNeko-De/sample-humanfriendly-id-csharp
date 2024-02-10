using System.Security.Cryptography;

namespace Sample.HumanFriendlyId;

public class HumanFriendlyId
{
    const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
    readonly char[] humanFriendlyId;

    public HumanFriendlyId(int length) : this(RandomNumberGenerator.GetItems<char>(chars, length))
    {
    }

    protected HumanFriendlyId(char[] humanFriendlyId)
    {
        this.humanFriendlyId = humanFriendlyId;
    }

    public static HumanFriendlyId Parse(string userInput)
    {
        return new HumanFriendlyId(userInput.ToUpper()
            .Replace("I", "1")
            .Replace(" ", string.Empty).ToCharArray());
    }

    public string Id
    {
        get
        {
            return new string(humanFriendlyId);
        }
    }

    public string DisplayId
    {
        get
        {
            return new string(GroupBySpaces(humanFriendlyId).ToArray());
        }
    }

    private static IEnumerable<char> GroupBySpaces(char[] humanFriendlyId)
    {
        for(int i = 0; i < humanFriendlyId.Length; i++)
        {
            if(i % 4 == 0 && i != 0)
            {
                yield return ' ';
            }
            yield return humanFriendlyId[i];
        }
    }
}
